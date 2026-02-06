using ImageFusion.Constants;
using ImageFusion.Models;

namespace ImageFusion.Services;

/// <summary>
/// Implementation of ISettingsService for managing application settings.
/// </summary>
public class SettingsService : ISettingsService
{
    private AppSettings _settings = new();
    
    private readonly IReadOnlyList<string> _combineTypes = new List<string>
    {
        AppConstants.CombineTypes.Horizontal,
        AppConstants.CombineTypes.Vertical,
        AppConstants.CombineTypes.Grid
    };
    
    private readonly IReadOnlyList<string> _imageFormats = new List<string>
    {
        AppConstants.ImageFormats.Png,
        AppConstants.ImageFormats.Jpeg,
        AppConstants.ImageFormats.Bmp,
        AppConstants.ImageFormats.Gif
    };
    
    public AppSettings GetSettings()
    {
        // Return a copy to avoid exposing internal state
        return new AppSettings
        {
            Height = _settings.Height,
            Width = _settings.Width,
            BorderPixel = _settings.BorderPixel,
            BorderColor = _settings.BorderColor,
            CombineType = _settings.CombineType,
            SplitPixel = _settings.SplitPixel,
            SplitColor = _settings.SplitColor,
            ImageFormat = _settings.ImageFormat
        };
    }
    
    public void SaveSettings(AppSettings settings)
    {
        // Store a copy to maintain encapsulation
        _settings = new AppSettings
        {
            Height = settings.Height,
            Width = settings.Width,
            BorderPixel = settings.BorderPixel,
            BorderColor = settings.BorderColor,
            CombineType = settings.CombineType,
            SplitPixel = settings.SplitPixel,
            SplitColor = settings.SplitColor,
            ImageFormat = settings.ImageFormat
        };
    }
    
    public IReadOnlyList<string> GetCombineTypes()
    {
        return _combineTypes;
    }
    
    public IReadOnlyList<string> GetImageFormats()
    {
        return _imageFormats;
    }
}
