using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using ImageFusion.Models;

namespace ImageFusion.ViewModels;

/// <summary>
/// ViewModel for the Original tab with carousel functionality.
/// </summary>
public partial class OriginalViewModel : BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<ImageItem> _originalImages = new();
    
    [ObservableProperty]
    private int _currentPosition;
    
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
