using ImageFusion.ViewModels;

namespace ImageFusion.Views;

public partial class PreviewView : ContentView
{
    public PreviewView(PreviewViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
