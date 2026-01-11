namespace ImageFusion.Core.Constants;

public static class AppConstants
{
    public const string ApplicationTitle = "ImageFusion";
    public const string SettingsFileName = "app_settings.json";
    
    public const int DefaultOutputWidth = 1920;
    public const int DefaultOutputHeight = 1080;
    public const int DefaultBorderPixel = 0;
    public const int DefaultSplitPixel = 10;
    
    public const int MinOutputDimension = 100;
    public const int MaxOutputDimension = 8000;
    public const int MaxBorderPixel = 200;
    public const int MaxSplitPixel = 100;
    
    public const double ImageThumbnailMaxWidthPercent = 0.8;
    public const double ImageThumbnailMaxHeightPercent = 0.8;
    public const double MainImagePercentInFeatureLayout = 0.65;
    
    public const int MinImagesForLayoutChoice = 3;
    public const int TwoImageLayout = 2;
    
    public const string DefaultBorderColorHex = "#FFFFFF";
    public const string DefaultSplitColorHex = "#FFFFFF";
    
    public const string ExportSuccessMessage = "Image exported successfully!";
    public const string ExportFailedMessage = "Failed to export image.";
    public const string NoImagesMessage = "Please add images first.";
    public const string AddImagesButtonText = "Add Images";
    public const string ExportButtonText = "Export";
    
    public const string PngFormat = "PNG";
    public const string JpegFormat = "JPEG";
    public const string WebpFormat = "WEBP";
    
    public const int DefaultJpegQuality = 90;
}
