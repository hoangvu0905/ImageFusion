using ImageFusion.ViewModels;

namespace ImageFusion.Views;

public partial class SettingsView : ContentView
{
    public SettingsView(SettingsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
