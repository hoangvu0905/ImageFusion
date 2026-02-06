using ImageFusion.ViewModels;

namespace ImageFusion.Pages;

public partial class OriginalPage : ContentPage
{
    public OriginalPage(OriginalViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
