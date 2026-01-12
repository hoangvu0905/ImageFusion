using ImageFusion.ViewModels;

namespace ImageFusion.Views;

public partial class OriginalView : ContentView
{
    public OriginalView(OriginalViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
