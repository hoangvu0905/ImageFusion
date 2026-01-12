using System.Windows.Input;
using ImageFusion.Constants;

namespace ImageFusion.ViewModels;

/// <summary>
/// ViewModel for the Main page containing header and tab functionality.
/// </summary>
public class MainViewModel : BaseViewModel
{
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
    
    public int SelectedTabIndex
    {
        get => _selectedTabIndex;
        set => SetProperty(ref _selectedTabIndex, value);
    }
    
    public ICommand ExportCommand { get; }
    public ICommand AddCommand { get; }
    
    public MainViewModel(
        PreviewViewModel previewViewModel,
        OriginalViewModel originalViewModel,
        SettingsViewModel settingsViewModel)
    {
        PreviewViewModel = previewViewModel;
        OriginalViewModel = originalViewModel;
        SettingsViewModel = settingsViewModel;
        
        ExportCommand = new RelayCommand(OnExport);
        AddCommand = new RelayCommand(OnAdd);
    }
    
    private void OnExport()
    {
        // Export functionality to be implemented
    }
    
    private void OnAdd()
    {
        // Add functionality to be implemented
    }
}
