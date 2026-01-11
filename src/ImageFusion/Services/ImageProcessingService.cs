using ImageFusion.Core.Constants;
using ImageFusion.Core.Models;
using ImageFusion.Models;
using SkiaSharp;
using CoreImageProcessingService = ImageFusion.Core.Services.IImageProcessingService;

namespace ImageFusion.Services;

public class ImageProcessingService : IImageProcessingService
{
    private readonly CoreImageProcessingService _coreService;
    
    public ImageProcessingService(CoreImageProcessingService coreService)
    {
        _coreService = coreService;
    }
    
    public SKBitmap ProcessImages(
        IList<ImageItem> images,
        int outputWidth,
        int outputHeight,
        int borderPixel,
        SKColor borderColor,
        int splitPixel,
        SKColor splitColor,
        MergeLayoutType layoutType)
    {
        var coreImages = images.Select(i => new ImageData
        {
            Id = i.Id,
            Data = i.ImageData,
            Order = i.Order,
            OriginalWidth = i.OriginalWidth,
            OriginalHeight = i.OriginalHeight
        }).ToList();
        
        return _coreService.ProcessImages(
            coreImages,
            outputWidth,
            outputHeight,
            borderPixel,
            borderColor,
            splitPixel,
            splitColor,
            layoutType);
    }
    
    public byte[] EncodeBitmap(SKBitmap bitmap, ImageFormatType format)
    {
        return _coreService.EncodeBitmap(bitmap, format);
    }
    
    public SKBitmap? LoadBitmapFromBytes(byte[] data)
    {
        return _coreService.LoadBitmapFromBytes(data);
    }
}