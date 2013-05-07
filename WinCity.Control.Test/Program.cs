using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PlatformAPI.GDIPlus;

namespace WinCity.Control.Test
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [MTAThread]
        static void Main()
        {
            IntPtr token = IntPtr.Zero;
            GdiplusStartupOutput output;
            GdiplusStartupInput input = new GdiplusStartupInput();
            NativeMethods.GdiplusStartup(out token, input, out output);
            
            //Application.Run(new LayoutTestForm());
            Application.Run(new ListViewTestForm());
            //Application.Run(new GridLayoutTestForm());

            NativeMethods.GdiplusShutdown(token);
        }
    }
}