using ScheduledShutdown.Common;
using ScheduledShutdown.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ScheduledShutdown.ViewModels
{
    public class CountdownWindowViewModel : ViewModelBase<CountdownWindow>
    {

        public TimeSpan RemainingTimes
        {
            get { return (TimeSpan)GetValue(RemainingTimesProperty); }
            set { SetValue(RemainingTimesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RemainingTimes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RemainingTimesProperty =
            DependencyProperty.Register("RemainingTimes", typeof(TimeSpan), typeof(CountdownWindowViewModel), new PropertyMetadata(TimeSpan.Zero));



        public TimeSpan SelectedDelay
        {
            get { return (TimeSpan)GetValue(SelectedDelayProperty); }
            set { SetValue(SelectedDelayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedDelay.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedDelayProperty =
            DependencyProperty.Register("SelectedDelay", typeof(TimeSpan), typeof(CountdownWindowViewModel), new PropertyMetadata(TimeSpan.FromMinutes(5)));



        public ObservableCollection<TimeSpan> Delays { get; } = new ObservableCollection<TimeSpan>()
        {
            TimeSpan.FromMinutes(5),
            TimeSpan.FromMinutes(10),
            TimeSpan.FromMinutes(15),
            TimeSpan.FromMinutes(30),
            TimeSpan.FromHours(1),
            TimeSpan.FromHours(2),
            TimeSpan.FromHours(3),
            TimeSpan.FromHours(4),
            TimeSpan.FromHours(5),
            TimeSpan.FromHours(10)
        };

        public CountdownWindowViewModel(CountdownWindow view) : base(view)
        {
            Globals.CountdownTicker.OnTick += CountdownTicker_OnTick;
            DelayCommand = new RelayCommand(() =>
            {
                Globals.ScheduledTime += SelectedDelay;
                ShutdownInterop.Cancel().WaitForExit();
                ShutdownInterop.Shutdown((int)Math.Floor(Globals.ScheduledTime.Subtract(DateTime.Now).TotalSeconds));
                Globals.CountdownTicker.Stop();
                Globals.WatchDogTicker.Start();
                View.Close();
            });
        }

        private void CountdownTicker_OnTick()
        {
            RemainingTimes = Globals.ScheduledTime - DateTime.Now;
            if (RemainingTimes <= TimeSpan.Zero)
            {
                Globals.CountdownTicker.Stop();
                Application.Current.Shutdown();
            }
        }

        public ICommand DelayCommand { get; }

        public ICommand CancelCommand => new RelayCommand(() =>
        {
            ShutdownInterop.Cancel().WaitForExit();
            Application.Current.Shutdown();
        });

    }
}
