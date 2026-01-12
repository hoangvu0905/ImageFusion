using System.Collections.ObjectModel;
using ImageFusion.Models;

namespace ImageFusion.ViewModels;

/// <summary>
/// ViewModel for the Preview tab with carousel functionality.
/// </summary>
public class PreviewViewModel : BaseViewModel
{
    private ObservableCollection<ImageItem> _previewImages = new();
    private int _currentPosition;
    
    public ObservableCollection<ImageItem> PreviewImages
    {
        get => _previewImages;
        set => SetProperty(ref _previewImages, value);
    }
    
    public int CurrentPosition
    {
        get => _currentPosition;
        set => SetProperty(ref _currentPosition, value);
    }
    
    public PreviewViewModel()
    {
        LoadSampleData();
    }
    
    private void LoadSampleData()
    {
        PreviewImages = new ObservableCollection<ImageItem>
        {
            new() { ImageSource = "dotnet_bot.png", Title = "Preview 1" },
            new() { ImageSource = "dotnet_bot.png", Title = "Preview 2" },
            new() { ImageSource = "dotnet_bot.png", Title = "Preview 3" }
        };
    }
}
