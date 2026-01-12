using System.Collections.ObjectModel;
using ImageFusion.Models;

namespace ImageFusion.ViewModels;

/// <summary>
/// ViewModel for the Original tab with carousel functionality.
/// </summary>
public class OriginalViewModel : BaseViewModel
{
    private ObservableCollection<ImageItem> _originalImages = new();
    private int _currentPosition;
    
    public ObservableCollection<ImageItem> OriginalImages
    {
        get => _originalImages;
        set => SetProperty(ref _originalImages, value);
    }
    
    public int CurrentPosition
    {
        get => _currentPosition;
        set => SetProperty(ref _currentPosition, value);
    }
    
    public OriginalViewModel()
    {
        LoadSampleData();
    }
    
    private void LoadSampleData()
    {
        OriginalImages = new ObservableCollection<ImageItem>
        {
            new() { ImageSource = "dotnet_bot.png", Title = "Original 1" },
            new() { ImageSource = "dotnet_bot.png", Title = "Original 2" },
            new() { ImageSource = "dotnet_bot.png", Title = "Original 3" }
        };
    }
}
