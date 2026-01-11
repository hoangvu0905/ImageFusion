using CommunityToolkit.Maui;
using ImageFusion.Services;
using ImageFusion.ViewModels;
using ImageFusion.Views;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;
using CoreServices = ImageFusion.Core.Services;

namespace ImageFusion;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseSkiaSharp()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<CoreServices.IImageProcessingService, CoreServices.ImageProcessingService>();
        
        builder.Services.AddSingleton<ISettingsService, SettingsService>();
        builder.Services.AddSingleton<IImageProcessingService, ImageProcessingService>();
        builder.Services.AddSingleton<IFileService, FileService>();
        
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<MainPage>();
        
        builder.Services.AddTransient<ImagePreviewPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}