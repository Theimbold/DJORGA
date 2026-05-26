using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DJORGA.Desktop.ViewModels;

namespace DJORGA.Desktop.Views
{
    public partial class AIBuilderView : UserControl
    {
        public AIBuilderView()
        {
            InitializeComponent();
        }

        protected override void OnAttachedToVisualTree(Avalonia.VisualTreeAttachmentEventArgs e)
        {
            base.OnAttachedToVisualTree(e);
            
            if (DataContext is AIBuilderViewModel vm)
            {
                vm.LoadTracksCommand.Execute(null);
            }
        }
    }
}
