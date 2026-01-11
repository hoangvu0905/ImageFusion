using ImageFusion.Models;

namespace ImageFusion.Services;

public interface IFileService
{
    Task<IEnumerable<ImageItem>> PickImagesAsync();
    Task<string?> SaveImageAsync(byte[] imageData, string suggestedFileName);
}
