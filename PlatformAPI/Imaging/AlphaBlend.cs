using System;
using System.Drawing;
using System.Runtime.InteropServices;


namespace PlatformAPI.Imaging
{

    // These structures, enumerations and p/invoke signatures come from
    // wingdi.h in the Windows Mobile 5.0 Pocket PC SDK

    public struct BlendFunction
    {
        public byte BlendOp;
        public byte BlendFlags;
        public byte SourceConstantAlpha;
        public byte AlphaFormat;
    }

    public enum BlendOperation : byte
    {
        AC_SRC_OVER = 0x00
    }

    public enum BlendFlags : byte
    {
        Zero = 0x00
    }

    public enum SourceConstantAlpha : byte
    {
        Transparent = 0x00,
        Opaque = 0xFF
    }

    public enum AlphaFormat : byte
    {
        AC_SRC_ALPHA = 0x01
    }

    public class DrawingAPI
    {
        [DllImport("coredll.dll")]
        extern public static bool AlphaBlend(IntPtr hdcDest, Int32 nXDest, Int32 nYDest, Int32 nWidthDst, Int32 nHeightDst, IntPtr hdcSrc, Int32 nXSrc, Int32 nYSrc, Int32 nWidthSrc, Int32 nHeightSrc, BlendFunction blendFunction);

        [DllImport("coredll.dll")]
        extern public static bool BitBlt(IntPtr hdcDest, Int32 nXDest, Int32 nYDest, Int32 nWidth, Int32 nHeight, IntPtr hdcSrc, Int32 nXSrc, Int32 nYSrc, UInt32 dwRop);

        public const UInt32 SRCCOPY = 0x00CC0020;
    }


    public static class AlphaBlend
    {
        public static void DrawAlpha(Graphics gx, Bitmap image, byte transp, int x, int y)
        {
            using (Graphics gxSrc = Graphics.FromImage(image))
            {
                IntPtr hdcDst = gx.GetHdc();
                IntPtr hdcSrc = gxSrc.GetHdc();
                BlendFunction blendFunction = new BlendFunction();
                blendFunction.BlendOp = (byte)BlendOperation.AC_SRC_OVER;   // Only supported blend operation
                blendFunction.BlendFlags = (byte)BlendFlags.Zero;           // Documentation says put 0 here
                blendFunction.SourceConstantAlpha = transp;                 // Constant alpha factor
                blendFunction.AlphaFormat = (byte)0;                        // Don't look for per pixel alpha
                DrawingAPI.AlphaBlend(hdcDst, x, y, image.Width, image.Height, hdcSrc, 0, 0, image.Width, image.Height, blendFunction);
                gx.ReleaseHdc(hdcDst);          // Required cleanup to GetHdc()
                gxSrc.ReleaseHdc(hdcSrc);       // Required cleanup to GetHdc()
            }
        }
    }
}
