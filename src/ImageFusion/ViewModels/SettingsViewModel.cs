using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using ImageFusion.Constants;
using ImageFusion.Models;
using ImageFusion.Services;

namespace ImageFusion.ViewModels;

/// <summary>
/// ViewModel for the Settings tab.
/// </summary>
public partial class SettingsViewModel : BaseViewModel
{
    private readonly ISettingsService _settingsService;
    private bool _isLoading;
    
    [ObservableProperty]
    private int _height;
    
    [ObservableProperty]
    private int _width;
    
    [ObservableProperty]
    private int _borderPixel;
    
    [ObservableProperty]
    private Color _borderColor;
    
    [ObservableProperty]
    private string _selectedCombineType = AppConstants.DefaultSettings.DefaultCombineType;
    
    [ObservableProperty]
    private int _splitPixel;
    
    [ObservableProperty]
    private Color _splitColor;
    
    [ObservableProperty]
    private string _selectedImageFormat = AppConstants.DefaultSettings.DefaultImageFormat;
    
    public ObservableCollection<string> CombineTypes { get; }
    public ObservableCollection<string> ImageFormats { get; }
    
    public SettingsViewModel(ISettingsService settingsService)
    {
        _settingsService = settingsService;
        
        CombineTypes = new ObservableCollection<string>(_settingsService.GetCombineTypes());
        ImageFormats = new ObservableCollection<string>(_settingsService.GetImageFormats());
        
        LoadSettings();
    }
    
    partial void OnHeightChanged(int value) => TrySaveSettings();
    partial void OnWidthChanged(int value) => TrySaveSettings();
    partial void OnBorderPixelChanged(int value) => TrySaveSettings();
    partial void OnBorderColorChanged(Color value) => TrySaveSettings();
    partial void OnSelectedCombineTypeChanged(string value) => TrySaveSettings();
    partial void OnSplitPixelChanged(int value) => TrySaveSettings();
    partial void OnSplitColorChanged(Color value) => TrySaveSettings();
    partial void OnSelectedImageFormatChanged(string value) => TrySaveSettings();
    
    private void TrySaveSettings()
    {
        if (!_isLoading)
        {
            SaveSettings();
        }
    }
    
    private void LoadSettings()
    {
        _isLoading = true;
        
        var settings = _settingsService.GetSettings();
        
        Height = settings.Height;
        Width = settings.Width;
        BorderPixel = settings.BorderPixel;
        BorderColor = Color.FromArgb(settings.BorderColor);
        SelectedCombineType = settings.CombineType;
        SplitPixel = settings.SplitPixel;
        SplitColor = Color.FromArgb(settings.SplitColor);
        SelectedImageFormat = settings.ImageFormat;
        
        _isLoading = false;
    }
    
    private void SaveSettings()
    {
        var settings = new AppSettings
        {
            Height = Height,
            Width = Width,
            BorderPixel = BorderPixel,
            BorderColor = BorderColor.ToArgbHex(),
            CombineType = SelectedCombineType,
            SplitPixel = SplitPixel,
            SplitColor = SplitColor.ToArgbHex(),
            ImageFormat = SelectedImageFormat
        };
        
        _settingsService.SaveSettings(settings);
    }
}
