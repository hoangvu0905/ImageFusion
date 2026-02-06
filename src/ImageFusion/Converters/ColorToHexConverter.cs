using System.Globalization;

namespace ImageFusion.Converters;

/// <summary>
/// Converter to convert Color to hex string and back.
/// </summary>
public class ColorToHexConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Color color)
        {
            return color.ToArgbHex();
        }
        return string.Empty;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string hexString && !string.IsNullOrWhiteSpace(hexString))
        {
            try
            {
                return Color.FromArgb(hexString);
            }
            catch
            {
                return Colors.Black;
            }
        }
        return Colors.Black;
    }
}
