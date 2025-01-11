using ScheduledShutdown.Common;
using ScheduledShutdown.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ScheduledShutdown.ViewModels
{
    public class MainWindowViewModel : ViewModelBase<MainWindow>
    {

        public bool SelectedMode
        {
            get { return (bool)GetValue(SelectedModeProperty); }
            set { SetValue(SelectedModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedModeProperty =
            DependencyProperty.Register("SelectedMode", typeof(bool), typeof(MainWindowViewModel), new PropertyMetadata(true));



        public DateTime SelectedDateTime
        {
            get { return (DateTime)GetValue(SelectedDateTimeProperty); }
            set { SetValue(SelectedDateTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedDateTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedDateTimeProperty =
            DependencyProperty.Register("SelectedDateTime", typeof(DateTime), typeof(MainWindowViewModel), new PropertyMetadata(DateTime.Now));



        public TimeSpan SelectedTimeSpan
        {
            get { return (TimeSpan)GetValue(SelectedTimeSpanProperty); }
            set { SetValue(SelectedTimeSpanProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedTimeSpan.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedTimeSpanProperty =
            DependencyProperty.Register("SelectedTimeSpan", typeof(TimeSpan), typeof(MainWindowViewModel), new PropertyMetadata(TimeSpan.Zero));



        public DateTime ScheduledTime
        {
            get { return (DateTime)GetValue(ScheduledTimeProperty); }
            set 
            {
                Globals.ScheduledTime = value;
                SetValue(ScheduledTimeProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for ScheduledTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScheduledTimeProperty =
            DependencyProperty.Register("ScheduledTime", typeof(DateTime), typeof(MainWindowViewModel), new PropertyMetadata(Globals.ScheduledTime));

        
        private CountdownWindow CountdownWindow { get; } = new CountdownWindow();

        public MainWindowViewModel(MainWindow view) : base(view)
        {
            Globals.WatchDogTicker.OnTick += WatchDogTicker_OnTick;
        }

        private void WatchDogTicker_OnTick()
        {
            ScheduledTime = Globals.ScheduledTime;
            if (ScheduledTime.Subtract(DateTime.Now).TotalMinutes < 5.0)
            {
                Globals.WatchDogTicker.Stop();
                Globals.CountdownTicker.Start();
                CountdownWindow.Show();
            }
        }

        public void OnSelectedTimeChanged()
        {
            if (SelectedMode)
            {
                ScheduledTime = SelectedDateTime;
            }
            else
            {
                ScheduledTime = DateTime.Now.Add(SelectedTimeSpan);
            }
        }

        public ICommand AcceptCommand => new RelayCommand(() =>
        {
            OnSelectedTimeChanged();
            ShutdownInterop.Cancel().WaitForExit();
            ShutdownInterop.Shutdown((int)Math.Floor(ScheduledTime.Subtract(DateTime.Now).TotalSeconds));
            OwnerWindow.Hide();
            View.ShowBalloonTip(5000, "关机计划设置成功", $"此电脑将于{ScheduledTime:yyyy年MM月dd日 HH:mm:ss}关闭");
            Globals.WatchDogTicker.Stop();
            Globals.WatchDogTicker.Start();
        });

        public ICommand CancelCommand => new RelayCommand(() =>
        {
            ShutdownInterop.Cancel().WaitForExit();
            Application.Current.Shutdown();
        });

    }
}
