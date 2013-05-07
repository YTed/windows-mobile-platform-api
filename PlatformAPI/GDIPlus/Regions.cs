using System;
using System.Runtime.InteropServices;
using System.Text;

namespace PlatformAPI.GDIPlus
{
    public static partial class NativeMethods
    {
//----------------------------------------------------------------------------
// Region APIs
//----------------------------------------------------------------------------

[DllImport("gdiplus")] extern static public GpStatus
GdipCreateRegion(out GpRegion region);

[DllImport("gdiplus")] extern static public GpStatus
GdipCreateRegionRect( ref GpRectF rect, out GpRegion region);

[DllImport("gdiplus")] extern static public GpStatus
GdipCreateRegionRectI( ref GpRect rect, out GpRegion region);

[DllImport("gdiplus")] extern static public GpStatus
GdipCreateRegionPath(GpPath path, out GpRegion region);

[DllImport("gdiplus")] extern static public GpStatus
GdipCreateRegionRgnData( byte[] regionData, int size, 
                        out GpRegion region);

[DllImport("gdiplus")] extern static public GpStatus
GdipCreateRegionHrgn(HRGN hRgn, out GpRegion region);

[DllImport("gdiplus")] extern static public GpStatus
GdipCloneRegion(GpRegion region, out GpRegion cloneRegion);

[DllImport("gdiplus")] extern static public GpStatus
GdipDeleteRegion(GpRegion region);

[DllImport("gdiplus")] extern static public GpStatus
GdipSetInfinite(GpRegion region);

[DllImport("gdiplus")] extern static public GpStatus
GdipSetEmpty(GpRegion region);

[DllImport("gdiplus")] extern static public GpStatus
GdipCombineRegionRect(GpRegion region,  ref GpRectF rect,
                      CombineMode combineMode);

[DllImport("gdiplus")] extern static public GpStatus
GdipCombineRegionRectI(GpRegion region,  ref GpRect rect,
                       CombineMode combineMode);

[DllImport("gdiplus")] extern static public GpStatus
GdipCombineRegionPath(GpRegion region, GpPath path, CombineMode combineMode);

[DllImport("gdiplus")] extern static public GpStatus
GdipCombineRegionRegion(GpRegion region,  GpRegion region2,
                        CombineMode combineMode);

[DllImport("gdiplus")] extern static public GpStatus
GdipTranslateRegion(GpRegion region, float dx, float dy);

[DllImport("gdiplus")] extern static public GpStatus
GdipTranslateRegionI(GpRegion region, int dx, int dy);


[DllImport("gdiplus")] extern static public GpStatus
GdipGetRegionBounds(GpRegion region, GpGraphics graphics,
                             out GpRectF rect);

[DllImport("gdiplus")] extern static public GpStatus
GdipGetRegionBoundsI(GpRegion region, GpGraphics graphics,
                             out GpRect rect);

[DllImport("gdiplus")] extern static public GpStatus
GdipGetRegionHRgn(GpRegion region, GpGraphics graphics, out HRGN hRgn);

[DllImport("gdiplus")] extern static public GpStatus
GdipIsEmptyRegion(GpRegion region, GpGraphics graphics,
                           out bool result);

[DllImport("gdiplus")] extern static public GpStatus
GdipIsInfiniteRegion(GpRegion region, GpGraphics graphics,
                              out bool result);

[DllImport("gdiplus")] extern static public GpStatus
GdipIsEqualRegion(GpRegion region, GpRegion region2,
                           GpGraphics graphics, out bool result);

[DllImport("gdiplus")] extern static public GpStatus
GdipGetRegionDataSize(GpRegion region, out uint  bufferSize);

[DllImport("gdiplus")] extern static public GpStatus
GdipGetRegionData(GpRegion region, byte[] buffer, uint bufferSize, 
                  out uint sizeFilled);

[DllImport("gdiplus")] extern static public GpStatus
GdipIsVisibleRegionPoint(GpRegion region, float x, float y,
                                  GpGraphics graphics, out bool result);

[DllImport("gdiplus")] extern static public GpStatus
GdipIsVisibleRegionPointI(GpRegion region, int x, int y,
                                  GpGraphics graphics, out bool result);

[DllImport("gdiplus")] extern static public GpStatus
GdipIsVisibleRegionRect(GpRegion region, float x, float y, float width,
                        float height, GpGraphics graphics, out bool result);

[DllImport("gdiplus")] extern static public GpStatus
GdipIsVisibleRegionRectI(GpRegion region, int x, int y, int width,
                         int height, GpGraphics graphics, out bool result);

[DllImport("gdiplus")] extern static public GpStatus
GdipGetRegionScansCount(GpRegion region, out uint count, GpMatrix matrix);

[DllImport("gdiplus")] extern static public GpStatus
GdipGetRegionScans(GpRegion region, GpRectF[] rects, ref int count, 
                   GpMatrix matrix);

[DllImport("gdiplus")] extern static public GpStatus
GdipGetRegionScansI(GpRegion region, GpRect[] rects, ref int count, 
                    GpMatrix matrix);

    }
}
