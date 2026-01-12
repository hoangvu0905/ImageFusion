namespace ImageFusion;

public partial class AppShell : Shell
{
    private readonly IServiceProvider _serviceProvider;
    
    public AppShell(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
        
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
    }
}