using ImageFusion.Core.Constants;
using ImageFusion.Core.Models;

namespace ImageFusion.Tests;

public class AppSettingsTests
{
    [Fact]
    public void DefaultValues_AreCorrect()
    {
        // Arrange & Act
        var settings = new AppSettings();
        
        // Assert
        Assert.Equal(AppConstants.DefaultOutputWidth, settings.OutputWidth);
        Assert.Equal(AppConstants.DefaultOutputHeight, settings.OutputHeight);
        Assert.Equal(AppConstants.DefaultBorderPixel, settings.BorderPixel);
        Assert.Equal(AppConstants.DefaultBorderColorHex, settings.BorderColorHex);
        Assert.Equal(AppConstants.DefaultSplitPixel, settings.SplitPixel);
        Assert.Equal(AppConstants.DefaultSplitColorHex, settings.SplitColorHex);
        Assert.Equal(MergeLayoutType.Grid, settings.MergeLayout);
        Assert.Equal(ImageFormatType.Png, settings.ImageFormat);
    }
    
    [Fact]
    public void Properties_CanBeModified()
    {
        // Arrange
        var settings = new AppSettings();
        
        // Act
        settings.OutputWidth = 800;
        settings.OutputHeight = 600;
        settings.BorderPixel = 10;
        settings.BorderColorHex = "#000000";
        settings.SplitPixel = 5;
        settings.SplitColorHex = "#CCCCCC";
        settings.MergeLayout = MergeLayoutType.FeatureMain;
        settings.ImageFormat = ImageFormatType.Jpeg;
        
        // Assert
        Assert.Equal(800, settings.OutputWidth);
        Assert.Equal(600, settings.OutputHeight);
        Assert.Equal(10, settings.BorderPixel);
        Assert.Equal("#000000", settings.BorderColorHex);
        Assert.Equal(5, settings.SplitPixel);
        Assert.Equal("#CCCCCC", settings.SplitColorHex);
        Assert.Equal(MergeLayoutType.FeatureMain, settings.MergeLayout);
        Assert.Equal(ImageFormatType.Jpeg, settings.ImageFormat);
    }
}
