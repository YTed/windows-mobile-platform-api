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
GdipDrawLine(GpGraphics graphics, GpPen pen, float x1, float y1,
       float x2, float y2);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipDrawLineI(GpGraphics graphics, GpPen pen, int x1, int y1,
       int x2, int y2);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipDrawLines(GpGraphics graphics, GpPen pen, GpPointF[] points,
        int count);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipDrawLinesI(GpGraphics graphics, GpPen pen, GpPoint[] points,
        int count);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipDrawArc(GpGraphics graphics, GpPen pen, float x, float y,
float width, float height, float startAngle, float sweepAngle);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipDrawArcI(GpGraphics graphics, GpPen pen, int x, int y,
      int width, int height, float startAngle, float sweepAngle);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipDrawBezier(GpGraphics graphics, GpPen pen, float x1, float y1,
         float x2, float y2, float x3, float y3, float x4, float y4);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipDrawBezierI(GpGraphics graphics, GpPen pen, int x1, int y1,
         int x2, int y2, int x3, int y3, int x4, int y4);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipDrawBeziers(GpGraphics graphics, GpPen pen, GpPointF[] points,
          int count);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipDrawBeziersI(GpGraphics graphics, GpPen pen, GpPoint[] points,
          int count);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipDrawRectangle(GpGraphics graphics, GpPen pen, float x, float y,
       float width, float height);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipDrawRectangleI(GpGraphics graphics, GpPen pen, int x, int y,
       int width, int height);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipDrawRectangles(GpGraphics graphics, GpPen pen, GpRectF[] rects,
        int count);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipDrawRectanglesI(GpGraphics graphics, GpPen pen, GpRect[] rects,
        int count);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipDrawEllipse(GpGraphics graphics, GpPen pen, float x, float y,
          float width, float height);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipDrawEllipseI(GpGraphics graphics, GpPen pen, int x, int y,
          int width, int height);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipDrawPie(GpGraphics graphics, GpPen pen, float x, float y,
      float width, float height, float startAngle,
float sweepAngle);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipDrawPieI(GpGraphics graphics, GpPen pen, int x, int y,
      int width, int height, float startAngle, float sweepAngle);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipDrawPolygon(GpGraphics graphics, GpPen pen, GpPointF[] points,
          int count);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipDrawPolygonI(GpGraphics graphics, GpPen pen, GpPoint[] points,
          int count);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipDrawPath(GpGraphics graphics, GpPen pen, GpPath path);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipDrawCurve(GpGraphics graphics, GpPen pen, GpPointF[] points,
        int count);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipDrawCurveI(GpGraphics graphics, GpPen pen, GpPoint[] points,
        int count);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipDrawCurve2(GpGraphics graphics, GpPen pen, GpPointF[] points,
        int count, float tension);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipDrawCurve2I(GpGraphics graphics, GpPen pen, GpPoint[] points,
        int count, float tension);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipDrawCurve3(GpGraphics graphics, GpPen pen, GpPointF[] points,
int count, int offset, int numberOfSegments, float tension);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipDrawCurve3I(GpGraphics graphics, GpPen pen, GpPoint[] points,
 int count, int offset, int numberOfSegments, float tension);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipDrawClosedCurve(GpGraphics graphics, GpPen pen,
      GpPointF[] points, int count);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipDrawClosedCurveI(GpGraphics graphics, GpPen pen,
       GpPoint[] points, int count);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipDrawClosedCurve2(GpGraphics graphics, GpPen pen,
       GpPointF[] points, int count, float tension);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipDrawClosedCurve2I(GpGraphics graphics, GpPen pen,
        GpPoint[] points, int count, float tension);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipGraphicsClear(GpGraphics graphics, int color);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipFillRectangle(GpGraphics graphics, GpBrush brush, float x, float y,
   float width, float height);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipFillRectangleI(GpGraphics graphics, GpBrush brush, int x, int y,
    int width, int height);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipFillRectangles(GpGraphics graphics, GpBrush brush,
     GpRectF[] rects, int count);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipFillRectanglesI(GpGraphics graphics, GpBrush brush,
      GpRect[] rects, int count);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipFillPolygon(GpGraphics graphics, GpBrush brush,
  GpPointF[] points, int count, FillMode fillMode);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipFillPolygon(GpGraphics graphics, GpSolidFill brush,
  GpPointF[] points, int count, FillMode fillMode);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipFillPolygon(GpGraphics graphics, GpHatch brush,
  GpPointF[] points, int count, FillMode fillMode);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipFillPolygon(GpGraphics graphics, GpTexture brush,
  GpPointF[] points, int count, FillMode fillMode);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipFillPolygonI(GpGraphics graphics, GpBrush brush,
   GpPoint[] points, int count, FillMode fillMode);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipFillPolygon2(GpGraphics graphics, GpBrush brush,
   GpPointF[] points, int count);

        [DllImport("gdiplus")]
        extern static public GpStatus
        GdipFillPolygon2I(GpGraphics graphics, GpBrush brush,
        GpPoint[] points, int count);

        [DllImport("gdiplus")]
        extern static public GpStatus
        GdipFillPolygon2I(GpGraphics graphics, GpSolidFill brush,
        GpPoint[] points, int count);

        [DllImport("gdiplus")]
        extern static public GpStatus
        GdipFillEllipse(GpGraphics graphics, GpBrush brush, float x, float y,
                        float width, float height);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipFillEllipseI(GpGraphics graphics, GpBrush brush, int x, int y,
  int width, int height);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipFillPie(GpGraphics graphics, GpBrush brush, float x, float y,
float width, float height, float startAngle, float sweepAngle);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipFillPieI(GpGraphics graphics, GpBrush brush, int x, int y,
int width, int height, float startAngle, float sweepAngle);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipFillPath(GpGraphics graphics, GpBrush brush, GpPath path);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipFillClosedCurve(GpGraphics graphics, GpBrush brush,
                GpPointF[] points, int count);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipFillClosedCurveI(GpGraphics graphics, GpBrush brush,
                GpPoint[] points, int count);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipFillClosedCurve2(GpGraphics graphics, GpBrush brush,
                GpPointF[] points, int count,
               float tension, FillMode fillMode);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipFillClosedCurve2I(GpGraphics graphics, GpBrush brush,
                GpPoint[] points, int count,
               float tension, FillMode fillMode);

        [DllImport("gdiplus")]
        extern static public GpStatus
GdipFillRegion(GpGraphics graphics, GpBrush brush,
         GpRegion region);
    }
}
