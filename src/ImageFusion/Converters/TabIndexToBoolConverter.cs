using System.Globalization;

namespace ImageFusion.Converters;

public class TabIndexToBoolConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int selectedIndex && parameter is string paramString && int.TryParse(paramString, out var tabIndex))
        {
            return selectedIndex == tabIndex;
        }
        return false;
    }
    
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
