using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduledShutdown.Common
{
    public static class ShutdownInterop
    {

        private static Process StartSilent(string filename, string args)
        {
            return Process.Start(new ProcessStartInfo()
            {
                FileName = filename,
                Arguments = args,
                UseShellExecute = true,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            });
        }

        public static Process Cancel()
        {
            return StartSilent("shutdown", "-a");
        }

        public static Process Shutdown(int timeout = 0, bool force = true)
        {
            return StartSilent("shutdown", (force ? "-f" : "") + $" -s -t {timeout}");
        }

        public static Process Reboot(int timeout = 0, bool force = true)
        {
            return StartSilent("shutdown", (force ? "-f" : "") + $" -r -t {timeout}");
        }

        public static Process Logout(int timeout = 0, bool force = true)
        {
            return StartSilent("shutdown", (force ? "-f" : "") + $" -l -t {timeout}");
        }

    }
}
