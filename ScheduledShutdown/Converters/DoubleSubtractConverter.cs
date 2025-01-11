using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ScheduledShutdown.Converters
{
    public class DoubleSubtractConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double input && targetType == typeof(double))
            {
                double operate = double.Parse(parameter as string);
                return input - operate;
            }
            throw new InvalidOperationException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double input && targetType == typeof(double))
            {
                double operate = double.Parse(parameter as string);
                return input + operate;
            }
            throw new InvalidOperationException();
        }
    }
}
