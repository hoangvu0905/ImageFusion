using ImageFusion.Core.Constants;

namespace ImageFusion.Core.Models;

public class AppSettings
{
    public int OutputWidth { get; set; } = AppConstants.DefaultOutputWidth;
    public int OutputHeight { get; set; } = AppConstants.DefaultOutputHeight;
    public int BorderPixel { get; set; } = AppConstants.DefaultBorderPixel;
    public string BorderColorHex { get; set; } = AppConstants.DefaultBorderColorHex;
    public int SplitPixel { get; set; } = AppConstants.DefaultSplitPixel;
    public string SplitColorHex { get; set; } = AppConstants.DefaultSplitColorHex;
    public MergeLayoutType MergeLayout { get; set; } = MergeLayoutType.Grid;
    public ImageFormatType ImageFormat { get; set; } = ImageFormatType.Png;
}
