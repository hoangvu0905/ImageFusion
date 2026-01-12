using ImageFusion.ViewModels;

namespace ImageFusion.Pages;

public partial class PreviewPage : ContentPage
{
    public PreviewPage(PreviewViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
