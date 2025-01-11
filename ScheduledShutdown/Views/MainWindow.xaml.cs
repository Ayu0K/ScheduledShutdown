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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using NotifyIcon = System.Windows.Forms.NotifyIcon;
using ContextMenu = System.Windows.Forms.ContextMenu;
using MenuItem = System.Windows.Forms.MenuItem;
using ToolTipIcon = System.Windows.Forms.ToolTipIcon;
using ScheduledShutdown.Common;

namespace ScheduledShutdown.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly MainWindowViewModel _viewModel;

        private readonly NotifyIcon _notifyIcon;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainWindowViewModel(this);
            DataContext = _viewModel;
            _notifyIcon = new NotifyIcon()
            {
                Icon = Properties.Resource.AppIcon,
                Visible = true,
                ContextMenu = new ContextMenu(new MenuItem[]
                {
                    new MenuItem("更改关机计划", (sender, e) =>
                    {
                        Show();
                    }),
                    new MenuItem("取消关机计划", (sender, e) =>
                    {
                        ShutdownInterop.Cancel();
                        Application.Current.Shutdown();
                    })
                })
            };
        }

        public void ShowBalloonTip(int timeout, string title, string message)
        {
            _notifyIcon.ShowBalloonTip(timeout, title, message, ToolTipIcon.Info);
        }

        private void OnSelectedTimeChanged(object sender, MouseWheelEventArgs e)
        {
            _viewModel.OnSelectedTimeChanged();
        }

        private void OnModeChanged(object sender, RoutedEventArgs e)
        {
            _viewModel.OnSelectedTimeChanged();
        }

        private void OnClosed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
