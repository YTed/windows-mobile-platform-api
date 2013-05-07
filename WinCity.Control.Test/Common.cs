using System;
using System.Collections.Generic;
using System.Text;
using PlatformAPI.Imaging;
using System.IO;
using System.Drawing;

namespace WinCity.Control.Test
{
    class Common
    {
        public static AlphaImage GetResource(string rltpath)
        {
            string path = Path.GetDirectoryName(
                typeof(Common).Assembly.GetModules()[0].FullyQualifiedName);
            string abspath = Path.Combine(path, rltpath);
            return AlphaImage.CreateFromFile(abspath);
        }

        public static Image GetResource2(string rltpath)
        {
            string path = Path.GetDirectoryName(
                typeof(Common).Assembly.GetModules()[0].FullyQualifiedName);
            string abspath = Path.Combine(path, rltpath);
            return new Bitmap(abspath);
        }

        public static Color RandomColor()
        {
            return Color.FromArgb(random.Next());
        }

        private static Random random = new Random();
    }
}
