using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace PlatformAPI
{
    public class WMSystem
    {
        [DllImport("coredll.dll")]
        public static extern void SystemIdleTimerReset();
    }
}
