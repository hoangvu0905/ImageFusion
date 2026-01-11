using ImageFusion.Resources.Strings;
using ImageFusion.Services;

namespace ImageFusion.Views;

public partial class ImagePreviewPage : ContentPage
{
    private byte[] _imageData;
    private readonly IFileService _fileService;
    
    public ImagePreviewPage(IFileService fileService)
    {
        InitializeComponent();
        _fileService = fileService;
        _imageData = [];
    }
    
    public void SetImageData(byte[] imageData)
    {
        _imageData = imageData;
        PreviewImage.Source = ImageSource.FromStream(() => new MemoryStream(_imageData));
    }
    
    private async void OnCloseClicked(object? sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
    
    private async void OnSaveClicked(object? sender, EventArgs e)
    {
        if (_imageData.Length == 0) return;
        
        var fileName = $"ImageFusion_{DateTime.Now:yyyyMMdd_HHmmss}.png";
        var result = await _fileService.SaveImageAsync(_imageData, fileName);
        
        var message = result != null
            ? $"{AppStrings.ExportSuccessMessage}\n{result}"
            : AppStrings.ExportFailedMessage;
        
        await DisplayAlert(AppStrings.ApplicationTitle, message, AppStrings.OkButtonText);
    }
}
