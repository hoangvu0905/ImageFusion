using CommunityToolkit.Mvvm.ComponentModel;

namespace ImageFusion.Models;

public partial class ImageItem : ObservableObject
{
    [ObservableProperty]
    private string _id = Guid.NewGuid().ToString();
    
    [ObservableProperty]
    private string _filePath = string.Empty;
    
    [ObservableProperty]
    private byte[] _imageData = [];
    
    [ObservableProperty]
    private int _order;
    
    [ObservableProperty]
    private ImageSource? _thumbnailSource;
    
    public int OriginalWidth { get; set; }
    public int OriginalHeight { get; set; }
}
