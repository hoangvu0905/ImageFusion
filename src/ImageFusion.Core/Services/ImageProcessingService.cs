using ImageFusion.Core.Constants;
using ImageFusion.Core.Models;
using SkiaSharp;

namespace ImageFusion.Core.Services;

public class ImageProcessingService : IImageProcessingService
{
    public SKBitmap ProcessImages(
        IList<ImageData> images,
        int outputWidth,
        int outputHeight,
        int borderPixel,
        SKColor borderColor,
        int splitPixel,
        SKColor splitColor,
        MergeLayoutType layoutType)
    {
        if (images.Count == 0)
        {
            return CreateEmptyBitmap(outputWidth, outputHeight, borderColor);
        }
        
        var contentWidth = outputWidth - (borderPixel * 2);
        var contentHeight = outputHeight - (borderPixel * 2);
        
        var mergedBitmap = MergeImages(images, contentWidth, contentHeight, splitPixel, splitColor, layoutType);
        var resultBitmap = AddBorder(mergedBitmap, outputWidth, outputHeight, borderPixel, borderColor);
        
        mergedBitmap.Dispose();
        
        return resultBitmap;
    }
    
    public byte[] EncodeBitmap(SKBitmap bitmap, ImageFormatType format)
    {
        var encodedFormat = format switch
        {
            ImageFormatType.Jpeg => SKEncodedImageFormat.Jpeg,
            ImageFormatType.Webp => SKEncodedImageFormat.Webp,
            _ => SKEncodedImageFormat.Png
        };
        
        var quality = format == ImageFormatType.Jpeg ? AppConstants.DefaultJpegQuality : 100;
        
        using var image = SKImage.FromBitmap(bitmap);
        using var data = image.Encode(encodedFormat, quality);
        
        return data.ToArray();
    }
    
    public SKBitmap? LoadBitmapFromBytes(byte[] data)
    {
        try
        {
            return SKBitmap.Decode(data);
        }
        catch
        {
            return null;
        }
    }
    
    private static SKBitmap CreateEmptyBitmap(int width, int height, SKColor backgroundColor)
    {
        var bitmap = new SKBitmap(width, height);
        using var canvas = new SKCanvas(bitmap);
        canvas.Clear(backgroundColor);
        return bitmap;
    }
    
    private SKBitmap MergeImages(
        IList<ImageData> images,
        int contentWidth,
        int contentHeight,
        int splitPixel,
        SKColor splitColor,
        MergeLayoutType layoutType)
    {
        var bitmap = new SKBitmap(contentWidth, contentHeight);
        using var canvas = new SKCanvas(bitmap);
        canvas.Clear(splitColor);
        
        if (images.Count == 1)
        {
            DrawSingleImage(canvas, images[0], contentWidth, contentHeight);
        }
        else if (images.Count == AppConstants.TwoImageLayout)
        {
            DrawTwoImagesVertical(canvas, images, contentWidth, contentHeight, splitPixel);
        }
        else
        {
            if (layoutType == MergeLayoutType.FeatureMain)
            {
                DrawFeatureLayout(canvas, images, contentWidth, contentHeight, splitPixel);
            }
            else
            {
                DrawGridLayout(canvas, images, contentWidth, contentHeight, splitPixel);
            }
        }
        
        return bitmap;
    }
    
    private void DrawSingleImage(SKCanvas canvas, ImageData image, int width, int height)
    {
        using var sourceBitmap = LoadBitmapFromBytes(image.Data);
        if (sourceBitmap == null) return;
        
        var resizedBitmap = ResizeToFit(sourceBitmap, width, height);
        var x = (width - resizedBitmap.Width) / 2;
        var y = (height - resizedBitmap.Height) / 2;
        
        canvas.DrawBitmap(resizedBitmap, x, y);
        resizedBitmap.Dispose();
    }
    
    private void DrawTwoImagesVertical(
        SKCanvas canvas,
        IList<ImageData> images,
        int contentWidth,
        int contentHeight,
        int splitPixel)
    {
        var halfSplit = splitPixel / 2;
        var cellHeight = (contentHeight - splitPixel) / 2;
        
        for (var i = 0; i < 2 && i < images.Count; i++)
        {
            using var sourceBitmap = LoadBitmapFromBytes(images[i].Data);
            if (sourceBitmap == null) continue;
            
            var resizedBitmap = ResizeToFit(sourceBitmap, contentWidth, cellHeight);
            var x = (contentWidth - resizedBitmap.Width) / 2;
            var y = i * (cellHeight + splitPixel) + (cellHeight - resizedBitmap.Height) / 2;
            
            canvas.DrawBitmap(resizedBitmap, x, y);
            resizedBitmap.Dispose();
        }
    }
    
    private void DrawGridLayout(
        SKCanvas canvas,
        IList<ImageData> images,
        int contentWidth,
        int contentHeight,
        int splitPixel)
    {
        var count = images.Count;
        var cols = (int)Math.Ceiling(Math.Sqrt(count));
        var rows = (int)Math.Ceiling((double)count / cols);
        
        var cellWidth = (contentWidth - (cols - 1) * splitPixel) / cols;
        var cellHeight = (contentHeight - (rows - 1) * splitPixel) / rows;
        
        for (var i = 0; i < count; i++)
        {
            var col = i % cols;
            var row = i / cols;
            
            using var sourceBitmap = LoadBitmapFromBytes(images[i].Data);
            if (sourceBitmap == null) continue;
            
            var resizedBitmap = ResizeToFit(sourceBitmap, cellWidth, cellHeight);
            var x = col * (cellWidth + splitPixel) + (cellWidth - resizedBitmap.Width) / 2;
            var y = row * (cellHeight + splitPixel) + (cellHeight - resizedBitmap.Height) / 2;
            
            canvas.DrawBitmap(resizedBitmap, x, y);
            resizedBitmap.Dispose();
        }
    }
    
    private void DrawFeatureLayout(
        SKCanvas canvas,
        IList<ImageData> images,
        int contentWidth,
        int contentHeight,
        int splitPixel)
    {
        var mainWidth = (int)(contentWidth * AppConstants.MainImagePercentInFeatureLayout);
        var mainHeight = (int)(contentHeight * AppConstants.MainImagePercentInFeatureLayout);
        
        using var mainBitmap = LoadBitmapFromBytes(images[0].Data);
        if (mainBitmap != null)
        {
            var resizedMain = ResizeToFit(mainBitmap, mainWidth - splitPixel, mainHeight - splitPixel);
            var mainX = (mainWidth - splitPixel - resizedMain.Width) / 2;
            var mainY = (mainHeight - splitPixel - resizedMain.Height) / 2;
            canvas.DrawBitmap(resizedMain, mainX, mainY);
            resizedMain.Dispose();
        }
        
        var rightWidth = contentWidth - mainWidth;
        var remainingImages = images.Skip(1).ToList();
        
        if (remainingImages.Count == 0) return;
        
        var rightCellHeight = (mainHeight - splitPixel) / Math.Min(remainingImages.Count, 3);
        
        for (var i = 0; i < Math.Min(remainingImages.Count, 3); i++)
        {
            using var sourceBitmap = LoadBitmapFromBytes(remainingImages[i].Data);
            if (sourceBitmap == null) continue;
            
            var resized = ResizeToFit(sourceBitmap, rightWidth - splitPixel, rightCellHeight - splitPixel);
            var x = mainWidth + (rightWidth - resized.Width) / 2;
            var y = i * rightCellHeight + (rightCellHeight - resized.Height) / 2;
            
            canvas.DrawBitmap(resized, x, y);
            resized.Dispose();
        }
        
        if (remainingImages.Count > 3)
        {
            var bottomHeight = contentHeight - mainHeight;
            var bottomImages = remainingImages.Skip(3).ToList();
            var bottomCellWidth = (contentWidth - splitPixel) / Math.Min(bottomImages.Count, 4);
            
            for (var i = 0; i < Math.Min(bottomImages.Count, 4); i++)
            {
                using var sourceBitmap = LoadBitmapFromBytes(bottomImages[i].Data);
                if (sourceBitmap == null) continue;
                
                var resized = ResizeToFit(sourceBitmap, bottomCellWidth - splitPixel, bottomHeight - splitPixel);
                var x = i * bottomCellWidth + (bottomCellWidth - resized.Width) / 2;
                var y = mainHeight + (bottomHeight - resized.Height) / 2;
                
                canvas.DrawBitmap(resized, x, y);
                resized.Dispose();
            }
        }
    }
    
    private static SKBitmap ResizeToFit(SKBitmap source, int maxWidth, int maxHeight)
    {
        var ratioX = (double)maxWidth / source.Width;
        var ratioY = (double)maxHeight / source.Height;
        var ratio = Math.Min(ratioX, ratioY);
        
        var newWidth = (int)(source.Width * ratio);
        var newHeight = (int)(source.Height * ratio);
        
        if (newWidth <= 0) newWidth = 1;
        if (newHeight <= 0) newHeight = 1;
        
        var resized = new SKBitmap(newWidth, newHeight);
        source.ScalePixels(resized, new SKSamplingOptions(SKFilterMode.Linear, SKMipmapMode.Linear));
        
        return resized;
    }
    
    private static SKBitmap AddBorder(SKBitmap content, int totalWidth, int totalHeight, int borderPixel, SKColor borderColor)
    {
        var result = new SKBitmap(totalWidth, totalHeight);
        using var canvas = new SKCanvas(result);
        
        canvas.Clear(borderColor);
        canvas.DrawBitmap(content, borderPixel, borderPixel);
        
        return result;
    }
}
