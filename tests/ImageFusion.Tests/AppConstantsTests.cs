using ImageFusion.Core.Constants;

namespace ImageFusion.Tests;

public class AppConstantsTests
{
    [Fact]
    public void ApplicationTitle_IsImageFusion()
    {
        Assert.Equal("ImageFusion", AppConstants.ApplicationTitle);
    }
    
    [Fact]
    public void DefaultOutputDimensions_AreReasonable()
    {
        Assert.Equal(1920, AppConstants.DefaultOutputWidth);
        Assert.Equal(1080, AppConstants.DefaultOutputHeight);
    }
    
    [Fact]
    public void DefaultBorderPixel_IsZero()
    {
        Assert.Equal(0, AppConstants.DefaultBorderPixel);
    }
    
    [Fact]
    public void DefaultSplitPixel_IsTen()
    {
        Assert.Equal(10, AppConstants.DefaultSplitPixel);
    }
    
    [Fact]
    public void DefaultColors_AreWhite()
    {
        Assert.Equal("#FFFFFF", AppConstants.DefaultBorderColorHex);
        Assert.Equal("#FFFFFF", AppConstants.DefaultSplitColorHex);
    }
    
    [Fact]
    public void MinImagesForLayoutChoice_IsThree()
    {
        Assert.Equal(3, AppConstants.MinImagesForLayoutChoice);
    }
    
    [Fact]
    public void TwoImageLayout_IsTwo()
    {
        Assert.Equal(2, AppConstants.TwoImageLayout);
    }
    
    [Fact]
    public void MainImagePercentInFeatureLayout_Is65Percent()
    {
        Assert.Equal(0.65, AppConstants.MainImagePercentInFeatureLayout);
    }
    
    [Fact]
    public void JpegQuality_Is90()
    {
        Assert.Equal(90, AppConstants.DefaultJpegQuality);
    }
}
