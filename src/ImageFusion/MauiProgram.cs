using ImageFusion.Pages;
using ImageFusion.Services;
using ImageFusion.ViewModels;
using Microsoft.Extensions.Logging;

namespace ImageFusion;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Register Services
        builder.Services.AddSingleton<ISettingsService, SettingsService>();
        
        // Register ViewModels
        builder.Services.AddTransient<PreviewViewModel>();
        builder.Services.AddTransient<OriginalViewModel>();
        builder.Services.AddTransient<SettingsViewModel>();
        
        // Register Pages
        builder.Services.AddTransient<PreviewPage>();
        builder.Services.AddTransient<OriginalPage>();
        builder.Services.AddTransient<SettingsPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}