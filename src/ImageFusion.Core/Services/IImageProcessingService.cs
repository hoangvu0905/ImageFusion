using ImageFusion.Core.Constants;
using ImageFusion.Core.Models;
using SkiaSharp;

namespace ImageFusion.Core.Services;

public interface IImageProcessingService
{
    SKBitmap ProcessImages(
        IList<ImageData> images,
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
