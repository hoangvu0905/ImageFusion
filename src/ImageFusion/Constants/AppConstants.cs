using ImageFusion.Resources.Strings;

namespace ImageFusion.Constants;

/// <summary>
/// Application-wide constants for default settings and configuration values.
/// Text strings are stored in Resources/Strings/AppResources.resx for localization support.
/// </summary>
public static class AppConstants
{
    public static class CombineTypes
    {
        public static string Horizontal => AppResources.CombineTypeHorizontal;
        public static string Vertical => AppResources.CombineTypeVertical;
        public static string Grid => AppResources.CombineTypeGrid;
    }
    
    public static class ImageFormats
    {
        public static string Png => AppResources.ImageFormatPng;
        public static string Jpeg => AppResources.ImageFormatJpeg;
        public static string Bmp => AppResources.ImageFormatBmp;
        public static string Gif => AppResources.ImageFormatGif;
    }
    
    public static class DefaultSettings
    {
        public const int DefaultHeight = 1080;
        public const int DefaultWidth = 1920;
        public const int DefaultBorderPixel = 0;
        public const int DefaultSplitPixel = 0;
        public const string DefaultBorderColor = "#000000";
        public const string DefaultSplitColor = "#000000";
        public static string DefaultCombineType => CombineTypes.Horizontal;
        public static string DefaultImageFormat => ImageFormats.Png;
    }
}
