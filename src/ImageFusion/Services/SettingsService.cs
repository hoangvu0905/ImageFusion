using System.Text.Json;
using ImageFusion.Core.Constants;
using ImageFusion.Models;

namespace ImageFusion.Services;

public class SettingsService : ISettingsService
{
    private readonly string _settingsFilePath;
    
    public SettingsService()
    {
        _settingsFilePath = Path.Combine(FileSystem.AppDataDirectory, AppConstants.SettingsFileName);
    }
    
    public async Task<AppSettings> LoadSettingsAsync()
    {
        try
        {
            if (!File.Exists(_settingsFilePath))
            {
                return new AppSettings();
            }
            
            var json = await File.ReadAllTextAsync(_settingsFilePath);
            return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
        }
        catch
        {
            return new AppSettings();
        }
    }
    
    public async Task SaveSettingsAsync(AppSettings settings)
    {
        try
        {
            var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_settingsFilePath, json);
        }
        catch
        {
            // Silently fail - settings are not critical
        }
    }
}
