using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyApp.Domain.Entities;
using MyApp.Domain.ValueObjects;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyApp.Desktop.ViewModels
{
    public partial class SmartCollectionEditorViewModel : ViewModelBase
    {
        private readonly SmartCollection _collection;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private bool _matchAllRules;

        public ObservableCollection<FilterRuleViewModel> Rules { get; } = new();
        public ObservableCollection<string> AvailableProperties { get; } = new() { "Title", "Artist", "Album", "Genre", "Bpm", "Key" };
        public ObservableCollection<FilterOperator> AvailableOperators { get; } = new(Enum.GetValues<FilterOperator>().Cast<FilterOperator>());

        public event Action<SmartCollection?>? CloseRequested;

        public SmartCollectionEditorViewModel(SmartCollection collection)
        {
            _collection = collection;
            _name = collection.Name;
            _matchAllRules = collection.MatchAllRules;

            foreach (var rule in collection.Rules)
            {
                Rules.Add(new FilterRuleViewModel(rule));
            }

            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);
            AddRuleCommand = new RelayCommand(AddRule);
            RemoveRuleCommand = new RelayCommand<FilterRuleViewModel>(RemoveRule);
        }

        public IRelayCommand SaveCommand { get; }
        public IRelayCommand CancelCommand { get; }
        public IRelayCommand AddRuleCommand { get; }
        public IRelayCommand<FilterRuleViewModel> RemoveRuleCommand { get; }

        private void AddRule()
        {
            Rules.Add(new FilterRuleViewModel(new FilterRule { PropertyName = "Genre", Operator = FilterOperator.Contains }));
        }

        private void RemoveRule(FilterRuleViewModel? rule)
        {
            if (rule != null) Rules.Remove(rule);
        }

        private void Save()
        {
            _collection.Name = Name;
            _collection.MatchAllRules = MatchAllRules;
            _collection.Rules.Clear();
            _collection.Rules.AddRange(Rules.Select(r => r.ToModel()));

            CloseRequested?.Invoke(_collection);
        }

        private void Cancel() => CloseRequested?.Invoke(null);
    }

    public partial class FilterRuleViewModel : ObservableObject
    {
        [ObservableProperty] private string _propertyName;
        [ObservableProperty] private FilterOperator _operator;
        [ObservableProperty] private string _value;

        public FilterRuleViewModel(FilterRule rule)
        {
            _propertyName = rule.PropertyName;
            _operator = rule.Operator;
            _value = rule.Value;
        }

        public FilterRule ToModel() => new FilterRule 
        { 
            PropertyName = PropertyName, 
            Operator = Operator, 
            Value = Value 
        };
    }
}
