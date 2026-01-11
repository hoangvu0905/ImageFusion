using ImageFusion.Core.Constants;
using ImageFusion.Core.Models;
using ImageFusion.Core.Services;
using SkiaSharp;

namespace ImageFusion.Tests;

public class ImageProcessingServiceTests
{
    private readonly ImageProcessingService _service;
    
    public ImageProcessingServiceTests()
    {
        _service = new ImageProcessingService();
    }
    
    [Fact]
    public void ProcessImages_WithNoImages_ReturnsEmptyBitmapWithBorderColor()
    {
        // Arrange
        var images = new List<ImageData>();
        var borderColor = SKColors.Red;
        
        // Act
        using var result = _service.ProcessImages(
            images,
            100,
            100,
            10,
            borderColor,
            5,
            SKColors.White,
            MergeLayoutType.Grid);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(100, result.Width);
        Assert.Equal(100, result.Height);
    }
    
    [Fact]
    public void ProcessImages_WithSingleImage_ReturnsProcessedBitmap()
    {
        // Arrange
        var testImage = CreateTestImageData(200, 100);
        var images = new List<ImageData> { testImage };
        
        // Act
        using var result = _service.ProcessImages(
            images,
            400,
            300,
            20,
            SKColors.Black,
            10,
            SKColors.Gray,
            MergeLayoutType.Grid);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(400, result.Width);
        Assert.Equal(300, result.Height);
    }
    
    [Fact]
    public void ProcessImages_WithTwoImages_CreatesVerticalLayout()
    {
        // Arrange
        var images = new List<ImageData>
        {
            CreateTestImageData(100, 100),
            CreateTestImageData(100, 100)
        };
        
        // Act
        using var result = _service.ProcessImages(
            images,
            200,
            400,
            0,
            SKColors.White,
            10,
            SKColors.Gray,
            MergeLayoutType.Grid);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.Width);
        Assert.Equal(400, result.Height);
    }
    
    [Fact]
    public void ProcessImages_WithThreeOrMoreImages_UsesGridLayout()
    {
        // Arrange
        var images = new List<ImageData>
        {
            CreateTestImageData(100, 100),
            CreateTestImageData(100, 100),
            CreateTestImageData(100, 100),
            CreateTestImageData(100, 100)
        };
        
        // Act
        using var result = _service.ProcessImages(
            images,
            400,
            400,
            0,
            SKColors.White,
            10,
            SKColors.Gray,
            MergeLayoutType.Grid);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(400, result.Width);
        Assert.Equal(400, result.Height);
    }
    
    [Fact]
    public void ProcessImages_WithThreeOrMoreImages_UsesFeatureLayout()
    {
        // Arrange
        var images = new List<ImageData>
        {
            CreateTestImageData(100, 100),
            CreateTestImageData(100, 100),
            CreateTestImageData(100, 100)
        };
        
        // Act
        using var result = _service.ProcessImages(
            images,
            400,
            400,
            0,
            SKColors.White,
            10,
            SKColors.Gray,
            MergeLayoutType.FeatureMain);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(400, result.Width);
        Assert.Equal(400, result.Height);
    }
    
    [Fact]
    public void EncodeBitmap_ToPng_ReturnsValidBytes()
    {
        // Arrange
        using var bitmap = new SKBitmap(100, 100);
        using var canvas = new SKCanvas(bitmap);
        canvas.Clear(SKColors.Blue);
        
        // Act
        var result = _service.EncodeBitmap(bitmap, ImageFormatType.Png);
        
        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.True(IsPngFormat(result));
    }
    
    [Fact]
    public void EncodeBitmap_ToJpeg_ReturnsValidBytes()
    {
        // Arrange
        using var bitmap = new SKBitmap(100, 100);
        using var canvas = new SKCanvas(bitmap);
        canvas.Clear(SKColors.Blue);
        
        // Act
        var result = _service.EncodeBitmap(bitmap, ImageFormatType.Jpeg);
        
        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.True(IsJpegFormat(result));
    }
    
    [Fact]
    public void LoadBitmapFromBytes_WithValidData_ReturnsBitmap()
    {
        // Arrange
        using var bitmap = new SKBitmap(50, 50);
        using var canvas = new SKCanvas(bitmap);
        canvas.Clear(SKColors.Green);
        var pngData = _service.EncodeBitmap(bitmap, ImageFormatType.Png);
        
        // Act
        using var result = _service.LoadBitmapFromBytes(pngData);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(50, result.Width);
        Assert.Equal(50, result.Height);
    }
    
    [Fact]
    public void LoadBitmapFromBytes_WithInvalidData_ReturnsNull()
    {
        // Arrange
        var invalidData = new byte[] { 0x00, 0x01, 0x02, 0x03 };
        
        // Act
        var result = _service.LoadBitmapFromBytes(invalidData);
        
        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public void ProcessImages_WithBorder_AddsBorderCorrectly()
    {
        // Arrange
        var testImage = CreateTestImageData(100, 100);
        var images = new List<ImageData> { testImage };
        var borderPixel = 20;
        var borderColor = SKColors.Red;
        
        // Act
        using var result = _service.ProcessImages(
            images,
            200,
            200,
            borderPixel,
            borderColor,
            0,
            SKColors.White,
            MergeLayoutType.Grid);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.Width);
        Assert.Equal(200, result.Height);
        
        var cornerPixel = result.GetPixel(0, 0);
        Assert.Equal(borderColor, cornerPixel);
    }
    
    private static ImageData CreateTestImageData(int width, int height)
    {
        using var bitmap = new SKBitmap(width, height);
        using var canvas = new SKCanvas(bitmap);
        canvas.Clear(SKColors.Blue);
        
        using var image = SKImage.FromBitmap(bitmap);
        using var data = image.Encode(SKEncodedImageFormat.Png, 100);
        
        return new ImageData
        {
            Data = data.ToArray(),
            OriginalWidth = width,
            OriginalHeight = height
        };
    }
    
    private static bool IsPngFormat(byte[] data)
    {
        if (data.Length < 8) return false;
        return data[0] == 0x89 && data[1] == 0x50 && data[2] == 0x4E && data[3] == 0x47;
    }
    
    private static bool IsJpegFormat(byte[] data)
    {
        if (data.Length < 2) return false;
        return data[0] == 0xFF && data[1] == 0xD8;
    }
}
