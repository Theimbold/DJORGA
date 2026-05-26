using DJORGA.Domain.Entities;
using DJORGA.Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DJORGA.Application.Services
{
    public class RuleEvaluatorService
    {
        private readonly ILogger<RuleEvaluatorService> _logger;

        public RuleEvaluatorService(ILogger<RuleEvaluatorService> logger)
        {
            _logger = logger;
        }

        public IQueryable<Track> ApplyRules(IQueryable<Track> query, IEnumerable<FilterRule> rules, bool matchAll)
        {
            if (rules == null || !rules.Any()) return query;

            Expression? finalExpression = null;
            var parameter = Expression.Parameter(typeof(Track), "t");

            foreach (var rule in rules)
            {
                var ruleExpr = CreateExpression(parameter, rule);
                if (ruleExpr == null) continue;

                finalExpression = finalExpression == null
                    ? ruleExpr
                    : matchAll
                        ? Expression.AndAlso(finalExpression, ruleExpr)
                        : Expression.OrElse(finalExpression, ruleExpr);
            }

            if (finalExpression == null) return query;

            var lambda = Expression.Lambda<Func<Track, bool>>(finalExpression, parameter);
            return query.Where(lambda);
        }

        private Expression? CreateExpression(ParameterExpression parameter, FilterRule rule)
        {
            try
            {
                var property = Expression.Property(parameter, rule.PropertyName);
                var constant = Expression.Constant(ConvertValue(rule.Value, property.Type));

                return rule.Operator switch
                {
                    FilterOperator.Equals => Expression.Equal(property, constant),
                    FilterOperator.NotEquals => Expression.NotEqual(property, constant),
                    FilterOperator.GreaterThan => Expression.GreaterThan(property, constant),
                    FilterOperator.LessThan => Expression.LessThan(property, constant),
                    FilterOperator.Contains => CreateStringMethodExpression(property, "Contains", constant),
                    FilterOperator.StartsWith => CreateStringMethodExpression(property, "StartsWith", constant),
                    FilterOperator.EndsWith => CreateStringMethodExpression(property, "EndsWith", constant),
                    _ => null
                };
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Could not build filter expression for rule (Property={Property}, Operator={Operator}, Value={Value}); rule skipped.",
                    rule.PropertyName, rule.Operator, rule.Value);
                return null;
            }
        }

        private Expression CreateStringMethodExpression(MemberExpression property, string methodName, ConstantExpression constant)
        {
            var method = typeof(string).GetMethod(methodName, new[] { typeof(string) });
            return Expression.Call(property, method!, constant);
        }

        private object? ConvertValue(string value, Type targetType)
        {
            if (targetType.IsEnum) return Enum.Parse(targetType, value, true);
            if (targetType == typeof(double)) return double.TryParse(value, out var d) ? d : 0.0;
            if (targetType == typeof(int)) return int.TryParse(value, out var i) ? i : 0;
            if (targetType == typeof(Guid)) return Guid.TryParse(value, out var g) ? g : Guid.Empty;
            return value;
        }
    }
}
