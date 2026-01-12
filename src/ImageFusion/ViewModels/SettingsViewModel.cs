using System.Collections.ObjectModel;
using System.Windows.Input;
using ImageFusion.Constants;
using ImageFusion.Models;
using ImageFusion.Services;

namespace ImageFusion.ViewModels;

/// <summary>
/// ViewModel for the Settings tab.
/// </summary>
public class SettingsViewModel : BaseViewModel
{
    private readonly ISettingsService _settingsService;
    
    private int _height;
    private int _width;
    private int _borderPixel;
    private Color _borderColor;
    private string _selectedCombineType;
    private int _splitPixel;
    private Color _splitColor;
    private string _selectedImageFormat;
    
    public ObservableCollection<string> CombineTypes { get; }
    public ObservableCollection<string> ImageFormats { get; }
    
    public int Height
    {
        get => _height;
        set => SetProperty(ref _height, value);
    }
    
    public int Width
    {
        get => _width;
        set => SetProperty(ref _width, value);
    }
    
    public int BorderPixel
    {
        get => _borderPixel;
        set => SetProperty(ref _borderPixel, value);
    }
    
    public Color BorderColor
    {
        get => _borderColor;
        set => SetProperty(ref _borderColor, value);
    }
    
    public string SelectedCombineType
    {
        get => _selectedCombineType;
        set => SetProperty(ref _selectedCombineType, value);
    }
    
    public int SplitPixel
    {
        get => _splitPixel;
        set => SetProperty(ref _splitPixel, value);
    }
    
    public Color SplitColor
    {
        get => _splitColor;
        set => SetProperty(ref _splitColor, value);
    }
    
    public string SelectedImageFormat
    {
        get => _selectedImageFormat;
        set => SetProperty(ref _selectedImageFormat, value);
    }
    
    public SettingsViewModel(ISettingsService settingsService)
    {
        _settingsService = settingsService;
        
        CombineTypes = new ObservableCollection<string>(_settingsService.GetCombineTypes());
        ImageFormats = new ObservableCollection<string>(_settingsService.GetImageFormats());
        
        _selectedCombineType = AppConstants.DefaultSettings.DefaultCombineType;
        _selectedImageFormat = AppConstants.DefaultSettings.DefaultImageFormat;
        
        LoadSettings();
    }
    
    private void LoadSettings()
    {
        var settings = _settingsService.GetSettings();
        
        Height = settings.Height;
        Width = settings.Width;
        BorderPixel = settings.BorderPixel;
        BorderColor = Color.FromArgb(settings.BorderColor);
        SelectedCombineType = settings.CombineType;
        SplitPixel = settings.SplitPixel;
        SplitColor = Color.FromArgb(settings.SplitColor);
        SelectedImageFormat = settings.ImageFormat;
    }
    
    public void SaveSettings()
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
