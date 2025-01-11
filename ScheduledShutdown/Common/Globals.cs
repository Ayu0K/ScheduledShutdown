using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduledShutdown.Common
{
    public static class Globals
    {

        /// <summary>
        /// 计划的关机时间
        /// </summary>
        public static DateTime ScheduledTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 守护Ticker
        /// </summary>
        public static Ticker WatchDogTicker { get; } = new Ticker() { Interval =  TimeSpan.FromMinutes(1) };

        /// <summary>
        /// 倒计时Ticker
        /// </summary>
        public static Ticker CountdownTicker { get; } = new Ticker() { Interval = TimeSpan.FromSeconds(1) };

    }
}
