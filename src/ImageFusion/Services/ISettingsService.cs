using ImageFusion.Models;

namespace ImageFusion.Services;

/// <summary>
/// Interface for settings service following Interface Segregation Principle.
/// </summary>
public interface ISettingsService
{
    AppSettings GetSettings();
    
    void SaveSettings(AppSettings settings);
    
    IReadOnlyList<string> GetCombineTypes();
    
    IReadOnlyList<string> GetImageFormats();
}
