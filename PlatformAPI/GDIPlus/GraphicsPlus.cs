using System;
using System.Collections.Generic;
using System.Text;
using Color = System.Drawing.Color;

namespace PlatformAPI.GDIPlus
{
    public class GraphicsPlus : IDisposable
    {
        public static GraphicsPlus FromHDC(HDC hdc)
        {
            return new GraphicsPlus(hdc);
        }

        public static GraphicsPlus FromHDC(HDC hdc,
                                 HANDLE hdevice)
        {
            return new GraphicsPlus(hdc, hdevice);
        }

        public static GraphicsPlus FromHWND(HWND hwnd,
                                  bool icm)
        {
            return new GraphicsPlus(hwnd, icm);
        }

        public static GraphicsPlus FromImage(ImagePlus image)
        {
            return new GraphicsPlus(image);
        }

        public GraphicsPlus(HDC hdc)
        {
            GpGraphics Graphics = new GpGraphics();

            lastResult = NativeMethods.GdipCreateFromHDC(hdc, out Graphics);

            SetNativeGraphics(Graphics);
        }

        public GraphicsPlus(HDC hdc,
                 HANDLE hdevice)
        {
            GpGraphics Graphics = new GpGraphics();

            lastResult = NativeMethods.GdipCreateFromHDC2(hdc, hdevice, out Graphics);

            SetNativeGraphics(Graphics);
        }

        public GraphicsPlus(HWND hwnd,
                 bool icm)
        {
            GpGraphics Graphics = new GpGraphics();

            if (icm)
            {
                lastResult = NativeMethods.GdipCreateFromHWNDICM(hwnd, out Graphics);
            }
            else
            {
                lastResult = NativeMethods.GdipCreateFromHWND(hwnd, out Graphics);
            }

            SetNativeGraphics(Graphics);
        }

        public GraphicsPlus(ImagePlus image)
        {
            GpGraphics Graphics = new GpGraphics();

            if (image != null)
            {
                lastResult = NativeMethods.GdipGetImageGraphicsContext(
                                                                    image.nativeImage, out Graphics);
            }
            SetNativeGraphics(Graphics);
        }

        ~GraphicsPlus()
        {
            Dispose(false);
        }

        public void Flush(FlushIntention intention)
        {
            NativeMethods.GdipFlush(nativeGraphics, intention);
        }

        //------------------------------------------------------------------------
        // GDI Interop methods
        //------------------------------------------------------------------------

        // Locks the GraphicsPlus until ReleaseDC is called

        public HDC GetHDC()
        {
            HDC hdc;

            SetStatus(NativeMethods.GdipGetDC(nativeGraphics, out hdc));

            return hdc;
        }

        public void ReleaseHDC(HDC hdc)
        {
            SetStatus(NativeMethods.GdipReleaseDC(nativeGraphics, hdc));
        }

        //------------------------------------------------------------------------
        // Rendering modes
        //------------------------------------------------------------------------

        public GpStatus SetRenderingOrigin(int x, int y)
        {
            return SetStatus(
                NativeMethods.GdipSetRenderingOrigin(
                    nativeGraphics, x, y
                )
            );
        }

        public GpStatus GetRenderingOrigin(out int x, out int y)
        {
            return SetStatus(
                NativeMethods.GdipGetRenderingOrigin(
                    nativeGraphics, out x, out y
                )
            );
        }

        //public GpStatus SetCompositingMode(CompositingMode compositingMode)
        //{
        //    return SetStatus(NativeMethods.GdipSetCompositingMode(nativeGraphics,
        //                                                        compositingMode));
        //}

        //public CompositingMode GetCompositingMode()
        //{
        //    CompositingMode mode;

        //    SetStatus(NativeMethods.GdipGetCompositingMode(nativeGraphics,
        //                                                 out mode));

        //    return mode;
        //}

        //public GpStatus SetCompositingQuality(CompositingQuality compositingQuality)
        //{
        //    return SetStatus(NativeMethods.GdipSetCompositingQuality(
        //        nativeGraphics,
        //        compositingQuality));
        //}

        //public CompositingQuality GetCompositingQuality()
        //{
        //    CompositingQuality quality;

        //    SetStatus(NativeMethods.GdipGetCompositingQuality(
        //        nativeGraphics,
        //        out quality));

        //    return quality;
        //}

        //public GpStatus SetTextRenderingHint(TextRenderingHint newMode)
        //{
        //    return SetStatus(NativeMethods.GdipSetTextRenderingHint(nativeGraphics,
        //                                                      newMode));
        //}

        //public TextRenderingHint GetTextRenderingHint()
        //{
        //    TextRenderingHint hint;

        //    SetStatus(NativeMethods.GdipGetTextRenderingHint(nativeGraphics,
        //                                               out hint));

        //    return hint;
        //}

        //public GpStatus SetTextContrast(uint contrast)
        //{
        //    return SetStatus(NativeMethods.GdipSetTextContrast(nativeGraphics,
        //                                                      contrast));
        //}

        //public uint GetTextContrast()
        //{
        //    uint contrast;

        //    SetStatus(NativeMethods.GdipGetTextContrast(nativeGraphics,
        //                                                out contrast));

        //    return contrast;
        //}

        //public InterpolationMode GetInterpolationMode()
        //{
        //    InterpolationMode mode = InterpolationMode.InterpolationModeInvalid;

        //    SetStatus(NativeMethods.GdipGetInterpolationMode(nativeGraphics,
        //                                                       out mode));

        //    return mode;
        //}

        //public GpStatus SetInterpolationMode(InterpolationMode interpolationMode)
        //{
        //    return SetStatus(NativeMethods.GdipSetInterpolationMode(nativeGraphics,
        //                                                       interpolationMode));
        //}


        public SmoothingMode GetSmoothingMode()
        {
            SmoothingMode smoothingMode = SmoothingMode.SmoothingModeInvalid;

            SetStatus(NativeMethods.GdipGetSmoothingMode(nativeGraphics,
                                                       out smoothingMode));

            return smoothingMode;
        }

        public GpStatus SetSmoothingMode(SmoothingMode smoothingMode)
        {
            return SetStatus(NativeMethods.GdipSetSmoothingMode(nativeGraphics,
                                                              smoothingMode));
        }

        //public PixelOffsetMode GetPixelOffsetMode()
        //{
        //    PixelOffsetMode pixelOffsetMode = PixelOffsetMode.PixelOffsetModeInvalid;

        //    SetStatus(NativeMethods.GdipGetPixelOffsetMode(nativeGraphics,
        //                                                 out pixelOffsetMode));

        //    return pixelOffsetMode;
        //}

        //public GpStatus SetPixelOffsetMode(PixelOffsetMode pixelOffsetMode)
        //{
        //    return SetStatus(NativeMethods.GdipSetPixelOffsetMode(nativeGraphics,
        //                                                        pixelOffsetMode));
        //}

        //public GpStatus SetPageUnit(Unit unit)
        //{
        //    return SetStatus(NativeMethods.GdipSetPageUnit(nativeGraphics,
        //                                                 unit));
        //}

        //public Unit GetPageUnit()
        //{
        //    Unit unit;

        //    SetStatus(NativeMethods.GdipGetPageUnit(nativeGraphics, out unit));

        //    return unit;
        //}

        //public float GetPageScale()
        //{
        //    float scale;

        //    SetStatus(NativeMethods.GdipGetPageScale(nativeGraphics, out scale));

        //    return scale;
        //}

        //public float GetDpiX()
        //{
        //    float dpi;

        //    SetStatus(NativeMethods.GdipGetDpiX(nativeGraphics, out dpi));

        //    return dpi;
        //}

        //public float GetDpiY()
        //{
        //    float dpi;

        //    SetStatus(NativeMethods.GdipGetDpiY(nativeGraphics, out dpi));

        //    return dpi;
        //}



        public GpStatus DrawLine(PenPlus pen,
                        float x1,
                        float y1,
                        float x2,
                        float y2)
        {
            return SetStatus(NativeMethods.GdipDrawLine(nativeGraphics,
                                                      pen.nativePen, x1, y1, x2,
                                                      y2));
        }

        public GpStatus DrawLine(PenPlus pen,
                        GpPointF pt1,
                        GpPointF pt2)
        {
            return DrawLine(pen, pt1.X, pt1.Y, pt2.X, pt2.Y);
        }

        public GpStatus DrawLines(PenPlus pen, GpPointF[] points)
        {
            return SetStatus(NativeMethods.GdipDrawLines(nativeGraphics,
                                                       pen.nativePen,
                                                       points, points.Length));
        }

        public GpStatus DrawLine(PenPlus pen,
                        int x1,
                        int y1,
                        int x2,
                        int y2)
        {
            return SetStatus(NativeMethods.GdipDrawLineI(nativeGraphics,
                                                       pen.nativePen,
                                                       x1,
                                                       y1,
                                                       x2,
                                                       y2));
        }

        public GpStatus DrawLine(PenPlus pen,
                        GpPoint pt1,
                        GpPoint pt2)
        {
            return DrawLine(pen,
                            pt1.X,
                            pt1.Y,
                            pt2.X,
                            pt2.Y);
        }

        public GpStatus DrawLines(PenPlus pen,
                          GpPoint[] points)
        {
            return SetStatus(NativeMethods.GdipDrawLinesI(nativeGraphics,
                                                        pen.nativePen,
                                                        points,
                                                        points.Length));
        }

        public GpStatus DrawArc(PenPlus pen,
                       float x,
                       float y,
                       float width,
                       float height,
                       float startAngle,
                       float sweepAngle)
        {
            return SetStatus(NativeMethods.GdipDrawArc(nativeGraphics,
                                                     pen.nativePen,
                                                     x,
                                                     y,
                                                     width,
                                                     height,
                                                     startAngle,
                                                     sweepAngle));
        }

        public GpStatus DrawArc(PenPlus pen,
                        GpRectF rect,
                       float startAngle,
                       float sweepAngle)
        {
            return DrawArc(pen, rect.X, rect.Y, rect.Width, rect.Height,
                           startAngle, sweepAngle);
        }

        public GpStatus DrawArc(PenPlus pen,
                       int x,
                       int y,
                       int width,
                       int height,
                       float startAngle,
                       float sweepAngle)
        {
            return SetStatus(NativeMethods.GdipDrawArcI(nativeGraphics,
                                                      pen.nativePen,
                                                      x,
                                                      y,
                                                      width,
                                                      height,
                                                      startAngle,
                                                      sweepAngle));
        }


        public GpStatus DrawArc(PenPlus pen,
                        GpRect rect,
                       float startAngle,
                       float sweepAngle)
        {
            return DrawArc(pen,
                           rect.X,
                           rect.Y,
                           rect.Width,
                           rect.Height,
                           startAngle,
                           sweepAngle);
        }

        public GpStatus DrawBezier(PenPlus pen,
                          float x1,
                          float y1,
                          float x2,
                          float y2,
                          float x3,
                          float y3,
                          float x4,
                          float y4)
        {
            return SetStatus(NativeMethods.GdipDrawBezier(nativeGraphics,
                                                        pen.nativePen, x1, y1,
                                                        x2, y2, x3, y3, x4, y4));
        }

        public GpStatus DrawBezier(PenPlus pen,
                           GpPointF pt1,
                           GpPointF pt2,
                           GpPointF pt3,
                           GpPointF pt4)
        {
            return DrawBezier(pen,
                              pt1.X,
                              pt1.Y,
                              pt2.X,
                              pt2.Y,
                              pt3.X,
                              pt3.Y,
                              pt4.X,
                              pt4.Y);
        }

        public GpStatus DrawBeziers(PenPlus pen,
                            GpPointF[] points)
        {
            return SetStatus(NativeMethods.GdipDrawBeziers(nativeGraphics,
                                                         pen.nativePen,
                                                         points,
                                                         points.Length));
        }

        public GpStatus DrawBezier(PenPlus pen,
                          int x1,
                          int y1,
                          int x2,
                          int y2,
                          int x3,
                          int y3,
                          int x4,
                          int y4)
        {
            return SetStatus(NativeMethods.GdipDrawBezierI(nativeGraphics,
                                                         pen.nativePen,
                                                         x1,
                                                         y1,
                                                         x2,
                                                         y2,
                                                         x3,
                                                         y3,
                                                         x4,
                                                         y4));
        }

        public GpStatus DrawBezier(PenPlus pen,
                           GpPoint pt1,
                           GpPoint pt2,
                           GpPoint pt3,
                           GpPoint pt4)
        {
            return DrawBezier(pen,
                              pt1.X,
                              pt1.Y,
                              pt2.X,
                              pt2.Y,
                              pt3.X,
                              pt3.Y,
                              pt4.X,
                              pt4.Y);
        }

        public GpStatus DrawBeziers(PenPlus pen,
                            GpPoint[] points)
        {
            return SetStatus(NativeMethods.GdipDrawBeziersI(nativeGraphics,
                                                          pen.nativePen,
                                                          points,
                                                          points.Length));
        }

        public GpStatus DrawRectangle(PenPlus pen,
                              GpRectF rect)
        {
            return DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
        }

        public GpStatus DrawRectangle(PenPlus pen,
                             float x,
                             float y,
                             float width,
                             float height)
        {
            return SetStatus(NativeMethods.GdipDrawRectangle(nativeGraphics,
                                                           pen.nativePen, x, y,
                                                           width, height));
        }

        public GpStatus DrawRectangles(PenPlus pen,
                               GpRectF[] rects)
        {
            return SetStatus(NativeMethods.GdipDrawRectangles(nativeGraphics,
                                                            pen.nativePen,
                                                            rects, rects.Length));
        }

        public GpStatus DrawRectangle(PenPlus pen,
                              GpRect rect)
        {
            return DrawRectangle(pen,
                                 rect.X,
                                 rect.Y,
                                 rect.Width,
                                 rect.Height);
        }

        public GpStatus DrawRectangle(PenPlus pen,
                             int x,
                             int y,
                             int width,
                             int height)
        {
            return SetStatus(NativeMethods.GdipDrawRectangleI(nativeGraphics,
                                                            pen.nativePen,
                                                            x,
                                                            y,
                                                            width,
                                                            height));
        }

        public GpStatus DrawRectangles(PenPlus pen,
                               GpRect[] rects)
        {
            return SetStatus(NativeMethods.GdipDrawRectanglesI(nativeGraphics,
                                                             pen.nativePen,
                                                             rects,
                                                             rects.Length));
        }

        public GpStatus DrawEllipse(PenPlus pen,
                            GpRectF rect)
        {
            return DrawEllipse(pen, rect.X, rect.Y, rect.Width, rect.Height);
        }

        public GpStatus DrawEllipse(PenPlus pen,
                           float x,
                           float y,
                           float width,
                           float height)
        {
            return SetStatus(NativeMethods.GdipDrawEllipse(nativeGraphics,
                                                         pen.nativePen,
                                                         x,
                                                         y,
                                                         width,
                                                         height));
        }

        public GpStatus DrawEllipse(PenPlus pen,
                            GpRect rect)
        {
            return DrawEllipse(pen,
                               rect.X,
                               rect.Y,
                               rect.Width,
                               rect.Height);
        }

        public GpStatus DrawEllipse(PenPlus pen,
                           int x,
                           int y,
                           int width,
                           int height)
        {
            return SetStatus(NativeMethods.GdipDrawEllipseI(nativeGraphics,
                                                          pen.nativePen,
                                                          x,
                                                          y,
                                                          width,
                                                          height));
        }

        public GpStatus DrawPie(PenPlus pen,
                        GpRectF rect,
                       float startAngle,
                       float sweepAngle)
        {
            return DrawPie(pen,
                           rect.X,
                           rect.Y,
                           rect.Width,
                           rect.Height,
                           startAngle,
                           sweepAngle);
        }

        public GpStatus DrawPie(PenPlus pen,
                       float x,
                       float y,
                       float width,
                       float height,
                       float startAngle,
                       float sweepAngle)
        {
            return SetStatus(NativeMethods.GdipDrawPie(nativeGraphics,
                                                     pen.nativePen,
                                                     x,
                                                     y,
                                                     width,
                                                     height,
                                                     startAngle,
                                                     sweepAngle));
        }

        public GpStatus DrawPie(PenPlus pen,
                        GpRect rect,
                       float startAngle,
                       float sweepAngle)
        {
            return DrawPie(pen,
                           rect.X,
                           rect.Y,
                           rect.Width,
                           rect.Height,
                           startAngle,
                           sweepAngle);
        }

        public GpStatus DrawPie(PenPlus pen,
                       int x,
                       int y,
                       int width,
                       int height,
                       float startAngle,
                       float sweepAngle)
        {
            return SetStatus(NativeMethods.GdipDrawPieI(nativeGraphics,
                                                      pen.nativePen,
                                                      x,
                                                      y,
                                                      width,
                                                      height,
                                                      startAngle,
                                                      sweepAngle));
        }

        public GpStatus DrawPolygon(PenPlus pen,
                            GpPointF[] points)
        {
            return SetStatus(NativeMethods.GdipDrawPolygon(nativeGraphics,
                                                         pen.nativePen,
                                                         points,
                                                         points.Length));
        }

        public GpStatus DrawPolygon(PenPlus pen,
                            GpPoint[] points)
        {
            return SetStatus(NativeMethods.GdipDrawPolygonI(nativeGraphics,
                                                          pen.nativePen,
                                                          points,
                                                          points.Length));
        }

        public GpStatus DrawPath(PenPlus pen,
                         GraphicsPath path)
        {
            return SetStatus(NativeMethods.GdipDrawPath(nativeGraphics,
                                                      pen != null ? pen.nativePen : null,
                                                      path != null ? path.nativePath : null));
        }

        public GpStatus DrawCurve(PenPlus pen,
                          GpPointF[] points)
        {
            return SetStatus(NativeMethods.GdipDrawCurve(nativeGraphics,
                                                       pen.nativePen, points,
                                                       points.Length));
        }

        public GpStatus DrawCurve(PenPlus pen,
                          GpPointF[] points,
                         float tension)
        {
            return SetStatus(NativeMethods.GdipDrawCurve2(nativeGraphics,
                                                        pen.nativePen, points,
                                                        points.Length, tension));
        }

        public GpStatus DrawCurve(PenPlus pen,
                          GpPointF[] points,
                         int offset,
                         int numberOfSegments,
                         float tension)
        {
            return SetStatus(NativeMethods.GdipDrawCurve3(nativeGraphics,
                                                        pen.nativePen, points,
                                                        points.Length, offset,
                                                        numberOfSegments, tension));
        }

        public GpStatus DrawCurve(PenPlus pen,
                          GpPoint[] points)
        {
            return SetStatus(NativeMethods.GdipDrawCurveI(nativeGraphics,
                                                        pen.nativePen,
                                                        points,
                                                        points.Length));
        }

        public GpStatus DrawCurve(PenPlus pen,
                          GpPoint[] points,
                         float tension)
        {
            return SetStatus(NativeMethods.GdipDrawCurve2I(nativeGraphics,
                                                         pen.nativePen,
                                                         points,
                                                         points.Length,
                                                         tension));
        }

        public GpStatus DrawCurve(PenPlus pen,
                          GpPoint[] points,
                         int offset,
                         int numberOfSegments,
                         float tension)
        {
            return SetStatus(NativeMethods.GdipDrawCurve3I(nativeGraphics,
                                                         pen.nativePen,
                                                         points,
                                                         points.Length,
                                                         offset,
                                                         numberOfSegments,
                                                         tension));
        }

        public GpStatus DrawClosedCurve(PenPlus pen,
                                GpPointF[] points)
        {
            return SetStatus(NativeMethods.GdipDrawClosedCurve(nativeGraphics,
                                                             pen.nativePen,
                                                             points, points.Length));
        }

        public GpStatus DrawClosedCurve(PenPlus pen,
                                GpPointF[] points,
                               float tension)
        {
            return SetStatus(NativeMethods.GdipDrawClosedCurve2(nativeGraphics,
                                                              pen.nativePen,
                                                              points, points.Length,
                                                              tension));
        }

        public GpStatus DrawClosedCurve(PenPlus pen,
                                GpPoint[] points)
        {
            return SetStatus(NativeMethods.GdipDrawClosedCurveI(nativeGraphics,
                                                              pen.nativePen,
                                                              points,
                                                              points.Length));
        }

        public GpStatus DrawClosedCurve(PenPlus pen,
                                GpPoint[] points,
                               float tension)
        {
            return SetStatus(NativeMethods.GdipDrawClosedCurve2I(nativeGraphics,
                                                               pen.nativePen,
                                                               points,
                                                               points.Length,
                                                               tension));
        }

        //GpStatus Clear( Color color)
        //{
        //    return SetStatus(NativeMethods.GdipGraphicsPlusClear(
        //        nativeGraphics,
        //        color.ToArgb()));
        //}

        public GpStatus FillRectangle(BrushPlus brush,
                              GpRectF rect)
        {
            return FillRectangle(brush, rect.X, rect.Y, rect.Width, rect.Height);
        }

        public GpStatus FillRectangle(BrushPlus brush,
                             float x,
                             float y,
                             float width,
                             float height)
        {
            return SetStatus(NativeMethods.GdipFillRectangle(nativeGraphics,
                                                           brush.nativeBrush, x, y,
                                                           width, height));
        }

        public GpStatus FillRectangles(BrushPlus brush,
                               GpRectF[] rects)
        {
            return SetStatus(NativeMethods.GdipFillRectangles(nativeGraphics,
                                                            brush.nativeBrush,
                                                            rects, rects.Length));
        }

        public GpStatus FillRectangle(BrushPlus brush,
                              GpRect rect)
        {
            return FillRectangle(brush,
                                 rect.X,
                                 rect.Y,
                                 rect.Width,
                                 rect.Height);
        }

        public GpStatus FillRectangle(BrushPlus brush,
                             int x,
                             int y,
                             int width,
                             int height)
        {
            return SetStatus(NativeMethods.GdipFillRectangleI(nativeGraphics,
                                                            brush.nativeBrush,
                                                            x,
                                                            y,
                                                            width,
                                                            height));
        }

        public GpStatus FillRectangles(BrushPlus brush,
                               GpRect[] rects)
        {
            return SetStatus(NativeMethods.GdipFillRectanglesI(nativeGraphics,
                                                             brush.nativeBrush,
                                                             rects,
                                                             rects.Length));
        }

        public GpStatus FillPolygon(BrushPlus brush,
                            GpPointF[] points)
        {
            return FillPolygon(brush, points, points.Length, FillMode.FillModeAlternate);
        }

        public GpStatus FillPolygon(BrushPlus brush,
                            GpPointF[] points,
                           int count,
                           FillMode fillMode)
        {
            return SetStatus(NativeMethods.GdipFillPolygon(nativeGraphics,
                                                         brush.nativeBrush,
                                                         points, points.Length, fillMode));
        }

        public GpStatus FillPolygon(BrushPlus brush,
                            GpPoint[] points)
        {
            return FillPolygon(brush, points, FillMode.FillModeAlternate);
        }

        public GpStatus FillPolygon(BrushPlus brush,
                            GpPoint[] points,
                           FillMode fillMode)
        {
            return SetStatus(NativeMethods.GdipFillPolygonI(nativeGraphics,
                                                          brush.nativeBrush,
                                                          points, points.Length,
                                                          fillMode));
        }

        public GpStatus FillEllipse(BrushPlus brush,
                            GpRectF rect)
        {
            return FillEllipse(brush, rect.X, rect.Y, rect.Width, rect.Height);
        }

        public GpStatus FillEllipse(BrushPlus brush,
                           float x,
                           float y,
                           float width,
                           float height)
        {
            return SetStatus(NativeMethods.GdipFillEllipse(nativeGraphics,
                                                         brush.nativeBrush, x, y,
                                                         width, height));
        }

        public GpStatus FillEllipse(BrushPlus brush,
                            GpRect rect)
        {
            return FillEllipse(brush, rect.X, rect.Y, rect.Width, rect.Height);
        }

        public GpStatus FillEllipse(BrushPlus brush,
                           int x,
                           int y,
                           int width,
                           int height)
        {
            return SetStatus(NativeMethods.GdipFillEllipseI(nativeGraphics,
                                                          brush.nativeBrush,
                                                          x,
                                                          y,
                                                          width,
                                                          height));
        }

        public GpStatus FillPie(BrushPlus brush,
                        GpRectF rect,
                       float startAngle,
                       float sweepAngle)
        {
            return FillPie(brush, rect.X, rect.Y, rect.Width, rect.Height,
                           startAngle, sweepAngle);
        }

        public GpStatus FillPie(BrushPlus brush,
                       float x,
                       float y,
                       float width,
                       float height,
                       float startAngle,
                       float sweepAngle)
        {
            return SetStatus(NativeMethods.GdipFillPie(nativeGraphics,
                                                     brush.nativeBrush, x, y,
                                                     width, height, startAngle,
                                                     sweepAngle));
        }

        public GpStatus FillPie(BrushPlus brush,
                        GpRect rect,
                       float startAngle,
                       float sweepAngle)
        {
            return FillPie(brush, rect.X, rect.Y, rect.Width, rect.Height,
                           startAngle, sweepAngle);
        }

        public GpStatus FillPie(BrushPlus brush,
                       int x,
                       int y,
                       int width,
                       int height,
                       float startAngle,
                       float sweepAngle)
        {
            return SetStatus(NativeMethods.GdipFillPieI(nativeGraphics,
                                                      brush.nativeBrush,
                                                      x,
                                                      y,
                                                      width,
                                                      height,
                                                      startAngle,
                                                      sweepAngle));
        }

        public GpStatus FillPath(BrushPlus brush,
                         GraphicsPath path)
        {
            return SetStatus(NativeMethods.GdipFillPath(nativeGraphics,
                                                      brush.nativeBrush,
                                                      path.nativePath));
        }

        public GpStatus FillClosedCurve(BrushPlus brush,
                                GpPointF[] points)
        {
            return SetStatus(NativeMethods.GdipFillClosedCurve(nativeGraphics,
                                                             brush.nativeBrush,
                                                             points, points.Length));

        }

        public GpStatus FillClosedCurve(BrushPlus brush,
                                GpPointF[] points,
                               FillMode fillMode,
                               float tension)
        {
            return SetStatus(NativeMethods.GdipFillClosedCurve2(nativeGraphics,
                                                              brush.nativeBrush,
                                                              points, points.Length,
                                                              tension, fillMode));
        }

        public GpStatus FillClosedCurve(BrushPlus brush,
                                GpPoint[] points)
        {
            return SetStatus(NativeMethods.GdipFillClosedCurveI(nativeGraphics,
                                                              brush.nativeBrush,
                                                              points,
                                                              points.Length));
        }

        public GpStatus FillClosedCurve(BrushPlus brush,
                                GpPoint[] points,
                               FillMode fillMode,
                               float tension)
        {
            return SetStatus(NativeMethods.GdipFillClosedCurve2I(nativeGraphics,
                                                               brush.nativeBrush,
                                                               points, points.Length,
                                                               tension, fillMode));
        }

        public GpStatus FillRegion(BrushPlus brush,
                           RegionPlus region)
        {
            return SetStatus(NativeMethods.GdipFillRegion(nativeGraphics,
                                                        brush.nativeBrush,
                                                        region.nativeRegion));
        }

        //GpStatus
        //DrawString(
        //     string text,
        //     int           length,
        //     FontPlus         font,
        //     GpRectF        layoutRect,
        //     StringFormatPlus stringFormat,
        //     BrushPlus        brush
        //)
        //{
        //    return SetStatus(NativeMethods.GdipDrawString(
        //        nativeGraphics,
        //        text,
        //        length,
        //        font != null? font.nativeFont : null,
        //        layoutRect,
        //        stringFormat != null ? stringFormat.nativeFormat : null,
        //        brush != null? brush.nativeBrush : null
        //    ));
        //}

        //GpStatus
        //DrawString(
        //     string text,
        //    int                 length,
        //     FontPlus         font,
        //     GpPointF       origin,
        //     BrushPlus        brush
        //)
        //{
        //    GpRectF rect = new GpRectF(origin.X, origin.Y, 0.0f, 0.0f);

        //    return SetStatus(NativeMethods.GdipDrawString(
        //        nativeGraphics,
        //        text,
        //        length,
        //        font != null? font.nativeFont : null,
        //        rect,
        //        null,
        //        brush != null? brush.nativeBrush : null
        //    ));
        //}

        //GpStatus
        //DrawString(
        //     string text,
        //     int                 length,
        //     FontPlus         font,
        //     GpPointF       origin,
        //     StringFormatPlus stringFormat,
        //     BrushPlus        brush
        //)
        //{
        //    GpRectF rect = new GpRectF(origin.X, origin.Y, 0.0f, 0.0f);

        //    return SetStatus(NativeMethods.GdipDrawString(
        //        nativeGraphics,
        //        text,
        //        length,
        //        font != null? font.nativeFont : null,
        //        &rect,
        //        stringFormat != null? stringFormat.nativeFormat : null,
        //        brush != null? brush.nativeBrush : null
        //    ));
        //}

        //GpStatus
        //MeasureString(
        //     string text,
        //     int    length,
        //     FontPlus         font,
        //     GpRectF        layoutRect,
        //     StringFormatPlus stringFormat,
        //    out GpRectF             boundingBox,
        //    out int               codepointsFitted,
        //    out int               linesFilled
        //)
        //{
        //    return SetStatus(NativeMethods.GdipMeasureString(
        //        nativeGraphics,
        //        text,
        //        length,
        //        font != null? font.nativeFont : null,
        //        layoutRect,
        //        stringFormat != null? stringFormat.nativeFormat : null,
        //        boundingBox,
        //        codepointsFitted,
        //        linesFilled
        //    ));
        //}

        //GpStatus
        //MeasureString(
        //     string text,
        //    int                 length,
        //     FontPlus         font,
        //     GpSizeF        layoutRectSize,
        //     StringFormatPlus stringFormat,
        //    out GpSizeF             size,
        //    out int               codepointsFitted,
        //    out int               linesFilled
        //)
        //{
        //    GpRectF   layoutRect= new GpRectF(0, 0, layoutRectSize.Width, layoutRectSize.Height);
        //    GpRectF   boundingBox;
        //    GpStatus  status;

        //    if (size == null)
        //    {
        //        return SetStatus(InvalidParameter);
        //    }

        //    status = SetStatus(NativeMethods.GdipMeasureString(
        //        nativeGraphics,
        //        text,
        //        length,
        //        font != null? font.nativeFont : null,
        //        layoutRect,
        //        stringFormat != null? stringFormat.nativeFormat : null,
        //        size != null? boundingBox : null,
        //        codepointsFitted,
        //        linesFilled
        //    ));

        //    if (size != null && status == GpStatus.Ok)
        //    {
        //        size.Width  = boundingBox.Width;
        //        size.Height = boundingBox.Height;
        //    }

        //    return status;
        //}

        //GpStatus
        //MeasureString(
        //     string text,
        //    int                 length,
        //     FontPlus         font,
        //     GpPointF       origin,
        //     StringFormatPlus stringFormat,
        //    out GpRectF             boundingBox
        //)
        //{
        //    GpRectF rect = new GpRectF(origin.X, origin.Y, 0.0f, 0.0f);

        //    return SetStatus(NativeMethods.GdipMeasureString(
        //        nativeGraphics,
        //        text,
        //        length,
        //        font != null? font.nativeFont : null,
        //        out rect,
        //        stringFormat != null? stringFormat.nativeFormat : null,
        //        boundingBox,
        //        null,
        //        null
        //    ));
        //}


        //GpStatus
        //MeasureCharacterRanges(
        //    string text,
        //    int                 length,
        //     FontPlus         font,
        //     GpRectF        layoutRect,
        //     StringFormatPlus stringFormat,
        //    int                 regionCount,
        //    RegionPlus[]            regions
        //)
        //{
        //    if (!regions || regionCount <= 0)
        //    {
        //        return InvalidParameter;
        //    }

        //    GpRegion[] nativeRegions = new GpRegion [regionCount];


        //    for (int i = 0; i < regionCount; i++)
        //    {
        //        nativeRegions[i] = regions[i].nativeRegion;
        //    }

        //    GpStatus status = SetStatus(NativeMethods.GdipMeasureCharacterRanges(
        //        nativeGraphics,
        //        text,
        //        length,
        //        font != null? font.nativeFont : null,
        //        layoutRect,
        //        stringFormat != null? stringFormat.nativeFormat : null,
        //        regionCount,
        //        nativeRegions
        //    ));


        //    return status;
        //}


        public GpStatus DrawImage(ImagePlus image,
                          GpPointF point)
        {
            return DrawImage(image, point.X, point.Y);
        }

        public GpStatus DrawImage(ImagePlus image,
                         float x,
                         float y)
        {
            return SetStatus(NativeMethods.GdipDrawImage(nativeGraphics,
                                                       image != null ? image.nativeImage
                                                             : null,
                                                       x,
                                                       y));
        }

        public GpStatus DrawImage(ImagePlus image,
                          GpRectF rect)
        {
            GpRectF bounds; Unit unit;
            image.GetBounds(out bounds, out unit);
            return DrawImage(image, rect, 0, 0, bounds.Width, bounds.Height, unit, null);
        }

        public GpStatus DrawImage(ImagePlus image,
                         float x,
                         float y,
                         float width,
                         float height)
        {
            GpRectF bounds; Unit unit;
            image.GetBounds(out bounds, out unit);
            return DrawImage(image, new GpRectF(x, y, width, height), 0, 0, bounds.Width, bounds.Height, unit, null);
        }

        public GpStatus DrawImage(ImagePlus image,
                          GpPoint point)
        {
            return DrawImage(image, point.X, point.Y);
        }

        public GpStatus DrawImage(ImagePlus image,
                         int x,
                         int y)
        {
            GpRectF bounds; Unit unit;
            image.GetBounds(out bounds, out unit);
            bounds.Offset(x, y);
            return DrawImage(image, bounds, 0, 0, bounds.Width, bounds.Height, unit, null);
        }

        public GpStatus DrawImage(ImagePlus image,
                          GpRect rect)
        {
            return DrawImage(image,
                             rect.X,
                             rect.Y,
                             rect.Width,
                             rect.Height);
        }

        public GpStatus DrawImage(ImagePlus image,
                         int x,
                         int y,
                         int width,
                         int height)
        {
            return DrawImage(image, new GpRectF(x, y, width, height));
        }


        public GpStatus DrawImage(ImagePlus image,
                          GpPointF[] destPoints)
        {
            int count = destPoints.Length;

            if (count != 3 && count != 4)
                return SetStatus(GpStatus.InvalidParameter);

            return SetStatus(NativeMethods.GdipDrawImagePoints(nativeGraphics,
                                                             image != null ? image.nativeImage
                                                                   : new GpImage(),
                                                             destPoints, count));
        }

        public GpStatus DrawImage(ImagePlus image,
                          GpPoint[] destPoints)
        {
            int count = destPoints.Length;
            if (count != 3 && count != 4)
                return SetStatus(GpStatus.InvalidParameter);

            return SetStatus(NativeMethods.GdipDrawImagePointsI(nativeGraphics,
                                                              image != null ? image.nativeImage
                                                                    : null,
                                                              destPoints,
                                                              count));
        }

        public GpStatus DrawImage(ImagePlus image,
                         float x,
                         float y,
                         float srcx,
                         float srcy,
                         float srcwidth,
                         float srcheight,
                         Unit srcUnit)
        {
            GpRectF bounds; Unit unit;
            image.GetBounds(out bounds, out unit);
            return DrawImage(image, new GpRectF(x, y, srcwidth, srcheight), srcx, srcy, srcwidth, srcheight, srcUnit, null);
        }

        public GpStatus DrawImage(ImagePlus image,
                          GpRectF destRect,
                         float srcx,
                         float srcy,
                         float srcwidth,
                         float srcheight,
                         Unit srcUnit,
                          ImageAttributesPlus imageAttributes)
        {
            return SetStatus(NativeMethods.GdipDrawImageRectRect(nativeGraphics,
                                                               image != null ? image.nativeImage
                                                                     : null,
                                                               destRect.X,
                                                               destRect.Y,
                                                               destRect.Width,
                                                               destRect.Height,
                                                               srcx, srcy,
                                                               srcwidth, srcheight,
                                                               srcUnit,
                                                               imageAttributes != null
                                                                ? imageAttributes.nativeImageAttr
                                                                : new GpImageAttributes(),
                                                               IntPtr.Zero,
                                                               IntPtr.Zero));
        }

        public GpStatus DrawImage(ImagePlus image,
                          GpPointF[] destPoints,
                         float srcx,
                         float srcy,
                         float srcwidth,
                         float srcheight,
                         Unit srcUnit,
                          ImageAttributesPlus imageAttributes)
        {
            return SetStatus(NativeMethods.GdipDrawImagePointsRect(nativeGraphics,
                                                                 image != null ? image.nativeImage
                                                                       : null,
                                                                 destPoints, destPoints.Length,
                                                                 srcx, srcy,
                                                                 srcwidth,
                                                                 srcheight,
                                                                 srcUnit,
                                                                 imageAttributes != null
                                                                  ? imageAttributes.nativeImageAttr
                                                                  : new GpImageAttributes(),
                                                                 IntPtr.Zero,
                                                                 IntPtr.Zero));
        }

        public GpStatus DrawImage(ImagePlus image,
                         int x,
                         int y,
                         int srcx,
                         int srcy,
                         int srcwidth,
                         int srcheight,
                         Unit srcUnit)
        {
            return SetStatus(NativeMethods.GdipDrawImagePointRectI(nativeGraphics,
                                                                 image != null ? image.nativeImage
                                                                       : null,
                                                                 x,
                                                                 y,
                                                                 srcx,
                                                                 srcy,
                                                                 srcwidth,
                                                                 srcheight,
                                                                 srcUnit));
        }

        public GpStatus DrawImage(ImagePlus image,
                          GpRect destRect,
                         int srcx,
                         int srcy,
                         int srcwidth,
                         int srcheight,
                         Unit srcUnit,
                          ImageAttributesPlus imageAttributes)
        {
            return SetStatus(NativeMethods.GdipDrawImageRectRectI(nativeGraphics,
                                                                image != null ? image.nativeImage
                                                                      : null,
                                                                destRect.X,
                                                                destRect.Y,
                                                                destRect.Width,
                                                                destRect.Height,
                                                                srcx,
                                                                srcy,
                                                                srcwidth,
                                                                srcheight,
                                                                srcUnit,
                                                                imageAttributes != null
                                                                ? imageAttributes.nativeImageAttr
                                                                : new GpImageAttributes(),
                                                                IntPtr.Zero,
                                                                IntPtr.Zero));
        }

        public GpStatus DrawImage(ImagePlus image,
                          GpPoint[] destPoints,
                         int srcx,
                         int srcy,
                         int srcwidth,
                         int srcheight,
                         Unit srcUnit,
                          ImageAttributesPlus imageAttributes)
        {
            return SetStatus(NativeMethods.GdipDrawImagePointsRectI(nativeGraphics,
                                                                  image != null ? image.nativeImage
                                                                        : null,
                                                                  destPoints,
                                                                  destPoints.Length,
                                                                  srcx,
                                                                  srcy,
                                                                  srcwidth,
                                                                  srcheight,
                                                                  srcUnit,
                                                                  imageAttributes != null
                                                                   ? imageAttributes.nativeImageAttr
                                                                   : new GpImageAttributes(),
                                                                  IntPtr.Zero,
                                                                  IntPtr.Zero));
        }



        //GpStatus SetClip( GraphicsPlus g,
        //               CombineMode combineMode)
        //{
        //    return SetStatus(NativeMethods.GdipSetClipGraphics(nativeGraphics,
        //                                                     g.nativeGraphics,
        //                                                     combineMode));
        //}

        //GpStatus SetClip( GpRectF rect,
        //               CombineMode combineMode)
        //{
        //    return SetStatus(NativeMethods.GdipSetClipRect(nativeGraphics,
        //                                                 rect.X, rect.Y,
        //                                                 rect.Width, rect.Height,
        //                                                 combineMode));
        //}

        //GpStatus SetClip( GpRect rect,
        //               CombineMode combineMode)
        //{
        //    return SetStatus(NativeMethods.GdipSetClipRectI(nativeGraphics,
        //                                                  rect.X, rect.Y,
        //                                                  rect.Width, rect.Height,
        //                                                  combineMode));
        //}

        //GpStatus SetClip( GraphicsPath path,
        //               CombineMode combineMode )
        //{
        //    return SetStatus(NativeMethods.GdipSetClipPath(nativeGraphics,
        //                                                 path.nativePath,
        //                                                 combineMode));
        //}

        //GpStatus SetClip( RegionPlus region,
        //               CombineMode combineMode)
        //{
        //    return SetStatus(NativeMethods.GdipSetClipRegion(nativeGraphics,
        //                                                   region.nativeRegion,
        //                                                   combineMode));
        //}

        // This is different than the other SetClip methods because it assumes
        // that the HRGN is already in device units, so it doesn't transform
        // the coordinates in the HRGN.

        GpStatus SetClip(HRGN hRgn,
                       CombineMode combineMode)
        {
            return SetStatus(NativeMethods.GdipSetClipHrgn(nativeGraphics, hRgn,
                                                         combineMode));
        }

        //GpStatus IntersectClip( GpRectF rect)
        //{
        //    return SetStatus(NativeMethods.GdipSetClipRect(nativeGraphics,
        //                                                 rect.X, rect.Y,
        //                                                 rect.Width, rect.Height,
        //                                                 CombineMode.CombineModeIntersect));
        //}

        //GpStatus IntersectClip( GpRect rect)
        //{
        //    return SetStatus(NativeMethods.GdipSetClipRectI(nativeGraphics,
        //                                                  rect.X, rect.Y,
        //                                                  rect.Width, rect.Height,
        //                                                  CombineMode.CombineModeIntersect));
        //}

        //GpStatus IntersectClip( RegionPlus region)
        //{
        //    return SetStatus(NativeMethods.GdipSetClipRegion(nativeGraphics,
        //                                                   region.nativeRegion,
        //                                                   CombineMode.CombineModeIntersect));
        //}

        //GpStatus ExcludeClip( GpRectF rect)
        //{
        //    return SetStatus(NativeMethods.GdipSetClipRect(nativeGraphics,
        //                                                 rect.X, rect.Y,
        //                                                 rect.Width, rect.Height,
        //                                                 CombineMode.CombineModeExclude));
        //}

        //GpStatus ExcludeClip( GpRect rect)
        //{
        //    return SetStatus(NativeMethods.GdipSetClipRectI(nativeGraphics,
        //                                                  rect.X, rect.Y,
        //                                                  rect.Width, rect.Height,
        //                                                  CombineMode.CombineModeExclude));
        //}

        //GpStatus ExcludeClip( RegionPlus region)
        //{
        //    return SetStatus(NativeMethods.GdipSetClipRegion(nativeGraphics,
        //                                                   region.nativeRegion,
        //                                                   CombineModeExclude));
        //}

        //GpStatus ResetClip()
        //{
        //    return SetStatus(NativeMethods.GdipResetClip(nativeGraphics));
        //}

        //GpStatus TranslateClip(float dx,
        //                     float dy)
        //{
        //    return SetStatus(NativeMethods.GdipTranslateClip(nativeGraphics, dx, dy));
        //}

        //GpStatus TranslateClip(int dx,
        //                     int dy)
        //{
        //    return SetStatus(NativeMethods.GdipTranslateClipI(nativeGraphics,
        //                                                    dx, dy));
        //}

        //GpStatus GetClip(out RegionPlus region)
        //{
        //    return SetStatus(NativeMethods.GdipGetClip(nativeGraphics,
        //                                             out region.nativeRegion));
        //}

        //GpStatus GetClipBounds(out GpRectF rect)
        //{
        //    return SetStatus(NativeMethods.GdipGetClipBounds(nativeGraphics, out rect));
        //}

        //GpStatus GetClipBounds(out GpRect rect)
        //{
        //    return SetStatus(NativeMethods.GdipGetClipBoundsI(nativeGraphics, out rect));
        //}

        //bool IsClipEmpty()
        //{
        //    bool booln = false;

        //    SetStatus(NativeMethods.GdipIsClipEmpty(nativeGraphics, out booln));

        //    return booln;
        //}

        //GpStatus GetVisibleClipBounds(out GpRectF rect)
        //{

        //    return SetStatus(NativeMethods.GdipGetVisibleClipBounds(nativeGraphics,
        //                                                          out rect));
        //}

        //GpStatus GetVisibleClipBounds(out GpRect rect)
        //{
        //   return SetStatus(NativeMethods.GdipGetVisibleClipBoundsI(nativeGraphics,
        //                                                          out rect));
        //}

        //bool IsVisibleClipEmpty()
        //{
        //    bool booln = false;

        //    SetStatus(NativeMethods.GdipIsVisibleClipEmpty(nativeGraphics, out booln));

        //    return booln;
        //}

        bool IsVisible(int x,
                       int y)
        {
            return IsVisible(new GpPoint(x, y));
        }

        bool IsVisible(GpPoint point)
        {
            bool booln = false;

            SetStatus(NativeMethods.GdipIsVisiblePathPointI(new GpPath(),
                                                      point.X,
                                                      point.Y,
                                                      nativeGraphics,
                                                      out booln));

            return booln;
        }


        bool IsVisible(float x,
                       float y)
        {
            return IsVisible(new GpPointF(x, y));
        }

        bool IsVisible(GpPointF point)
        {
            bool booln = false;

            SetStatus(NativeMethods.GdipIsVisiblePathPoint(new GpPath(),
                                                     point.X,
                                                     point.Y,
                                                     nativeGraphics,
                                                     out booln));

            return booln;
        }


        GraphicsState Save()
        {
            GraphicsState gstate;

            SetStatus(NativeMethods.GdipSaveGraphics(nativeGraphics, out gstate));

            return gstate;
        }

        GpStatus Restore(GraphicsState gstate)
        {
            return SetStatus(NativeMethods.GdipRestoreGraphics(nativeGraphics,
                                                             gstate));
        }


        //static HPALETTE GetHalftonePalette()
        //{
        //    return NativeMethods.GdipCreateHalftonePalette();
        //}

        GpStatus GetLastStatus()
        {
            GpStatus lastStatus = lastResult;
            lastResult = GpStatus.Ok;

            return lastStatus;
        }


        protected
            GraphicsPlus(GpGraphics Graphics)
        {
            lastResult = GpStatus.Ok;
            SetNativeGraphics(Graphics);
        }

        protected void SetNativeGraphics(GpGraphics Graphics)
        {
            this.nativeGraphics = Graphics;
        }

        protected GpStatus SetStatus(GpStatus status)
        {
            GpStatusPlus sp = status;
            if (status != GpStatus.Ok)
                return (lastResult = status);
            else
                return status;
        }

        internal GpGraphics GetNativeGraphics()
        {
            return this.nativeGraphics;
        }

        internal GpPen GetNativePen(PenPlus pen)
        {
            return pen.nativePen;
        }

        internal GpGraphics nativeGraphics;
        protected GpStatus lastResult;

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            // free native resources if there are any.
            if ((IntPtr)nativeGraphics != IntPtr.Zero)
            {
                NativeMethods.GdipDeleteGraphics(nativeGraphics);
                nativeGraphics = new GpGraphics();
            }
        }


        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    };

    //----------------------------------------------------------------------------
    // Implementation of GraphicsPath methods that use Graphics
    //----------------------------------------------------------------------------

    // The GetBounds rectangle may not be the tightest bounds.
    /*
Status
GraphicsPath::GetBounds(
    OUT RectF* bounds,
     Matrix* matrix,
     Pen* pen)
{
    GpMatrix* nativeMatrix = null;
    GpPen* nativePen = null;

    if (matrix)
        nativeMatrix = matrix.nativeMatrix;

    if (pen)
        nativePen = pen.nativePen;

    return SetStatus(NativeMethods.GdipGetPathWorldBounds(nativePath, bounds,
                                                   nativeMatrix, nativePen));
}

inline Status
GraphicsPlusPath::GetBounds(
    OUT Rect* bounds,
     Matrix* matrix,
     Pen* pen
)
{
    GpMatrix* nativeMatrix = null;
    GpPen* nativePen = null;

    if (matrix)
        nativeMatrix = matrix.nativeMatrix;

    if (pen)
        nativePen = pen.nativePen;

    return SetStatus(NativeMethods.GdipGetPathWorldBoundsI(nativePath, bounds,
                                                    nativeMatrix, nativePen));
}

inline bool
GraphicsPlusPath::IsVisible(
    float x,
    float y,
     GraphicsPlus g)
{
   bool booln = FALSE;

   GpGraphics nativeGraphics = null;

   if (g)
       nativeGraphics = g.nativeGraphics;

   SetStatus(NativeMethods.GdipIsVisiblePathPoint(nativePath,
                                                x, y, nativeGraphics,
                                                &booln));
   return booln;
}

inline bool
GraphicsPlusPath::IsVisible(
    int x,
    int y,
     GraphicsPlus g)
{
   bool booln = FALSE;

   GpGraphics nativeGraphics = null;

   if (g)
       nativeGraphics = g.nativeGraphics;

   SetStatus(NativeMethods.GdipIsVisiblePathPointI(nativePath,
                                                 x, y, nativeGraphics,
                                                 &booln));
   return booln;
}

inline bool
GraphicsPlusPath::IsOutlineVisible(
    float x,
    float y,
     Pen* pen,
     GraphicsPlus g)
{
    bool booln = FALSE;

    GpGraphics nativeGraphics = null;
    GpPen* nativePen = null;

    if(g)
        nativeGraphics = g.nativeGraphics;
    if(pen)
        nativePen = pen.nativePen;

    SetStatus(NativeMethods.GdipIsOutlineVisiblePathPoint(nativePath,
                                                        x, y, nativePen, nativeGraphics,
                                                        &booln));
    return booln;
}

inline bool
GraphicsPlusPath::IsOutlineVisible(
    int x,
    int y,
     Pen* pen,
     GraphicsPlus g)
{
    bool booln = FALSE;

    GpGraphics nativeGraphics = null;
    GpPen* nativePen = null;

    if(g)
        nativeGraphics = g.nativeGraphics;
    if(pen)
        nativePen = pen.nativePen;

    SetStatus(NativeMethods.GdipIsOutlineVisiblePathPointI(nativePath,
                                                         x, y, nativePen, nativeGraphics,
                                                         &booln));
    return booln;
}
     */
}

