using ImageFusion.Pages;

namespace ImageFusion;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        Routing.RegisterRoute(nameof(PreviewPage), typeof(PreviewPage));
        Routing.RegisterRoute(nameof(OriginalPage), typeof(OriginalPage));
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        
        // Set default tab to Original (index 1)
        CurrentItem = Items[0];
        if (CurrentItem is TabBar tabBar && tabBar.Items.Count > 1)
        {
            tabBar.CurrentItem = tabBar.Items[1];
        }
    }
}