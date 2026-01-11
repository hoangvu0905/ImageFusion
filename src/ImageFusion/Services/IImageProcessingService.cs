using ImageFusion.Core.Constants;
using ImageFusion.Core.Models;
using ImageFusion.Core.Services;
using ImageFusion.Models;
using SkiaSharp;

namespace ImageFusion.Services;

public interface IImageProcessingService
{
    SKBitmap ProcessImages(
        IList<ImageItem> images,
        int outputWidth,
        int outputHeight,
        int borderPixel,
        SKColor borderColor,
        int splitPixel,
        SKColor splitColor,
        MergeLayoutType layoutType);
    
    byte[] EncodeBitmap(SKBitmap bitmap, ImageFormatType format);
    
    SKBitmap? LoadBitmapFromBytes(byte[] data);
}
