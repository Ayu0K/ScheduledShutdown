using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ScheduledShutdown.Converters
{
    public class BooleanReConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool input && targetType == typeof(bool))
            {
                return !input;
            }
            throw new InvalidOperationException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool input && targetType == typeof(bool))
            {
                return !input;
            }
            throw new InvalidOperationException();
        }
    }
}
