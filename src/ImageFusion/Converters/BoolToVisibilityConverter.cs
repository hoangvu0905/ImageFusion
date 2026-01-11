using System.Globalization;

namespace ImageFusion.Converters;

public class BoolToVisibilityConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            var invert = parameter?.ToString()?.Equals("invert", StringComparison.OrdinalIgnoreCase) == true;
            return invert ? !boolValue : boolValue;
        }
        return false;
    }
    
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
