using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduledShutdown.Common
{
    public interface ITicker
    {

        bool IsTicking { get; set; }

        TimeSpan Interval { get; set; }

        event Action OnTick;

        void Start();

        void Stop();

    }
}
