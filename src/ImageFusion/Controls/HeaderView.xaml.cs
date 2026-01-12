namespace ImageFusion.Controls;

public partial class HeaderView : ContentView
{
    public event EventHandler? ExportClicked;
    public event EventHandler? AddClicked;
    
    public HeaderView()
    {
        InitializeComponent();
    }
    
    private void OnExportClicked(object? sender, EventArgs e)
    {
        ExportClicked?.Invoke(this, e);
    }
    
    private void OnAddClicked(object? sender, EventArgs e)
    {
        AddClicked?.Invoke(this, e);
    }
}
