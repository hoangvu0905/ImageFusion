using CommunityToolkit.Maui.Storage;
using ImageFusion.Models;
using SkiaSharp;

namespace ImageFusion.Services;

public class FileService : IFileService
{
    public async Task<IEnumerable<ImageItem>> PickImagesAsync()
    {
        var results = new List<ImageItem>();
        
        try
        {
            var options = new PickOptions
            {
                PickerTitle = "Select Images",
                FileTypes = FilePickerFileType.Images
            };
            
            var files = await FilePicker.PickMultipleAsync(options);
            
            if (files == null) return results;
            
            var order = 0;
            foreach (var file in files)
            {
                using var stream = await file.OpenReadAsync();
                using var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);
                var imageData = memoryStream.ToArray();
                
                using var bitmap = SKBitmap.Decode(imageData);
                if (bitmap == null) continue;
                
                var imageItem = new ImageItem
                {
                    FilePath = file.FullPath,
                    ImageData = imageData,
                    Order = order++,
                    OriginalWidth = bitmap.Width,
                    OriginalHeight = bitmap.Height,
                    ThumbnailSource = ImageSource.FromStream(() => new MemoryStream(imageData))
                };
                
                results.Add(imageItem);
            }
        }
        catch
        {
            // Return empty list on error
        }
        
        return results;
    }
    
    public async Task<string?> SaveImageAsync(byte[] imageData, string suggestedFileName)
    {
        try
        {
#if ANDROID
            return await SaveToAndroidGalleryAsync(imageData, suggestedFileName);
#else
            return await SaveWithDialogAsync(imageData, suggestedFileName);
#endif
        }
        catch
        {
            return null;
        }
    }
    
#if ANDROID
    private static async Task<string?> SaveToAndroidGalleryAsync(byte[] imageData, string fileName)
    {
        var picturesPath = Android.OS.Environment.GetExternalStoragePublicDirectory(
            Android.OS.Environment.DirectoryPictures)?.AbsolutePath;
            
        if (string.IsNullOrEmpty(picturesPath))
        {
            picturesPath = FileSystem.AppDataDirectory;
        }
        
        var filePath = Path.Combine(picturesPath, fileName);
        await File.WriteAllBytesAsync(filePath, imageData);
        
        return filePath;
    }
#else
    private static async Task<string?> SaveWithDialogAsync(byte[] imageData, string suggestedFileName)
    {
        using var stream = new MemoryStream(imageData);
        var result = await FileSaver.Default.SaveAsync(suggestedFileName, stream, CancellationToken.None);
        
        return result.IsSuccessful ? result.FilePath : null;
    }
#endif
}
