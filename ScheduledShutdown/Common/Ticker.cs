using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ScheduledShutdown.Common
{
    public class Ticker : ITicker
    {
        public bool IsTicking { get; set; } = false;

        public TimeSpan Interval { get; set; }

        public event Action OnTick;

        private CancellationTokenSource _cancellationTokenSource;

#pragma warning disable CS4014 
        public void Start()
        {
            if (IsTicking) return;
            IsTicking = true;
            _cancellationTokenSource = new CancellationTokenSource();
            Tick(_cancellationTokenSource.Token);
        }
#pragma warning restore CS4014

        public void Stop()
        {
            IsTicking = false;
            try
            {
                _cancellationTokenSource.Cancel();
            }
            catch { }
        }

        private Task Tick(CancellationToken token)
        {
            return Task.Factory.StartNew(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    Application.Current.Dispatcher.Invoke(OnTick);
                    try
                    {
                        Task.Delay(Interval, token).Wait();
                    }
                    catch { }
                }
            }, token);
        }

    }
}
