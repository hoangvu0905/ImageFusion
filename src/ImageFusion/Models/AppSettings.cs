using ImageFusion.Constants;

namespace ImageFusion.Models;

/// <summary>
/// Model representing application settings.
/// </summary>
public class AppSettings
{
    public int Height { get; set; } = AppConstants.DefaultSettings.DefaultHeight;
    
    public int Width { get; set; } = AppConstants.DefaultSettings.DefaultWidth;
    
    public int BorderPixel { get; set; } = AppConstants.DefaultSettings.DefaultBorderPixel;
    
    public string BorderColor { get; set; } = AppConstants.DefaultSettings.DefaultBorderColor;
    
    public string CombineType { get; set; } = AppConstants.DefaultSettings.DefaultCombineType;
    
    public int SplitPixel { get; set; } = AppConstants.DefaultSettings.DefaultSplitPixel;
    
    public string SplitColor { get; set; } = AppConstants.DefaultSettings.DefaultSplitColor;
    
    public string ImageFormat { get; set; } = AppConstants.DefaultSettings.DefaultImageFormat;
}
