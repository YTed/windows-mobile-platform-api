using System;
using System.Runtime.InteropServices;
using System.Text;

namespace PlatformAPI.GDIPlus
{
    public static partial  class NativeMethods
    {
//----------------------------------------------------------------------------
// GraphicsPath APIs
//----------------------------------------------------------------------------

[DllImport("gdiplus")] public static extern GpStatus 
GdipCreatePath(FillMode brushMode, out GpPath path);

//[DllImport("gdiplus")] public static extern GpStatus 
//GdipCreatePath2(GpPointF[] points, BYTE*, int count, FillMode,
//                                    out GpPath path);

//[DllImport("gdiplus")] public static extern GpStatus 
//GdipCreatePath2I(GpPointF[] points, BYTE*, int count, FillMode,
//                                     out GpPath path);

[DllImport("gdiplus")] public static extern GpStatus 
GdipClonePath(GpPath  path, out GpPath clonePath);

[DllImport("gdiplus")] public static extern GpStatus 
GdipDeletePath(GpPath  path);

[DllImport("gdiplus")] public static extern GpStatus 
GdipResetPath(GpPath  path);

[DllImport("gdiplus")] public static extern GpStatus 
GdipGetPointCount(GpPath  path, out int count);

[DllImport("gdiplus")] public static extern GpStatus 
GdipGetPathTypes(GpPath  path, byte[] types, int count);

[DllImport("gdiplus")] public static extern GpStatus 
GdipGetPathPoints(GpPath path, GpPointF[] points, int count);

[DllImport("gdiplus")] public static extern GpStatus 
GdipGetPathPointsI(GpPath path, GpPoint[] points, int count);

[DllImport("gdiplus")] public static extern GpStatus 
GdipGetPathFillMode(GpPath path, out FillMode fillmode);

[DllImport("gdiplus")] public static extern GpStatus 
GdipSetPathFillMode(GpPath path, FillMode fillmode);

[DllImport("gdiplus")] public static extern GpStatus 
GdipGetPathData(GpPath path, out GpPathData pathData);

[DllImport("gdiplus")] public static extern GpStatus 
GdipStartPathFigure(GpPath path);

[DllImport("gdiplus")] public static extern GpStatus 
GdipClosePathFigure(GpPath path);

[DllImport("gdiplus")] public static extern GpStatus 
GdipClosePathFigures(GpPath path);

[DllImport("gdiplus")] public static extern GpStatus 
GdipSetPathMarker(GpPath  path);

[DllImport("gdiplus")] public static extern GpStatus 
GdipClearPathMarkers(GpPath  path);

[DllImport("gdiplus")] public static extern GpStatus 
GdipReversePath(GpPath  path);

[DllImport("gdiplus")] public static extern GpStatus 
GdipGetPathLastPoint(GpPath  path, out GpPointF lastPoint);

[DllImport("gdiplus")] public static extern GpStatus 
GdipAddPathLine(GpPath path, float x1, float y1, float x2, float y2);

[DllImport("gdiplus")] public static extern GpStatus 
GdipAddPathLine2(GpPath path, GpPointF[] points, int count);

[DllImport("gdiplus")] public static extern GpStatus 
GdipAddPathArc(GpPath path, float x, float y, float width, float height,
                        float startAngle, float sweepAngle);

[DllImport("gdiplus")] public static extern GpStatus 
GdipAddPathBezier(GpPath path, float x1, float y1, float x2, float y2,
                           float x3, float y3, float x4, float y4);

[DllImport("gdiplus")] public static extern GpStatus 
GdipAddPathBeziers(GpPath path, GpPointF[] points, int count);

[DllImport("gdiplus")] public static extern GpStatus 
GdipAddPathCurve(GpPath path, GpPointF[] points, int count);

[DllImport("gdiplus")] public static extern GpStatus 
GdipAddPathCurve2(GpPath path, GpPointF[] points, int count,
                           float tension);

[DllImport("gdiplus")] public static extern GpStatus 
GdipAddPathCurve3(GpPath path, GpPointF[] points, int count,
                           int offset, int numberOfSegments, float tension);

[DllImport("gdiplus")] public static extern GpStatus 
GdipAddPathClosedCurve(GpPath path, GpPointF[] points, int count);

[DllImport("gdiplus")] public static extern GpStatus 
GdipAddPathClosedCurve2(GpPath path, GpPointF[] points, int count,
                                 float tension);

[DllImport("gdiplus")] public static extern GpStatus 
GdipAddPathRectangle(GpPath path, float x, float y, float width, float height);

[DllImport("gdiplus")] public static extern GpStatus 
GdipAddPathRectangles(GpPath path, GpRectF[] rects, int count);

[DllImport("gdiplus")] public static extern GpStatus 
GdipAddPathEllipse(GpPath path, float x, float y, float width,
                            float height);

[DllImport("gdiplus")] public static extern GpStatus 
GdipAddPathPie(GpPath path, float x, float y, float width, float height,
                        float startAngle, float sweepAngle);

[DllImport("gdiplus")] public static extern GpStatus 
GdipAddPathPolygon(GpPath path, GpPointF[] points, int count);

[DllImport("gdiplus")] public static extern GpStatus 
GdipAddPathPath(GpPath path, GpPath  addingPath, bool connect);

[DllImport("gdiplus")] public static extern GpStatus 
GdipAddPathString(GpPath path, string str,
                        int length, GpFontFamily family, int style,
                        float emSize, GpRectF layoutRect,
                        GpStringFormat format);

[DllImport("gdiplus")] public static extern GpStatus 
GdipAddPathStringI(GpPath path, string text,
                        int length, GpFontFamily family, int style,
                        float emSize, GpRect layoutRect,
                        GpStringFormat format);

[DllImport("gdiplus")] public static extern GpStatus 
GdipAddPathLineI(GpPath path, int x1, int y1, int x2, int y2);

[DllImport("gdiplus")] public static extern GpStatus 
GdipAddPathLine2I(GpPath path, GpPoint []points, int count);

[DllImport("gdiplus")] public static extern GpStatus 
GdipAddPathArcI(GpPath path, int x, int y, int width, int height,
                        float startAngle, float sweepAngle);

[DllImport("gdiplus")] public static extern GpStatus 
GdipAddPathBezierI(GpPath path, int x1, int y1, int x2, int y2,
                           int x3, int y3, int x4, int y4);

[DllImport("gdiplus")] public static extern GpStatus 
GdipAddPathBeziersI(GpPath path, GpPoint[] points, int count);

[DllImport("gdiplus")] public static extern GpStatus 
GdipAddPathCurveI(GpPath path, GpPoint[] points, int count);

[DllImport("gdiplus")] public static extern GpStatus 
GdipAddPathCurve2I(GpPath path, GpPoint[] points, int count,
                           float tension);

[DllImport("gdiplus")] public static extern GpStatus 
GdipAddPathCurve3I(GpPath path, GpPoint[] points, int count,
                           int offset, int numberOfSegments, float tension);

[DllImport("gdiplus")] public static extern GpStatus 
GdipAddPathClosedCurveI(GpPath path, GpPoint[] points, int count);

[DllImport("gdiplus")] public static extern GpStatus 
GdipAddPathClosedCurve2I(GpPath path, GpPoint[] points, int count,
                                 float tension);

[DllImport("gdiplus")] public static extern GpStatus 
GdipAddPathRectangleI(GpPath path, int x, int y, int width, int height);

[DllImport("gdiplus")] public static extern GpStatus 
GdipAddPathRectanglesI(GpPath path, GpRect[] rects, int count);

[DllImport("gdiplus")] public static extern GpStatus 
GdipAddPathEllipseI(GpPath path, int x, int y, int width, int height);

[DllImport("gdiplus")] public static extern GpStatus 
GdipAddPathPieI(GpPath path, int x, int y, int width, int height,
                        float startAngle, float sweepAngle);

[DllImport("gdiplus")] public static extern GpStatus 
GdipAddPathPolygonI(GpPath path, GpPoint[] points, int count);

[DllImport("gdiplus")] public static extern GpStatus 
GdipFlattenPath(GpPath path, GpMatrix matrix, float flatness);

[DllImport("gdiplus")] public static extern GpStatus 
GdipWindingModeOutline(
    GpPath path,
    GpMatrix matrix,
    float flatness
);

[DllImport("gdiplus")] public static extern GpStatus 
GdipWidenPath(
    GpPath nativePath,
    GpPen pen,
    GpMatrix matrix,
    float flatness
);

[DllImport("gdiplus")] public static extern GpStatus 
GdipWarpPath(GpPath path, GpMatrix matrix,
            GpPointF[] points, int count,
            float srcx, float srcy, float srcwidth, float srcheight,
            WarpMode warpMode, float flatness);

[DllImport("gdiplus")] public static extern GpStatus 
GdipTransformPath(GpPath  path, GpMatrix matrix);

[DllImport("gdiplus")] public static extern GpStatus 
GdipGetPathWorldBounds(GpPath  path, GpRectF[] bounds, 
                       out GpMatrix matrix, out GpPen pen);

[DllImport("gdiplus")] public static extern GpStatus 
GdipGetPathWorldBoundsI(GpPath  path, GpRect[] bounds, 
                        out GpMatrix matrix, out GpPen pen);

[DllImport("gdiplus")] public static extern GpStatus 
GdipIsVisiblePathPoint(GpPath  path, float x, float y,
                       GpGraphics graphics, out bool result);

[DllImport("gdiplus")] public static extern GpStatus 
GdipIsVisiblePathPointI(GpPath  path, int x, int y,
                        GpGraphics graphics, out bool result);

[DllImport("gdiplus")] public static extern GpStatus 
GdipIsOutlineVisiblePathPoint(GpPath  path, float x, float y, GpPen pen,
                              GpGraphics graphics, out bool result);

[DllImport("gdiplus")] public static extern GpStatus 
GdipIsOutlineVisiblePathPointI(GpPath  path, int x, int y, GpPen pen,
                               GpGraphics graphics, out bool result);
    }
}
