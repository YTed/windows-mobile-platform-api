using System;
using System.Collections.Generic;
using System.Text;
using Color = System.Drawing.Color;

namespace PlatformAPI.GDIPlus
{
    public class PenPlus: IDisposable
    {
        public PenPlus(Color color,
            float width)
        {
            Unit unit = Unit. UnitWorld;
            nativePen = null;
            lastResult = NativeMethods.GdipCreatePen1(color.ToArgb(),
                                        width, unit, out nativePen);
        }

        public PenPlus(BrushPlus brush,
            float width)
        {
            Unit unit = Unit.UnitWorld;
            nativePen = null;
            lastResult = NativeMethods.GdipCreatePen2(brush.nativeBrush,
                                        width, unit, out nativePen);
        }

        ~PenPlus()
        {
            Dispose(true);
        }

        public PenPlus Clone()
        {
            GpPen clonePen = null;

            lastResult = NativeMethods.GdipClonePen(nativePen, out clonePen);

            return new PenPlus(clonePen, lastResult);
        }

        public GpStatus SetWidth(float width)
        {
            return SetStatus(NativeMethods.GdipSetPenWidth(nativePen, width));
        }

        public float GetWidth()
        {
            float width;

            SetStatus(NativeMethods.GdipGetPenWidth(nativePen, out width));

            return width;
        }

        // Set/get line caps: start, end, and dash

        // Line cap and join APIs by using LineCap and LineJoin enums.

        public GpStatus SetLineCap(LineCap startCap,
                          LineCap endCap,
                          DashCap dashCap)
        {
            return SetStatus(NativeMethods.GdipSetPenLineCap197819(nativePen,
                                       startCap, endCap, dashCap));
        }

        public GpStatus SetStartCap(LineCap startCap)
        {
            return SetStatus(NativeMethods.GdipSetPenStartCap(nativePen, startCap));
        }

        public GpStatus SetEndCap(LineCap endCap)
        {
            return SetStatus(NativeMethods.GdipSetPenEndCap(nativePen, endCap));
        }

        //public GpStatus SetDashCap(DashCap dashCap)
        //{
        //    return SetStatus(NativeMethods.GdipSetPenDashCap197819(nativePen,
        //                               dashCap));
        //}

        public LineCap GetStartCap()
        {
            LineCap startCap;

            SetStatus(NativeMethods.GdipGetPenStartCap(nativePen, out startCap));

            return startCap;
        }

        public LineCap GetEndCap()
        {
            LineCap endCap;

            SetStatus(NativeMethods.GdipGetPenEndCap(nativePen, out endCap));

            return endCap;
        }

        public DashCap GetDashCap()
        {
            DashCap dashCap;

            SetStatus(NativeMethods.GdipGetPenDashCap197819(nativePen,
                                out dashCap));

            return dashCap;
        }

        public GpStatus SetLineJoin(LineJoin lineJoin)
        {
            return SetStatus(NativeMethods.GdipSetPenLineJoin(nativePen, lineJoin));
        }

        public LineJoin GetLineJoin()
        {
            LineJoin lineJoin;

            SetStatus(NativeMethods.GdipGetPenLineJoin(nativePen, out lineJoin));

            return lineJoin;
        }

        public GpStatus SetCustomStartCap(CustomLineCap customCap)
        {
            GpCustomLineCap nativeCap = new GpCustomLineCap();
            if (customCap != null)
                nativeCap = customCap.nativeCap;

            return SetStatus(NativeMethods.GdipSetPenCustomStartCap(nativePen,
                                                                  nativeCap));
        }

        public GpStatus GetCustomStartCap(out CustomLineCap customCap)
        {
            customCap = new CustomLineCap();
            return SetStatus(NativeMethods.GdipGetPenCustomStartCap(nativePen,
                                                        out customCap.nativeCap));
        }

        public GpStatus SetCustomEndCap(CustomLineCap customCap)
        {
            GpCustomLineCap nativeCap = new GpCustomLineCap();
            if (customCap != null)
                nativeCap = customCap.nativeCap;

            return SetStatus(NativeMethods.GdipSetPenCustomEndCap(nativePen,
                                                                nativeCap));
        }

        public GpStatus GetCustomEndCap(out CustomLineCap customCap)
        {
            customCap = new CustomLineCap();
            return SetStatus(NativeMethods.GdipGetPenCustomEndCap(nativePen,
                                                        out customCap.nativeCap));
        }

        public GpStatus SetMiterLimit(float miterLimit)
        {
            return SetStatus(NativeMethods.GdipSetPenMiterLimit(nativePen,
                                                        miterLimit));
        }

        public float GetMiterLimit()
        {
            float miterLimit;

            SetStatus(NativeMethods.GdipGetPenMiterLimit(nativePen, out miterLimit));

            return miterLimit;
        }

        public GpStatus SetAlignment(PenAlignment penAlignment)
        {
            return SetStatus(NativeMethods.GdipSetPenMode(nativePen, penAlignment));
        }

        public PenAlignment GetAlignment()
        {
            PenAlignment penAlignment;

            SetStatus(NativeMethods.GdipGetPenMode(nativePen, out penAlignment));

            return penAlignment;
        }

        //PenType GetPenType()
        //{
        //    PenType type;
        //    SetStatus(NativeMethods.GdipGetPenFillType(nativePen, out type));

        //    return type;
        //}

        public GpStatus SetColor(Color color)
        {
            return SetStatus(NativeMethods.GdipSetPenColor(nativePen,
                                                         color.ToArgb()));
        }

        public GpStatus SetBrush(BrushPlus brush)
        {
            return SetStatus(NativeMethods.GdipSetPenBrushFill(nativePen,
                                           brush.nativeBrush));
        }

        public GpStatus GetColor(out Color color)
        {

            //PenType type = GetPenType();

            //if (type != PenType. PenTypeSolidColor)
            //{
            //    return GpStatus. WrongState;
            //}

            int argb;
            color = Color.FromArgb(0);
            SetStatus(NativeMethods.GdipGetPenColor(nativePen,
                                                  out argb));
            if (lastResult == GpStatus.Ok)
            {
                color = Color.FromArgb(argb);
            }

            return lastResult;
        }

        //BrushPlus GetBrush()
        //{
        //    PenType type = GetPenType();

        //    BrushPlus brush = null;

        //    switch (type)
        //    {
        //        case PenType.PenTypeSolidColor:
        //            brush = new SolidBrushPlus();
        //            break;

        //        case PenType.PenTypeHatchFill:
        //            brush = new HatchBrush();
        //            break;

        //        case PenType.PenTypeTextureFill:
        //            brush = new TextureBrush();
        //            break;

        //        case PenType.PenTypePathGradient:
        //            brush = new BrushPlus();
        //            break;

        //        case PenType.PenTypeLinearGradient:
        //            brush = new LinearGradientBrush();
        //            break;

        //        default:
        //            break;
        //    }

        //    if (brush != null)
        //    {
        //        GpBrush nativeBrush;

        //        SetStatus(NativeMethods.GdipGetPenBrushFill(nativePen,
        //                                                  out nativeBrush));
        //        brush.SetNativeBrush(nativeBrush);
        //    }

        //    return brush;
        //}

        public DashStyle GetDashStyle()
        {
            DashStyle dashStyle;

            SetStatus(NativeMethods.GdipGetPenDashStyle(nativePen, out dashStyle));

            return dashStyle;
        }

        public GpStatus SetDashStyle(DashStyle dashStyle)
        {
            return SetStatus(NativeMethods.GdipSetPenDashStyle(nativePen,
                                                             dashStyle));
        }

        public float GetDashOffset()
        {
            float dashOffset;

            SetStatus(NativeMethods.GdipGetPenDashOffset(nativePen, out dashOffset));

            return dashOffset;
        }

        public GpStatus SetDashOffset(float dashOffset)
        {
            return SetStatus(NativeMethods.GdipSetPenDashOffset(nativePen,
                                                              dashOffset));
        }

        public GpStatus SetDashPattern(float[] dashArray)
        {
            return SetStatus(NativeMethods.GdipSetPenDashArray(nativePen,
                                                             dashArray,
                                                             dashArray.Length));
        }

        public int GetDashPatternCount()
        {
            int count = 0;

            SetStatus(NativeMethods.GdipGetPenDashCount(nativePen, out count));

            return count;
        }

        public GpStatus GetDashPattern(float[] dashArray)
        {
            if (dashArray == null || dashArray.Length == 0)
                return SetStatus(GpStatus.InvalidParameter);

            return SetStatus(NativeMethods.GdipGetPenDashArray(nativePen,
                                                             dashArray,
                                                             dashArray.Length));
        }


        public GpStatus GetLastStatus()
        {
            GpStatus lastStatus = lastResult;
            lastResult = GpStatus.Ok;

            return lastStatus;
        }




        protected PenPlus(GpPen nativePen, GpStatus status)
        {
            lastResult = status;
            SetNativePen(nativePen);
        }

        void SetNativePen(GpPen nativePen)
        {
            this.nativePen = nativePen;
        }

        GpStatus SetStatus(GpStatus status)
        {
            GpStatusPlus sp = status;
            if (status != GpStatus.Ok)
                return (lastResult = status);
            else
                return status;
        }


        internal GpPen nativePen;
        protected GpStatus lastResult;

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            // free native resources if there are any.
            if ((IntPtr)nativePen!= IntPtr.Zero)
            {
                NativeMethods.GdipDeletePen(nativePen);
                nativePen = new GpPen();
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
