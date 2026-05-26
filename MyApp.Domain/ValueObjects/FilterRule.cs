using System;

namespace MyApp.Domain.ValueObjects
{
    /// <summary>
    /// Repräsentiert eine einzelne Filter-Regel innerhalb einer Smart Collection.
    /// </summary>
    public sealed record FilterRule
    {
        public string PropertyName { get; init; } = string.Empty; // z.B. "Bpm", "Genre", "Key"
        public FilterOperator Operator { get; init; } = FilterOperator.Equals;
        public string Value { get; init; } = string.Empty;
    }

    public enum FilterOperator
    {
        Equals,
        NotEquals,
        GreaterThan,
        LessThan,
        Contains,
        NotContains,
        StartsWith,
        EndsWith
    }
}
