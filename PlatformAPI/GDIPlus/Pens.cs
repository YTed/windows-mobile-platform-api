using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace PlatformAPI.GDIPlus
{
    public static partial class NativeMethods
    {
        [DllImport("gdiplus")]
        extern static public GpStatus
GdipCreatePen1(int color, float width, Unit unit, out GpPen pen);
        [DllImport("gdiplus")]
        extern static public GpStatus
GdipCreatePen1(int color, float width, Unit unit, out IntPtr hPen);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipCreatePen2(GpBrush brush, float width, Unit unit,
         out GpPen pen);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipClonePen(GpPen pen, out GpPen clonepen);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipDeletePen(GpPen pen);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipSetPenWidth(GpPen pen, float width);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipGetPenWidth(GpPen pen, out float width);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipSetPenUnit(GpPen pen, Unit unit);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipGetPenUnit(GpPen pen, out Unit unit);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipSetPenLineCap197819(GpPen pen, LineCap startCap, LineCap endCap,
   DashCap dashCap);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipSetPenStartCap(GpPen pen, LineCap startCap);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipSetPenEndCap(GpPen pen, LineCap endCap);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipSetPenDashCap197819(GpPen pen, DashCap dashCap);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipGetPenStartCap(GpPen pen, out LineCap startCap);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipGetPenEndCap(GpPen pen, out LineCap endCap);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipGetPenDashCap197819(GpPen pen, out DashCap dashCap);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipSetPenLineJoin(GpPen pen, LineJoin lineJoin);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipGetPenLineJoin(GpPen pen, out LineJoin lineJoin);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipSetPenCustomStartCap(GpPen pen, GpCustomLineCap customCap);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipGetPenCustomStartCap(GpPen pen, out GpCustomLineCap customCap);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipSetPenCustomEndCap(GpPen pen, GpCustomLineCap customCap);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipGetPenCustomEndCap(GpPen pen, out GpCustomLineCap customCap);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipSetPenMiterLimit(GpPen pen, float miterLimit);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipGetPenMiterLimit(GpPen pen, out float miterLimit);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipSetPenMode(GpPen pen, PenAlignment penMode);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipGetPenMode(GpPen pen, out PenAlignment penMode);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipSetPenTransform(GpPen pen, GpMatrix matrix);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipGetPenTransform(GpPen pen, out GpMatrix matrix);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipResetPenTransform(GpPen pen);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipMultiplyPenTransform(GpPen pen, GpMatrix matrix,
            MatrixOrder order);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipTranslatePenTransform(GpPen pen, float dx, float dy,
             MatrixOrder order);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipScalePenTransform(GpPen pen, float sx, float sy,
             MatrixOrder order);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipRotatePenTransform(GpPen pen, float angle, MatrixOrder order);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipSetPenColor(GpPen pen, int argb);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipGetPenColor(GpPen pen, out int argb);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipSetPenBrushFill(GpPen pen, GpBrush brush);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipGetPenBrushFill(GpPen pen, out GpBrush brush);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipGetPenFillType(GpPen pen, out PenType type);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipGetPenDashStyle(GpPen pen, out DashStyle dashstyle);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipSetPenDashStyle(GpPen pen, DashStyle dashstyle);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipGetPenDashOffset(GpPen pen, out float offset);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipSetPenDashOffset(GpPen pen, float offset);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipGetPenDashCount(GpPen pen, out int count);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipSetPenDashArray(GpPen pen, float[] dash, int count);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipGetPenDashArray(GpPen pen, float[] dash, int count);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipGetPenCompoundCount(GpPen pen, out int count);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipSetPenCompoundArray(GpPen pen, float[] dash, int count);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipGetPenCompoundArray(GpPen pen, float[] dash, int count);

    }
}
