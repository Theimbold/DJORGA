using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MyApp.Desktop.ViewModels;
using MyApp.Domain.Entities;

namespace MyApp.Desktop.Views.Dialogs
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
