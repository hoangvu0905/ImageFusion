using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ImageFusion.Core.Constants;
using ImageFusion.Models;
using ImageFusion.Services;
using SkiaSharp;

namespace ImageFusion.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly ISettingsService _settingsService;
    private readonly IImageProcessingService _imageProcessingService;
    private readonly IFileService _fileService;
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasImages))]
    [NotifyPropertyChangedFor(nameof(ShowLayoutOptions))]
    [NotifyCanExecuteChangedFor(nameof(ExportCommand))]
    [NotifyCanExecuteChangedFor(nameof(RemoveImageCommand))]
    private ObservableCollection<ImageItem> _images = [];
    
    [ObservableProperty]
    private int _outputWidth = AppConstants.DefaultOutputWidth;
    
    [ObservableProperty]
    private int _outputHeight = AppConstants.DefaultOutputHeight;
    
    [ObservableProperty]
    private int _borderPixel = AppConstants.DefaultBorderPixel;
    
    [ObservableProperty]
    private string _borderColorHex = AppConstants.DefaultBorderColorHex;
    
    [ObservableProperty]
    private int _splitPixel = AppConstants.DefaultSplitPixel;
    
    [ObservableProperty]
    private string _splitColorHex = AppConstants.DefaultSplitColorHex;
    
    [ObservableProperty]
    private MergeLayoutType _mergeLayout = MergeLayoutType.Grid;
    
    [ObservableProperty]
    private ImageFormatType _imageFormat = ImageFormatType.Png;
    
    [ObservableProperty]
    private ImageSource? _previewImageSource;
    
    [ObservableProperty]
    private bool _isProcessing;
    
    [ObservableProperty]
    private int _selectedTabIndex = 1;
    
    public bool HasImages => Images.Count > 0;
    public bool ShowLayoutOptions => Images.Count >= AppConstants.MinImagesForLayoutChoice;
    
    public List<MergeLayoutType> AvailableLayouts { get; } = [MergeLayoutType.Grid, MergeLayoutType.FeatureMain];
    public List<ImageFormatType> AvailableFormats { get; } = [ImageFormatType.Png, ImageFormatType.Jpeg, ImageFormatType.Webp];
    
    private byte[]? _currentPreviewData;
    
    public MainViewModel(
        ISettingsService settingsService,
        IImageProcessingService imageProcessingService,
        IFileService fileService)
    {
        _settingsService = settingsService;
        _imageProcessingService = imageProcessingService;
        _fileService = fileService;
        
        Images.CollectionChanged += (_, _) =>
        {
            OnPropertyChanged(nameof(HasImages));
            OnPropertyChanged(nameof(ShowLayoutOptions));
            ExportCommand.NotifyCanExecuteChanged();
            RemoveImageCommand.NotifyCanExecuteChanged();
        };
    }
    
    public async Task InitializeAsync()
    {
        var settings = await _settingsService.LoadSettingsAsync();
        ApplySettings(settings);
    }
    
    private void ApplySettings(AppSettings settings)
    {
        OutputWidth = settings.OutputWidth;
        OutputHeight = settings.OutputHeight;
        BorderPixel = settings.BorderPixel;
        BorderColorHex = settings.BorderColorHex;
        SplitPixel = settings.SplitPixel;
        SplitColorHex = settings.SplitColorHex;
        MergeLayout = settings.MergeLayout;
        ImageFormat = settings.ImageFormat;
    }
    
    public async Task SaveSettingsAsync()
    {
        var settings = new AppSettings
        {
            OutputWidth = OutputWidth,
            OutputHeight = OutputHeight,
            BorderPixel = BorderPixel,
            BorderColorHex = BorderColorHex,
            SplitPixel = SplitPixel,
            SplitColorHex = SplitColorHex,
            MergeLayout = MergeLayout,
            ImageFormat = ImageFormat
        };
        
        await _settingsService.SaveSettingsAsync(settings);
    }
    
    [RelayCommand]
    private async Task AddImagesAsync()
    {
        var newImages = await _fileService.PickImagesAsync();
        var startOrder = Images.Count;
        
        foreach (var image in newImages)
        {
            image.Order = startOrder++;
            Images.Add(image);
        }
        
        if (newImages.Any())
        {
            await GeneratePreviewAsync();
        }
    }
    
    [RelayCommand(CanExecute = nameof(HasImages))]
    private void RemoveImage(ImageItem? image)
    {
        if (image == null) return;
        
        Images.Remove(image);
        ReorderImages();
        _ = GeneratePreviewAsync();
    }
    
    [RelayCommand(CanExecute = nameof(HasImages))]
    private async Task ExportAsync()
    {
        if (_currentPreviewData == null || !HasImages)
        {
            await ShowAlertAsync(AppConstants.NoImagesMessage);
            return;
        }
        
        var extension = ImageFormat switch
        {
            ImageFormatType.Jpeg => "jpg",
            ImageFormatType.Webp => "webp",
            _ => "png"
        };
        
        var fileName = $"ImageFusion_{DateTime.Now:yyyyMMdd_HHmmss}.{extension}";
        var result = await _fileService.SaveImageAsync(_currentPreviewData, fileName);
        
        var message = result != null
            ? $"{AppConstants.ExportSuccessMessage}\n{result}"
            : AppConstants.ExportFailedMessage;
        
        await ShowAlertAsync(message);
    }
    
    private static async Task ShowAlertAsync(string message)
    {
        var page = Application.Current?.Windows.FirstOrDefault()?.Page;
        if (page != null)
        {
            await page.DisplayAlert(AppConstants.ApplicationTitle, message, "OK");
        }
    }
    
    public async Task GeneratePreviewAsync()
    {
        if (!HasImages)
        {
            PreviewImageSource = null;
            _currentPreviewData = null;
            return;
        }
        
        IsProcessing = true;
        
        try
        {
            await Task.Run(() =>
            {
                var borderColor = ParseColor(BorderColorHex);
                var splitColor = ParseColor(SplitColorHex);
                
                using var bitmap = _imageProcessingService.ProcessImages(
                    Images.OrderBy(i => i.Order).ToList(),
                    OutputWidth,
                    OutputHeight,
                    BorderPixel,
                    borderColor,
                    SplitPixel,
                    splitColor,
                    MergeLayout);
                
                _currentPreviewData = _imageProcessingService.EncodeBitmap(bitmap, ImageFormat);
            });
            
            if (_currentPreviewData != null)
            {
                PreviewImageSource = ImageSource.FromStream(() => new MemoryStream(_currentPreviewData));
            }
        }
        finally
        {
            IsProcessing = false;
        }
    }
    
    public void MoveImage(ImageItem image, int newIndex)
    {
        var oldIndex = Images.IndexOf(image);
        if (oldIndex < 0 || oldIndex == newIndex) return;
        
        Images.Move(oldIndex, newIndex);
        ReorderImages();
        _ = GeneratePreviewAsync();
    }
    
    private void ReorderImages()
    {
        for (var i = 0; i < Images.Count; i++)
        {
            Images[i].Order = i;
        }
    }
    
    private static SKColor ParseColor(string hex)
    {
        try
        {
            if (hex.StartsWith('#'))
            {
                hex = hex[1..];
            }
            
            if (hex.Length == 6)
            {
                var r = Convert.ToByte(hex[..2], 16);
                var g = Convert.ToByte(hex[2..4], 16);
                var b = Convert.ToByte(hex[4..6], 16);
                return new SKColor(r, g, b);
            }
        }
        catch
        {
            // Fall through to default
        }
        
        return SKColors.White;
    }
    
    partial void OnOutputWidthChanged(int value) => OnSettingChanged();
    partial void OnOutputHeightChanged(int value) => OnSettingChanged();
    partial void OnBorderPixelChanged(int value) => OnSettingChanged();
    partial void OnBorderColorHexChanged(string value) => OnSettingChanged();
    partial void OnSplitPixelChanged(int value) => OnSettingChanged();
    partial void OnSplitColorHexChanged(string value) => OnSettingChanged();
    partial void OnMergeLayoutChanged(MergeLayoutType value) => OnSettingChanged();
    partial void OnImageFormatChanged(ImageFormatType value) => OnSettingChanged();
    
    private void OnSettingChanged()
    {
        _ = SaveSettingsAsync();
        _ = GeneratePreviewAsync();
    }
    
    public byte[]? GetCurrentPreviewData() => _currentPreviewData;
}
