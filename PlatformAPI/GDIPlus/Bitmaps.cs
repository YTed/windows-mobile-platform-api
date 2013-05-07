using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using PlatformAPI.Runtime.InteropServices.ComTypes;

namespace PlatformAPI.GDIPlus
{
    
    public static partial class NativeMethods
    {
//----------------------------------------------------------------------------
// Bitmap APIs
//----------------------------------------------------------------------------

//[DllImport("gdiplus")] public static extern GpStatus 
//GdipCreateBitmapFromStream(IStream* stream, out GpBitmap bitmap);

//[DllImport("gdiplus")] public static extern GpStatus 
//GdipCreateBitmapFromFile(string filename, out GpBitmap bitmap);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipCreateBitmapFromStreamICM(IStream stream, out GpBitmap bitmap);

[DllImport("gdiplus")] public static extern GpStatus 
GdipCreateBitmapFromFileICM(string filename, out GpBitmap bitmap);

[DllImport("gdiplus")] public static extern GpStatus 
GdipCreateBitmapFromScan0(int width,
                          int height,
                          int stride,
                          PixelFormat format,
                          IntPtr scan0,
                          out GpBitmap  bitmap);

[DllImport("gdiplus")] public static extern GpStatus 
GdipCreateBitmapFromGraphics(int width,
                             int height,
                             GpGraphics target,
                             out GpBitmap bitmap);


//[DllImport("gdiplus")] public static extern GpStatus 
//GdipCreateBitmapFromGdiDib(GDIPCONST BITMAPINFO* gdiBitmapInfo,
//                           VOID* gdiBitmapData,
//                           out GpBitmap  bitmap);

[DllImport("gdiplus")] public static extern GpStatus 
GdipCreateBitmapFromHBITMAP(IntPtr hbm,
                            IntPtr hpal,
                            out GpBitmap  bitmap);

[DllImport("gdiplus")] public static extern GpStatus 
GdipCreateHBITMAPFromBitmap(GpBitmap bitmap,
                            out HBITMAP hbmReturn,
                            int background);


[DllImport("gdiplus")] public static extern GpStatus 
GdipBitmapLockBits(GpBitmap bitmap,
                   GpRect rect,
                   uint flags,
                   PixelFormat format,
                   BitmapData lockedBitmapData);

[DllImport("gdiplus")] public static extern GpStatus 
GdipBitmapUnlockBits(GpBitmap bitmap,
                     BitmapData lockedBitmapData);

[DllImport("gdiplus")] public static extern GpStatus 
GdipBitmapGetPixel(GpBitmap bitmap, int x, int y, out int color);

[DllImport("gdiplus")] public static extern GpStatus 
GdipBitmapSetPixel(GpBitmap bitmap, int x, int y, int color);
    
    
    }
}
