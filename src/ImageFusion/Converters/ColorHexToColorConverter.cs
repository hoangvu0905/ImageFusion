using System.Globalization;

namespace ImageFusion.Converters;

public class ColorHexToColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string hex)
        {
            try
            {
                return Color.FromArgb(hex);
            }
            catch
            {
                return Colors.White;
            }
        }
        return Colors.White;
    }
    
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Color color)
        {
            return color.ToArgbHex();
        }
        return "#FFFFFF";
    }
}
