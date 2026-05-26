using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DJORGA.Desktop.ViewModels;
using DJORGA.Domain.Entities;

namespace DJORGA.Desktop.Views.Dialogs
{
    public partial class EditTrackDialog : Window
    {
        public EditTrackDialog()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        protected override void OnDataContextChanged(System.EventArgs e)
        {
            base.OnDataContextChanged(e);
            if (DataContext is EditTrackViewModel vm)
            {
                vm.CloseRequested += result => Close(result);
            }
        }
    }
}
