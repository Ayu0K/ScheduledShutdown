using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScheduledShutdown.Views.Controls
{
    /// <summary>
    /// TimePicker.xaml 的交互逻辑
    /// </summary>
    public partial class TimePicker : UserControl
    {

        public int SelectedHour
        {
            get { return (int)GetValue(SelectedHourProperty); }
            set { SetValue(SelectedHourProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedHour.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedHourProperty =
            DependencyProperty.Register("SelectedHour", typeof(int), typeof(TimePicker), new PropertyMetadata(DateTime.Now.Hour));



        public int SelectedMinute
        {
            get { return (int)GetValue(SelectedMinuteProperty); }
            set { SetValue(SelectedMinuteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedMinute.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedMinuteProperty =
            DependencyProperty.Register("SelectedMinute", typeof(int), typeof(TimePicker), new PropertyMetadata(DateTime.Now.Minute));



        public DateTime SelectedTime
        {
            get { return (DateTime)GetValue(SelectedTimeProperty); }
            set 
            {
                SelectedHour = value.Hour;
                SelectedMinute = value.Minute;
                SetValue(SelectedTimeProperty, value); 
            }
        }

        // Using a DependencyProperty as the backing store for SelectedTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedTimeProperty =
            DependencyProperty.Register("SelectedTime", typeof(DateTime), typeof(TimePicker), new PropertyMetadata(DateTime.Now));


        public TimePicker()
        {
            InitializeComponent();
        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            DateTime result = DateTime.Parse($"{SelectedHour}:{SelectedMinute}");
            if (result < DateTime.Now)
            {
                result += TimeSpan.FromDays(1);
            }
            SetValue(SelectedTimeProperty, result);
        }
    }
}
