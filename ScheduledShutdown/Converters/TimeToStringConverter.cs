using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ScheduledShutdown.Converters
{
    public class TimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                return dateTime.ToString(parameter as string);
            }
            if (value is TimeSpan time)
            {
                return time.ToString(parameter as string);
            }
            throw new InvalidOperationException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(DateTime))
            {
                return DateTime.Parse(value as string);
            }
            if (targetType == typeof(TimeSpan))
            {
                return TimeSpan.Parse(value as string);
            }
            throw new InvalidOperationException();
        }
    }
}
