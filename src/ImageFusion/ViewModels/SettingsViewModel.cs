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
    private string _selectedCombineType = AppConstants.DefaultSettings.DefaultCombineType;
    private int _splitPixel;
    private Color _splitColor;
    private string _selectedImageFormat = AppConstants.DefaultSettings.DefaultImageFormat;
    
    public ObservableCollection<string> CombineTypes { get; }
    public ObservableCollection<string> ImageFormats { get; }
    
    public int Height
    {
        get => _height;
        set
        {
            if (SetProperty(ref _height, value))
            {
                SaveSettings();
            }
        }
    }
    
    public int Width
    {
        get => _width;
        set
        {
            if (SetProperty(ref _width, value))
            {
                SaveSettings();
            }
        }
    }
    
    public int BorderPixel
    {
        get => _borderPixel;
        set
        {
            if (SetProperty(ref _borderPixel, value))
            {
                SaveSettings();
            }
        }
    }
    
    public Color BorderColor
    {
        get => _borderColor;
        set
        {
            if (SetProperty(ref _borderColor, value))
            {
                SaveSettings();
            }
        }
    }
    
    public string SelectedCombineType
    {
        get => _selectedCombineType;
        set
        {
            if (SetProperty(ref _selectedCombineType, value))
            {
                SaveSettings();
            }
        }
    }
    
    public int SplitPixel
    {
        get => _splitPixel;
        set
        {
            if (SetProperty(ref _splitPixel, value))
            {
                SaveSettings();
            }
        }
    }
    
    public Color SplitColor
    {
        get => _splitColor;
        set
        {
            if (SetProperty(ref _splitColor, value))
            {
                SaveSettings();
            }
        }
    }
    
    public string SelectedImageFormat
    {
        get => _selectedImageFormat;
        set
        {
            if (SetProperty(ref _selectedImageFormat, value))
            {
                SaveSettings();
            }
        }
    }
    
    public SettingsViewModel(ISettingsService settingsService)
    {
        _settingsService = settingsService;
        
        CombineTypes = new ObservableCollection<string>(_settingsService.GetCombineTypes());
        ImageFormats = new ObservableCollection<string>(_settingsService.GetImageFormats());
        
        LoadSettings();
    }
    
    private void LoadSettings()
    {
        var settings = _settingsService.GetSettings();
        
        // Use backing fields directly to avoid triggering SaveSettings during load
        _height = settings.Height;
        _width = settings.Width;
        _borderPixel = settings.BorderPixel;
        _borderColor = Color.FromArgb(settings.BorderColor);
        _selectedCombineType = settings.CombineType;
        _splitPixel = settings.SplitPixel;
        _splitColor = Color.FromArgb(settings.SplitColor);
        _selectedImageFormat = settings.ImageFormat;
        
        // Notify UI of all changes
        OnPropertyChanged(nameof(Height));
        OnPropertyChanged(nameof(Width));
        OnPropertyChanged(nameof(BorderPixel));
        OnPropertyChanged(nameof(BorderColor));
        OnPropertyChanged(nameof(SelectedCombineType));
        OnPropertyChanged(nameof(SplitPixel));
        OnPropertyChanged(nameof(SplitColor));
        OnPropertyChanged(nameof(SelectedImageFormat));
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
