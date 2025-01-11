using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ScheduledShutdown.Converters
{
    public class TimeSpanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TimeSpan ts)
            {
                if (ts.TotalMinutes < 60)
                {
                    return $"{Math.Floor(ts.TotalMinutes)}分钟";
                }
                else
                {
                    if (ts.TotalMinutes % 60 == 0)
                    {
                        return $"{Math.Floor(ts.TotalHours)}小时";
                    }
                    else
                    {
                        return $"{Math.Floor(ts.TotalHours)}小时{Math.Floor(ts.TotalMinutes % 60)}分钟";
                    }
                }
            }
            throw new InvalidOperationException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
            {
                Regex matchHour = new Regex("([\\d]+)小时");
                Regex matchMinute = new Regex("([\\d]+)分钟");
                TimeSpan ts = TimeSpan.Zero;
                if (matchHour.IsMatch(str))
                {
                    ts += TimeSpan.FromHours(int.Parse(matchHour.Match(str).Groups[1].Value));
                }
                if (matchMinute.IsMatch(str))
                {
                    ts += TimeSpan.FromHours(int.Parse(matchMinute.Match(str).Groups[1].Value));
                }
                return ts;
            }
            throw new InvalidOperationException();
        }
    }
}
