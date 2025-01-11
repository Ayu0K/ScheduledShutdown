using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace ScheduledShutdown.Converters
{
    public class BooleanToVisibilityReConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool input && targetType == typeof(Visibility))
            {
                return input ? Visibility.Collapsed : Visibility.Visible;
            }
            throw new InvalidOperationException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility input && targetType == typeof(bool))
            {
                return input == Visibility.Collapsed;
            }
            throw new InvalidOperationException();
        }
    }
}
