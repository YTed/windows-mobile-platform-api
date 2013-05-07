using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using PlatformAPI.Imaging;

namespace WinCity.Control
{
    class Utility
    {
        public static bool TIAHasComponent(
            TextImageAlignment alignSrc,
            TextImageAlignment alignCmp)
        {
            return (alignSrc & alignCmp) == alignCmp;
        }

        public static string StartupPath
        {
            get
            {
                if (string.IsNullOrEmpty(startupath))
                {
                    startupath = Path.GetDirectoryName(
                        typeof(Utility).Assembly.GetModules()[0].FullyQualifiedName);
                }
                return startupath;
            }
        }

        public static Image GetImage(string relative)
        {
            string basepath = StartupPath;
            string imagefile = Path.Combine(basepath, relative);
            if (File.Exists(imagefile))
            {
                return new Bitmap(imagefile);
            }
            else
            {
                return null;
            }
        }

        public static AlphaImage GetAlphaImage(string relative)
        {
            string basepath = StartupPath;
            string imagefile = Path.Combine(basepath, relative);
            if (File.Exists(imagefile))
            {
                return AlphaImage.CreateFromFile(imagefile);
            }
            else
            {
                return null;
            }
        }

        public static Color GrayScale(Color color)
        {
            int gray = (color.R + color.G + color.B) / 3;
            int alpha = color.A;
            gray =  (alpha << 24) | (gray << 16) | (gray << 8) | gray;
            return Color.FromArgb(gray);
        }
        
        private static string startupath = null;
    }
}
