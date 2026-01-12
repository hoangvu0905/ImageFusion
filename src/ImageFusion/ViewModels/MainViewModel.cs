using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ImageFusion.Constants;

namespace ImageFusion.ViewModels;

/// <summary>
/// ViewModel for the Main page containing header and tab functionality.
/// </summary>
public partial class MainViewModel : BaseViewModel
{
    [ObservableProperty]
    private int _selectedTabIndex = 1; // Default to Original tab (index 1)
    
    public string ApplicationTitle => AppConstants.ApplicationTitle;
    public string ExportButtonLabel => AppConstants.ButtonLabels.Export;
    public string AddButtonLabel => AppConstants.ButtonLabels.Add;
    
    public string PreviewTabTitle => AppConstants.TabTitles.Preview;
    public string OriginalTabTitle => AppConstants.TabTitles.Original;
    public string SettingsTabTitle => AppConstants.TabTitles.Settings;
    
    public PreviewViewModel PreviewViewModel { get; }
    public OriginalViewModel OriginalViewModel { get; }
    public SettingsViewModel SettingsViewModel { get; }
    
    public MainViewModel(
        PreviewViewModel previewViewModel,
        OriginalViewModel originalViewModel,
        SettingsViewModel settingsViewModel)
    {
        PreviewViewModel = previewViewModel;
        OriginalViewModel = originalViewModel;
        SettingsViewModel = settingsViewModel;
    }
    
    [RelayCommand]
    private void Export()
    {
        // Export functionality to be implemented
    }
    
    [RelayCommand]
    private void Add()
    {
        // Add functionality to be implemented
    }
}
