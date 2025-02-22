using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace TickTest
{
    public class RadiusConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null || value is not double radius)
            {
                return null;
            }

            return radius * 2;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null || value is not double length)
            {
                return null;
            }

            return length / 2;
        }
    }
}
