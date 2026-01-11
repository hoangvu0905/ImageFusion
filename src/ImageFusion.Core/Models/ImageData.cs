namespace ImageFusion.Core.Models;

public class ImageData
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public byte[] Data { get; set; } = [];
    public int Order { get; set; }
    public int OriginalWidth { get; set; }
    public int OriginalHeight { get; set; }
}
