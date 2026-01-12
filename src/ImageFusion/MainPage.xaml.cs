using ImageFusion.ViewModels;
using ImageFusion.Views;

namespace ImageFusion;

public partial class MainPage : ContentPage
{
    private readonly MainViewModel _viewModel;
    private readonly PreviewView _previewView;
    private readonly OriginalView _originalView;
    private readonly SettingsView _settingsView;

    public MainPage(
        MainViewModel viewModel,
        PreviewView previewView,
        OriginalView originalView,
        SettingsView settingsView)
    {
        InitializeComponent();
        
        _viewModel = viewModel;
        _previewView = previewView;
        _originalView = originalView;
        _settingsView = settingsView;
        
        BindingContext = _viewModel;
        
        // Set default tab to Original (index 1)
        SelectTab(1);
    }

    private void OnPreviewTabTapped(object? sender, TappedEventArgs e)
    {
        SelectTab(0);
    }

    private void OnOriginalTabTapped(object? sender, TappedEventArgs e)
    {
        SelectTab(1);
    }

    private void OnSettingsTabTapped(object? sender, TappedEventArgs e)
    {
        SelectTab(2);
    }

    private void SelectTab(int tabIndex)
    {
        _viewModel.SelectedTabIndex = tabIndex;
        
        // Reset all tab indicators
        PreviewTabIndicator.Color = Colors.Transparent;
        OriginalTabIndicator.Color = Colors.Transparent;
        SettingsTabIndicator.Color = Colors.Transparent;
        
        PreviewTabLabel.FontAttributes = FontAttributes.None;
        OriginalTabLabel.FontAttributes = FontAttributes.None;
        SettingsTabLabel.FontAttributes = FontAttributes.None;
        
        var primaryColor = (Color)Application.Current!.Resources["Primary"];
        
        switch (tabIndex)
        {
            case 0:
                TabContentContainer.Content = _previewView;
                PreviewTabIndicator.Color = primaryColor;
                PreviewTabLabel.FontAttributes = FontAttributes.Bold;
                break;
            case 1:
                TabContentContainer.Content = _originalView;
                OriginalTabIndicator.Color = primaryColor;
                OriginalTabLabel.FontAttributes = FontAttributes.Bold;
                break;
            case 2:
                TabContentContainer.Content = _settingsView;
                SettingsTabIndicator.Color = primaryColor;
                SettingsTabLabel.FontAttributes = FontAttributes.Bold;
                break;
        }
    }
}