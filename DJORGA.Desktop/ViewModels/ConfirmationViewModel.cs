using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;

namespace DJORGA.Desktop.ViewModels
{
    public partial class ConfirmationViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private string _message;

        [ObservableProperty]
        private string _confirmButtonText = "Confirm";

        [ObservableProperty]
        private string _cancelButtonText = "Cancel";

        [ObservableProperty]
        private bool _showSecondaryOption;

        [ObservableProperty]
        private string _secondaryOptionText = string.Empty;

        [ObservableProperty]
        private bool _isSecondaryOptionChecked;

        public object? Tag { get; set; }

        public event Action<ConfirmationResult>? CloseRequested;

        public ConfirmationViewModel(string title, string message)
        {
            _title = title;
            _message = message;
            _secondaryOptionText = string.Empty;
            
            ConfirmCommand = new RelayCommand(() => CloseRequested?.Invoke(new ConfirmationResult(true, IsSecondaryOptionChecked)));
            CancelCommand = new RelayCommand(() => CloseRequested?.Invoke(new ConfirmationResult(false, false)));
        }

        public IRelayCommand ConfirmCommand { get; }
        public IRelayCommand CancelCommand { get; }
    }

    public record ConfirmationResult(bool Confirmed, bool SecondaryOptionActive);
}
