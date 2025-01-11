using ScheduledShutdown.Common;
using ScheduledShutdown.ViewModels;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ScheduledShutdown.Views
{
    /// <summary>
    /// CountdownWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CountdownWindow : Window
    {

        private readonly CountdownWindowViewModel _viewModel;

        public CountdownWindow()
        {
            InitializeComponent();
            _viewModel = new CountdownWindowViewModel(this);
            DataContext = _viewModel;
        }

        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Globals.ScheduledTime > DateTime.Now)
            {
                e.Cancel = true;
                Globals.CountdownTicker.Stop();
                Hide();
            }
        }

        public new void Show()
        {
            base.Show();
            Left = SystemParameters.WorkArea.Width - Width;
            Top = SystemParameters.WorkArea.Height;

            var anime = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(1)),
                From = SystemParameters.WorkArea.Height,
                To = SystemParameters.WorkArea.Height - Height
            };
            BeginAnimation(TopProperty, anime);
        }

    }
}
