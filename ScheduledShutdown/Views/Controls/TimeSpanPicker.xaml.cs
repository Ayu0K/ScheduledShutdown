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
    /// TimeSpanPicker.xaml 的交互逻辑
    /// </summary>
    public partial class TimeSpanPicker : UserControl
    {



        public int SelectedHour
        {
            get { return (int)GetValue(SelectedHourProperty); }
            set { SetValue(SelectedHourProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedHour.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedHourProperty =
            DependencyProperty.Register("SelectedHour", typeof(int), typeof(TimeSpanPicker), new PropertyMetadata(0));



        public int SelectedMinute
        {
            get { return (int)GetValue(SelectedMinuteProperty); }
            set { SetValue(SelectedMinuteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedMinute.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedMinuteProperty =
            DependencyProperty.Register("SelectedMinute", typeof(int), typeof(TimeSpanPicker), new PropertyMetadata(0));



        public int SelectedSecond
        {
            get { return (int)GetValue(SelectedSecondProperty); }
            set { SetValue(SelectedSecondProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedSecond.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedSecondProperty =
            DependencyProperty.Register("SelectedSecond", typeof(int), typeof(TimeSpanPicker), new PropertyMetadata(0));



        public TimeSpan SelectedTimeSpan
        {
            get { return (TimeSpan)GetValue(SelectedTimeSpanProperty); }
            set 
            {
                SelectedHour = value.Hours;
                SelectedMinute = value.Minutes;
                SelectedSecond = value.Seconds;
                SetValue(SelectedTimeSpanProperty, value); 
            }
        }

        // Using a DependencyProperty as the backing store for SelectedTimeSpan.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedTimeSpanProperty =
            DependencyProperty.Register("SelectedTimeSpan", typeof(TimeSpan), typeof(TimeSpanPicker), new PropertyMetadata(TimeSpan.Zero));



        public TimeSpanPicker()
        {
            InitializeComponent();
        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            SetValue(SelectedTimeSpanProperty, new TimeSpan(SelectedHour, SelectedMinute, SelectedSecond));
        }
    }
}
