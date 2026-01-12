using ImageFusion.Services;
using ImageFusion.ViewModels;
using ImageFusion.Views;
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
        builder.Services.AddTransient<MainViewModel>();
        
        // Register Views
        builder.Services.AddTransient<PreviewView>();
        builder.Services.AddTransient<OriginalView>();
        builder.Services.AddTransient<SettingsView>();
        builder.Services.AddTransient<MainPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}