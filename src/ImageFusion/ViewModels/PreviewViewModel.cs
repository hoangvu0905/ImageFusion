using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using ImageFusion.Models;

namespace ImageFusion.ViewModels;

/// <summary>
/// ViewModel for the Preview tab with carousel functionality.
/// </summary>
public partial class PreviewViewModel : BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<ImageItem> _previewImages = new();
    
    [ObservableProperty]
    private int _currentPosition;
    
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
