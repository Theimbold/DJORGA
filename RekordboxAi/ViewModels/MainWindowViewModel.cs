using System;
using System.Reactive;
using ReactiveUI;

namespace RekordboxAi.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        private string _title = "RekordboxAi - Avalonia";
n        public string Title
        {
            get => _title;
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }
n        public ReactiveCommand<Unit, Unit> ClickCommand { get; }

        public MainWindowViewModel()
        {
            ClickCommand = ReactiveCommand.Create(OnClick);
        }
n        private void OnClick()
        {
            Title = "Button clicked at " + DateTime.Now.ToLongTimeString();
        }
    }
}
