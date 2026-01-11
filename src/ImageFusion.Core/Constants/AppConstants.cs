namespace ImageFusion.Core.Constants;

public static class AppConstants
{
    public const string ApplicationTitle = "ImageFusion";
    
    public const int DefaultOutputWidth = DefaultValues.OutputWidth;
    public const int DefaultOutputHeight = DefaultValues.OutputHeight;
    public const int DefaultBorderPixel = DefaultValues.BorderPixel;
    public const int DefaultSplitPixel = DefaultValues.SplitPixel;
    public const string DefaultBorderColorHex = DefaultValues.BorderColorHex;
    public const string DefaultSplitColorHex = DefaultValues.SplitColorHex;
    public const int DefaultJpegQuality = DefaultValues.JpegQuality;
    public const int DefaultImagesPerOutput = DefaultValues.ImagesPerOutput;
    
    public const int MinOutputDimension = Limits.MinOutputDimension;
    public const int MaxOutputDimension = Limits.MaxOutputDimension;
    public const int MaxBorderPixel = Limits.MaxBorderPixel;
    public const int MaxSplitPixel = Limits.MaxSplitPixel;
    public const int MinImagesPerOutput = Limits.MinImagesPerOutput;
    public const int MaxImagesPerOutput = Limits.MaxImagesPerOutput;
    
    public const double ImageThumbnailMaxWidthPercent = LayoutConstants.ImageThumbnailMaxWidthPercent;
    public const double ImageThumbnailMaxHeightPercent = LayoutConstants.ImageThumbnailMaxHeightPercent;
    public const double MainImagePercentInFeatureLayout = LayoutConstants.MainImagePercentInFeatureLayout;
    public const int MinImagesForLayoutChoice = LayoutConstants.MinImagesForLayoutChoice;
    public const int TwoImageLayout = LayoutConstants.TwoImageLayout;
    
    public const string SettingsFileName = FileConstants.SettingsFileName;
    public const string PngFormat = FileConstants.PngFormat;
    public const string JpegFormat = FileConstants.JpegFormat;
    public const string WebpFormat = FileConstants.WebpFormat;
}
