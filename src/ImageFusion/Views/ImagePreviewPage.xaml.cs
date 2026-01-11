using ImageFusion.Core.Constants;
using ImageFusion.Services;

namespace ImageFusion.Views;

public partial class ImagePreviewPage : ContentPage
{
    private readonly byte[] _imageData;
    private readonly IFileService _fileService;
    
    public ImagePreviewPage(byte[] imageData)
    {
        InitializeComponent();
        _imageData = imageData;
        _fileService = Application.Current?.Handler?.MauiContext?.Services.GetService<IFileService>() 
                       ?? new FileService();
        
        PreviewImage.Source = ImageSource.FromStream(() => new MemoryStream(_imageData));
    }
    
    private async void OnCloseClicked(object? sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
    
    private async void OnSaveClicked(object? sender, EventArgs e)
    {
        var fileName = $"ImageFusion_{DateTime.Now:yyyyMMdd_HHmmss}.png";
        var result = await _fileService.SaveImageAsync(_imageData, fileName);
        
        var message = result != null
            ? $"{AppConstants.ExportSuccessMessage}\n{result}"
            : AppConstants.ExportFailedMessage;
        
        await DisplayAlert(AppConstants.ApplicationTitle, message, "OK");
    }
}
