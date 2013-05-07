using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;

namespace PlatformAPI.GDIPlus
{
    public static partial class NativeMethods
    {
        [DllImport("gdiplus")]
        extern static public GpStatus
GdipFlush(GpGraphics graphics, FlushIntention intention);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipCreateFromHDC(HDC hdc, out GpGraphics graphics);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipCreateFromHDC2(HDC hdc, IntPtr hDevice, out GpGraphics graphics);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipCreateFromHWND(HWND hwnd, out GpGraphics graphics);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipCreateFromHWNDICM(HWND hwnd, out GpGraphics graphics);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipDeleteGraphics(GpGraphics graphics);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipGetDC(GpGraphics graphics, out HDC hdc);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipReleaseDC(GpGraphics graphics, HDC hdc);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipSetCompositingMode(GpGraphics graphics, CompositingMode compositingMode);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipGetCompositingMode(GpGraphics graphics, out CompositingMode compositingMode);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipSetRenderingOrigin(GpGraphics graphics, int x, int y);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipGetRenderingOrigin(GpGraphics graphics, out int x, out int y);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipSetCompositingQuality(GpGraphics graphics,
           CompositingQuality compositingQuality);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipGetCompositingQuality(GpGraphics graphics,
           out CompositingQuality compositingQuality);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipSetSmoothingMode(GpGraphics graphics, SmoothingMode smoothingMode);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipGetSmoothingMode(GpGraphics graphics, out SmoothingMode smoothingMode);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipSetPixelOffsetMode(GpGraphics graphics, PixelOffsetMode pixelOffsetMode);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipGetPixelOffsetMode(GpGraphics graphics, out PixelOffsetMode pixelOffsetMode);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipSetTextRenderingHint(GpGraphics graphics, TextRenderingHint mode);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipGetTextRenderingHint(GpGraphics graphics, out TextRenderingHint mode);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipSetTextContrast(GpGraphics graphics, uint contrast);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipGetTextContrast(GpGraphics graphics, out uint contrast);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipSetInterpolationMode(GpGraphics graphics,
          InterpolationMode interpolationMode);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipGetInterpolationMode(GpGraphics graphics,
          out InterpolationMode interpolationMode);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipSetWorldTransform(GpGraphics graphics, GpMatrix matrix);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipResetWorldTransform(GpGraphics graphics);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipMultiplyWorldTransform(GpGraphics graphics, GpMatrix matrix,
            MatrixOrder order);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipTranslateWorldTransform(GpGraphics graphics, float dx, float dy,
             MatrixOrder order);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipScaleWorldTransform(GpGraphics graphics, float sx, float sy,
         MatrixOrder order);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipGetClipBounds(GpGraphics graphics, out GpRectF rect);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipGetClipBoundsI(GpGraphics graphics, out GpRect rect);


        [DllImport("gdiplus")]
        public static extern GpStatus
GdipGetPageUnit(GpGraphics graphics, out Unit unit);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipGetPageScale(GpGraphics graphics, out float scale);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipSetPageUnit(GpGraphics graphics, Unit unit);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipSetPageScale(GpGraphics graphics, float scale);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipGetDpiX(GpGraphics graphics, out float dpi);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipGetDpiY(GpGraphics graphics, out float dpi);

        [DllImport("gdiplus")]
        public static extern GpStatus
        GdipSetClipHrgn(GpGraphics graphics, HRGN hRgn, CombineMode combineMode);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipSaveGraphics(GpGraphics graphics, out GraphicsState state);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipRestoreGraphics(GpGraphics graphics, GraphicsState state);

    }


}
