using ImageFusion.ViewModels;

namespace ImageFusion.Views.Tabs;

public partial class OriginalTabView : ContentView
{
    public OriginalTabView()
    {
        InitializeComponent();
    }
    
    private void OnReorderCompleted(object? sender, EventArgs e)
    {
        if (BindingContext is MainViewModel viewModel)
        {
            viewModel.UpdateImageOrdersAfterReorder();
        }
    }
}
