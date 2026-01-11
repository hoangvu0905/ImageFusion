using ImageFusion.ViewModels;

namespace ImageFusion;

public partial class MainPage : ContentPage
{
    private readonly MainViewModel _viewModel;
    
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.InitializeAsync();
    }
    
    private void OnPreviewTabClicked(object? sender, EventArgs e)
    {
        _viewModel.SelectedTabIndex = 0;
    }
    
    private void OnOriginalTabClicked(object? sender, EventArgs e)
    {
        _viewModel.SelectedTabIndex = 1;
    }
    
    private void OnSettingsTabClicked(object? sender, EventArgs e)
    {
        _viewModel.SelectedTabIndex = 2;
    }
    
    private async void OnPreviewImageTapped(object? sender, TappedEventArgs e)
    {
        var previewData = _viewModel.GetCurrentPreviewData();
        if (previewData == null) return;
        
        var previewPage = new Views.ImagePreviewPage(previewData);
        await Navigation.PushModalAsync(previewPage);
    }
}