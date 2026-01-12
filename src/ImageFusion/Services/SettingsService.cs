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
        return _settings;
    }
    
    public void SaveSettings(AppSettings settings)
    {
        _settings = settings;
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
