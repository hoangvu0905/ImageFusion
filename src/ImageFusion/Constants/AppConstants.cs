namespace ImageFusion.Constants;

/// <summary>
/// Application-wide constants to avoid magic strings.
/// </summary>
public static class AppConstants
{
    public const string ApplicationTitle = "Image Fusion";
    
    public static class TabTitles
    {
        public const string Preview = "Preview";
        public const string Original = "Original";
        public const string Settings = "Settings";
    }
    
    public static class ButtonLabels
    {
        public const string Export = "Export";
        public const string Add = "Add";
    }
    
    public static class SettingLabels
    {
        public const string ImageDimensions = "Image Dimensions";
        public const string Height = "Height";
        public const string Width = "Width";
        public const string Border = "Border";
        public const string BorderPixel = "Border Pixel";
        public const string BorderColor = "Border Color";
        public const string CombineType = "Combine Type";
        public const string Split = "Split";
        public const string SplitPixel = "Split Pixel";
        public const string SplitColor = "Split Color";
        public const string ImageFormat = "Image Format";
    }
    
    public static class CombineTypes
    {
        public const string Horizontal = "Horizontal";
        public const string Vertical = "Vertical";
        public const string Grid = "Grid";
    }
    
    public static class ImageFormats
    {
        public const string Png = "PNG";
        public const string Jpeg = "JPEG";
        public const string Bmp = "BMP";
        public const string Gif = "GIF";
    }
    
    public static class DefaultSettings
    {
        public const int DefaultHeight = 1080;
        public const int DefaultWidth = 1920;
        public const int DefaultBorderPixel = 0;
        public const int DefaultSplitPixel = 0;
        public const string DefaultBorderColor = "#000000";
        public const string DefaultSplitColor = "#000000";
        public const string DefaultCombineType = CombineTypes.Horizontal;
        public const string DefaultImageFormat = ImageFormats.Png;
    }
}
