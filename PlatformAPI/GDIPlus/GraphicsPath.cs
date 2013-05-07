using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformAPI.GDIPlus
{
    public class GraphicsPath: IDisposable 
    {
        public GraphicsPath(): this(FillMode.FillModeAlternate) { }
        public GraphicsPath(FillMode fillMode)
        {
            nativePath = null;
            lastResult = NativeMethods.GdipCreatePath(fillMode, out nativePath);
        }

        //GraphicsPath(GpPointF[] points,
        //             byte[] types,
        //             FillMode fillMode)
        //{
        //    nativePath = null;
        //    lastResult = NativeMethods.GdipCreatePath2(points,
        //                                             types,
        //                                             points.Length,
        //                                             fillMode,
        //                                             out nativePath);
        //}

        //GraphicsPath(GpPoint[] points,
        //             byte[] types,
        //             FillMode fillMode)
        //{
        //    nativePath = null;
        //    lastResult = NativeMethods.GdipCreatePath2I(points,
        //                                              types,
        //                                              points.Length,
        //                                              fillMode,
        //                                              out nativePath);
        //}

        ~GraphicsPath()
        {
            Dispose(true);
        }

        public void Clear()
        {
            NativeMethods.GdipDeletePath(nativePath);
            NativeMethods.GdipCreatePath(FillMode.FillModeAlternate, out nativePath);
        }

        GpStatus Transform(Matrix matrix)
        {
            if (matrix != null)
                return SetStatus(NativeMethods.GdipTransformPath(nativePath,
                                                          matrix.nativeMatrix));
            else
                return GpStatus.Ok;
        }

        public GraphicsPath Clone()
        {
            GpPath clonepath = null;

            SetStatus(NativeMethods.GdipClonePath(nativePath, out clonepath));

            return new GraphicsPath(clonepath);
        }

        // Reset the path object to empty (and fill mode to FillModeAlternate)

        public GpStatus Reset()
        {
            return SetStatus(NativeMethods.GdipResetPath(nativePath));
        }

        public FillMode GetFillMode()
        {
            FillMode fillmode = FillMode.FillModeAlternate;

            SetStatus(NativeMethods.GdipGetPathFillMode(nativePath, out fillmode));

            return fillmode;
        }

        public GpStatus SetFillMode(FillMode fillmode)
        {
            return SetStatus(NativeMethods.GdipSetPathFillMode(nativePath,
                                                             fillmode));
        }

        //GpStatus GetPathData(out GpPathData pathData) 
        //{
        //    if (pathData == null) 
        //    {
        //        return SetStatus(InvalidParameter);
        //    }

        //    int count = GetPointCount();

        //    if ((count <= 0) || (pathData.Count>0 && pathData.Count<count))
        //    {
        //        pathData.Count = 0;

        //        if (count <= 0)
        //        {
        //            return GpStatus.Ok;
        //        }
        //    }

        //    if (pathData.Count == 0) 
        //    {
        //        pathData.Points = new GpPointF[count];

        //        pathData.Types = new byte[count];
        //        pathData.Count = count;
        //    }

        //    return SetStatus(NativeMethods.GdipGetPathData(nativePath, pathData));
        //}

        public GpStatus StartFigure()
        {
            return SetStatus(NativeMethods.GdipStartPathFigure(nativePath));
        }

        public GpStatus CloseFigure()
        {
            return SetStatus(NativeMethods.GdipClosePathFigure(nativePath));
        }

        public GpStatus CloseAllFigures()
        {
            return SetStatus(NativeMethods.GdipClosePathFigures(nativePath));
        }

        public GpStatus SetMarker()
        {
            return SetStatus(NativeMethods.GdipSetPathMarker(nativePath));
        }

        public GpStatus ClearMarkers()
        {
            return SetStatus(NativeMethods.GdipClearPathMarkers(nativePath));
        }

        public GpStatus Reverse()
        {
            return SetStatus(NativeMethods.GdipReversePath(nativePath));
        }

        public GpStatus GetLastPoint(out GpPointF lastPoint)
        {
            return SetStatus(NativeMethods.GdipGetPathLastPoint(nativePath,
                                                              out lastPoint));
        }

        public GpStatus AddLine(GpPointF pt1,
                   GpPointF pt2)
        {
            return AddLine(pt1.X, pt1.Y, pt2.X, pt2.Y);
        }

        public GpStatus AddLine(float x1,
                   float y1,
                   float x2,
                   float y2)
        {
            return SetStatus(NativeMethods.GdipAddPathLine(nativePath, x1, y1,
                                                         x2, y2));
        }

        public GpStatus AddLines(GpPointF[] points)
        {
            return SetStatus(NativeMethods.GdipAddPathLine2(nativePath, points,
                                                          points.Length));
        }

        public GpStatus AddLine(GpPoint pt1,
                   GpPoint pt2)
        {
            return AddLine(pt1.X,
                           pt1.Y,
                           pt2.X,
                           pt2.Y);
        }

        public GpStatus AddLine(int x1,
                   int y1,
                   int x2,
                   int y2)
        {
            return SetStatus(NativeMethods.GdipAddPathLineI(nativePath,
                                                         x1,
                                                         y1,
                                                         x2,
                                                         y2));
        }

        public GpStatus AddLines(GpPoint[] points)
        {
            return SetStatus(NativeMethods.GdipAddPathLine2I(nativePath,
                                                           points,
                                                           points.Length));
        }

        public GpStatus AddArc(GpRectF rect,
                  float startAngle,
                  float sweepAngle)
        {
            return AddArc(rect.X, rect.Y, rect.Width, rect.Height,
                          startAngle, sweepAngle);
        }

        public GpStatus AddArc(float x,
                  float y,
                  float width,
                  float height,
                  float startAngle,
                  float sweepAngle)
        {
            return SetStatus(NativeMethods.GdipAddPathArc(nativePath, x, y, width,
                                                        height, startAngle,
                                                        sweepAngle));
        }

        public GpStatus AddArc(GpRect rect,
                  float startAngle,
                  float sweepAngle)
        {
            return AddArc(rect.X, rect.Y, rect.Width, rect.Height,
                          startAngle, sweepAngle);
        }

        public GpStatus AddArc(int x,
                  int y,
                  int width,
                  int height,
                  float startAngle,
                  float sweepAngle)
        {
            return SetStatus(NativeMethods.GdipAddPathArcI(nativePath,
                                                        x,
                                                        y,
                                                        width,
                                                        height,
                                                        startAngle,
                                                        sweepAngle));
        }

        public GpStatus AddBezier(GpPointF pt1,
                     GpPointF pt2,
                     GpPointF pt3,
                     GpPointF pt4)
        {
            return AddBezier(pt1.X, pt1.Y, pt2.X, pt2.Y, pt3.X, pt3.Y, pt4.X,
                             pt4.Y);
        }

        public GpStatus AddBezier(float x1,
                     float y1,
                     float x2,
                     float y2,
                     float x3,
                     float y3,
                     float x4,
                     float y4)
        {
            return SetStatus(NativeMethods.GdipAddPathBezier(nativePath, x1, y1, x2,
                                                           y2, x3, y3, x4, y4));
        }

        public GpStatus AddBeziers(GpPointF[] points)
        {
            int count = points.Length;
            if (count < 4)
                return GpStatus.Ok;
            count -= (count - 4) % 3;
            return SetStatus(NativeMethods.GdipAddPathBeziers(nativePath, points,
                                                            count));
        }

        public GpStatus AddBezier(GpPoint pt1,
                     GpPoint pt2,
                     GpPoint pt3,
                     GpPoint pt4)
        {
            return AddBezier(pt1.X, pt1.Y, pt2.X, pt2.Y, pt3.X, pt3.Y, pt4.X,
                             pt4.Y);
        }

        public GpStatus AddBezier(int x1,
                     int y1,
                     int x2,
                     int y2,
                     int x3,
                     int y3,
                     int x4,
                     int y4)
        {
            return SetStatus(NativeMethods.GdipAddPathBezierI(nativePath,
                                                           x1,
                                                           y1,
                                                           x2,
                                                           y2,
                                                           x3,
                                                           y3,
                                                           x4,
                                                           y4));
        }

        public GpStatus AddBeziers(GpPoint[] points)
        {
            return SetStatus(NativeMethods.GdipAddPathBeziersI(nativePath,
                                                             points,
                                                             points.Length));
        }

        public GpStatus AddCurve(GpPointF[] points)
        {
            return SetStatus(NativeMethods.GdipAddPathCurve(nativePath,
                                                          points,
                                                          points.Length));
        }

        public GpStatus AddCurve(GpPointF[] points,
                    float tension)
        {
            return SetStatus(NativeMethods.GdipAddPathCurve2(nativePath,
                                                           points,
                                                           points.Length,
                                                           tension));
        }

        public GpStatus AddCurve(GpPointF[] points,
                    int offset,
                    int numberOfSegments,
                    float tension)
        {
            return SetStatus(NativeMethods.GdipAddPathCurve3(nativePath,
                                                           points,
                                                           points.Length,
                                                           offset,
                                                           numberOfSegments,
                                                           tension));
        }

        public GpStatus AddCurve(GpPoint[] points)
        {
            return SetStatus(NativeMethods.GdipAddPathCurveI(nativePath,
                                                          points,
                                                          points.Length));
        }

        public GpStatus AddCurve(GpPoint[] points,
                    float tension)
        {
            return SetStatus(NativeMethods.GdipAddPathCurve2I(nativePath,
                                                           points,
                                                           points.Length,
                                                           tension));
        }

        public GpStatus AddCurve(GpPoint[] points,
                    int offset,
                    int numberOfSegments,
                    float tension)
        {
            return SetStatus(NativeMethods.GdipAddPathCurve3I(nativePath,
                                                           points,
                                                           points.Length,
                                                           offset,
                                                           numberOfSegments,
                                                           tension));
        }

        public GpStatus AddClosedCurve(GpPointF[] points)
        {
            return SetStatus(NativeMethods.GdipAddPathClosedCurve(nativePath,
                                                                points,
                                                                points.Length));
        }

        public GpStatus AddClosedCurve(GpPointF[] points,
                          float tension)
        {
            return SetStatus(NativeMethods.GdipAddPathClosedCurve2(nativePath,
                                                                 points,
                                                                 points.Length,
                                                                 tension));
        }

        public GpStatus AddClosedCurve(GpPoint[] points)
        {
            return SetStatus(NativeMethods.GdipAddPathClosedCurveI(nativePath,
                                                                 points,
                                                                 points.Length));
        }


        public GpStatus AddClosedCurve(GpPoint[] points,
                          float tension)
        {
            return SetStatus(NativeMethods.GdipAddPathClosedCurve2I(nativePath,
                                                                  points,
                                                                  points.Length,
                                                                  tension));
        }

        public GpStatus AddRectangle(GpRectF rect)
        {
            return SetStatus(NativeMethods.GdipAddPathPolygon(nativePath,
                                                              new GpPointF[]
                                                              {
                                                                  new GpPointF(rect.X, rect.Y),
                                                                  new GpPointF(rect.X, rect.Y + rect.Height),
                                                                  new GpPointF(rect.X + rect.Width, rect.Y + rect.Height),
                                                                  new GpPointF(rect.X + rect.Width, rect.Y),
                                                              },
                                                              4));
        }

        public GpStatus AddRectangles(GpRectF[] rects)
        {
            return SetStatus(NativeMethods.GdipAddPathRectangles(nativePath,
                                                               rects,
                                                               rects.Length));
        }

        //public GpStatus AddRectangle(GpRect rect)
        //{
        //    return SetStatus(NativeMethods.GdipAddPathRectangleI(nativePath,
        //                                                      rect.X,
        //                                                      rect.Y,
        //                                                      rect.Width,
        //                                                      rect.Height));
        //}

        public GpStatus AddRectangles(GpRect[] rects)
        {
            return SetStatus(NativeMethods.GdipAddPathRectanglesI(nativePath,
                                                               rects,
                                                               rects.Length));
        }

        public GpStatus AddEllipse(GpRectF rect)
        {
            return AddEllipse(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public GpStatus AddEllipse(float x,
                      float y,
                      float width,
                      float height)
        {
            return SetStatus(NativeMethods.GdipAddPathEllipse(nativePath,
                                                            x,
                                                            y,
                                                            width,
                                                            height));
        }

        public GpStatus AddEllipse(GpRect rect)
        {
            return AddEllipse(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public GpStatus AddEllipse(int x,
                      int y,
                      int width,
                      int height)
        {
            return SetStatus(NativeMethods.GdipAddPathEllipseI(nativePath,
                                                            x,
                                                            y,
                                                            width,
                                                            height));
        }

        public GpStatus AddPie(GpRectF rect,
                  float startAngle,
                  float sweepAngle)
        {
            return AddPie(rect.X, rect.Y, rect.Width, rect.Height, startAngle,
                          sweepAngle);
        }

        public GpStatus AddPie(float x,
                  float y,
                  float width,
                  float height,
                  float startAngle,
                  float sweepAngle)
        {
            return SetStatus(NativeMethods.GdipAddPathPie(nativePath, x, y, width,
                                                        height, startAngle,
                                                        sweepAngle));
        }

        public GpStatus AddPie(GpRect rect,
                  float startAngle,
                  float sweepAngle)
        {
            return AddPie(rect.X,
                          rect.Y,
                          rect.Width,
                          rect.Height,
                          startAngle,
                          sweepAngle);
        }

        public GpStatus AddPie(int x,
                  int y,
                  int width,
                  int height,
                  float startAngle,
                  float sweepAngle)
        {
            return SetStatus(NativeMethods.GdipAddPathPieI(nativePath,
                                                        x,
                                                        y,
                                                        width,
                                                        height,
                                                        startAngle,
                                                        sweepAngle));
        }

        public GpStatus AddPolygon(GpPointF[] points)
        {
            return SetStatus(NativeMethods.GdipAddPathPolygon(nativePath, points,
                                                            points.Length));
        }

        public GpStatus AddPolygon(GpPoint[] points)
        {
            return SetStatus(NativeMethods.GdipAddPathPolygonI(nativePath, points,
                                                             points.Length));
        }

        public GpStatus AddPath(GraphicsPath addingPath,
                   bool connect)
        {
            GpPath nativePath2 = null;
            if (addingPath != null)
                nativePath2 = addingPath.nativePath;

            return SetStatus(NativeMethods.GdipAddPathPath(nativePath, nativePath2,
                                                         connect));
        }

        // This is not always the tightest bounds.

        //Status GetBounds(out GpRectF bounds, 
        //                 Matrix matrix, 
        //                 Pen pen ) ;

        //Status GetBounds(OUT GpRect* bounds,
        //                 Matrix* matrix = null, 
        //                 Pen* pen = null) ;

        // Once flattened, the resultant path is made of line segments and
        // the original path information is lost.  When matrix is null the
        // identity matrix is assumed.

        public GpStatus Flatten(Matrix matrix,
                   float flatness)
        {
            GpMatrix nativeMatrix = new GpMatrix();
            if (matrix != null)
            {
                nativeMatrix = matrix.nativeMatrix;
            }

            return SetStatus(NativeMethods.GdipFlattenPath(
                nativePath,
                nativeMatrix,
                flatness
            ));
        }

        public GpStatus Widen(
        PenPlus pen,
        Matrix matrix,
        float flatness
    )
        {
            GpMatrix nativeMatrix = new GpMatrix();
            if (matrix != null)
                nativeMatrix = matrix.nativeMatrix;

            return SetStatus(NativeMethods.GdipWidenPath(
                nativePath,
                pen.nativePen,
                nativeMatrix,
                flatness
            ));
        }

        public GpStatus Outline(
        Matrix matrix,
        float flatness
    )
        {
            GpMatrix nativeMatrix = new GpMatrix();
            if (matrix != null)
            {
                nativeMatrix = matrix.nativeMatrix;
            }

            return SetStatus(NativeMethods.GdipWindingModeOutline(
                nativePath, nativeMatrix, flatness
            ));
        }

        // Once this is called, the resultant path is made of line segments and
        // the original path information is lost.  When matrix is null, the 
        // identity matrix is assumed.

        public GpStatus Warp(GpPointF[] destPoints,
                GpRectF srcRect,
                Matrix matrix,
                WarpMode warpMode,
                float flatness)
        {
            GpMatrix nativeMatrix = new GpMatrix();
            if (matrix != null)
                nativeMatrix = matrix.nativeMatrix;

            return SetStatus(NativeMethods.GdipWarpPath(
                                            nativePath,
                                            nativeMatrix,
                                            destPoints,
                                            destPoints.Length,
                                            srcRect.X,
                                            srcRect.Y,
                                            srcRect.Width,
                                            srcRect.Height,
                                            warpMode,
                                            flatness));
        }

        public int GetPointCount()
        {
            int count = 0;

            SetStatus(NativeMethods.GdipGetPointCount(nativePath, out count));

            return count;
        }

        public GpStatus GetPathTypes(byte[] types)
        {
            return SetStatus(NativeMethods.GdipGetPathTypes(nativePath, types,
                                                          types.Length));
        }

        public GpStatus GetPathPoints(GpPointF[] points)
        {
            return SetStatus(NativeMethods.GdipGetPathPoints(nativePath, points,
                                                           points.Length));
        }

        public GpStatus GetPathPoints(GpPoint[] points)
        {
            return SetStatus(NativeMethods.GdipGetPathPointsI(nativePath, points,
                                                            points.Length));
        }

        public GpStatus GetLastStatus()
        {
            GpStatus lastStatus = lastResult;
            lastResult = GpStatus.Ok;

            return lastStatus;
        }

        public bool IsVisible(GpPointF point,
                   GraphicsPlus g)
        {
            return IsVisible(point.X, point.Y, g);
        }

        public bool IsVisible(float x,
                   float y,
                   GraphicsPlus g)
        {
            bool ret;
            SetStatus(NativeMethods.GdipIsVisiblePathPoint(nativePath, x, y, g.nativeGraphics, out ret));
            return ret;
        }

        public bool IsVisible(GpPoint point,
                   GraphicsPlus g)
        {
            return IsVisible(point.X, point.Y, g);
        }

        public bool IsVisible(int x,
                       int y,
                       GraphicsPlus g)
        {
            bool ret;
            NativeMethods.GdipIsVisiblePathPoint(nativePath, x, y, g.nativeGraphics, out ret);
            return ret;
        }

        public bool IsOutlineVisible(GpPointF point,
                          PenPlus pen,
                          GraphicsPlus g)
        {
            return IsOutlineVisible(point.X, point.Y, pen, g);
        }

        public bool IsOutlineVisible(float x,
                          float y,
                          PenPlus pen,
                          GraphicsPlus g)
        {
            bool ret;
            SetStatus(NativeMethods.GdipIsOutlineVisiblePathPoint(nativePath, x, y, pen.nativePen, g.nativeGraphics, out ret));
            return ret;
        }

        public bool IsOutlineVisible(GpPoint point,
                          PenPlus pen,
                          GraphicsPlus g)
        {
            return IsOutlineVisible(point.X, point.Y, pen, g);
        }



        public GraphicsPath(GraphicsPath path)
        {
            GpPath clonepath = null;
            SetStatus(NativeMethods.GdipClonePath(path.nativePath, out clonepath));
            SetNativePath(clonepath);
        }

        protected
            GraphicsPath(GpPath nativePath)
        {
            lastResult = GpStatus.Ok;
            SetNativePath(nativePath);
        }

        void SetNativePath(GpPath nativePath)
        {
            this.nativePath = nativePath;
        }

        GpStatus SetStatus(GpStatus status)
        {
            GpStatusPlus sp = status;
            if (status != GpStatus.Ok)
                return (lastResult = status);
            else
                return status;
        }


        internal GpPath nativePath;
        protected GpStatus lastResult;

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            // free native resources if there are any.
            if ((IntPtr)nativePath!= IntPtr.Zero)
            {
                NativeMethods.GdipDeletePath(nativePath);
                nativePath = new GpPath();
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
