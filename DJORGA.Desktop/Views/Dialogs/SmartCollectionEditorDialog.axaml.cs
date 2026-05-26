using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DJORGA.Desktop.ViewModels;

namespace DJORGA.Desktop.Views.Dialogs
{
    public partial class SmartCollectionEditorDialog : Window
    {
        public SmartCollectionEditorDialog()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        protected override void OnDataContextChanged(System.EventArgs e)
        {
            base.OnDataContextChanged(e);
            if (DataContext is SmartCollectionEditorViewModel vm)
            {
                vm.CloseRequested += result => Close(result);
            }
        }
    }
}
