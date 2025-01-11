using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ScheduledShutdown.ViewModels
{
    public abstract class ViewModelBase<T> : DependencyObject where T : DependencyObject
    {

        public Window OwnerWindow { get; set; }

        public T View { get; set; }

#pragma warning disable CS4014
        public ViewModelBase(T view) 
        {
            View = view;
            if (View is Window window)
            {
                OwnerWindow = window;
            }
            else
            {
                FindOwner();
            }
        }
#pragma warning restore CS4014

#pragma warning disable CS4014
        private async Task FindOwner()
        {
            if (View is UIElement elm)
            {
                var parent = VisualTreeHelper.GetParent(elm);
                while (parent != null)
                {
                    if (parent is Window)
                    {
                        OwnerWindow = parent as Window;
                        break;
                    }
                    parent = VisualTreeHelper.GetParent(parent);
                }
                if (OwnerWindow == null)
                {
                    await Task.Delay(100);
                    FindOwner();
                }
            }
        }
#pragma warning restore CS4014
    }
}
