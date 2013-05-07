using System;
using System.Runtime.InteropServices;
using System.Text;

namespace PlatformAPI.GDIPlus
{
    public static partial class NativeMethods
    {
        //----------------------------------------------------------------------------
        // Matrix APIs
        //----------------------------------------------------------------------------

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipCreateMatrix(out GpMatrix matrix);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipCreateMatrix2(float m11, float m12, float m21, float m22, float dx,
                       float dy, out GpMatrix matrix);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipCreateMatrix3(GpRectF rect, GpPointF[] dstplg,
                       out GpMatrix matrix);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipDeleteMatrix(GpMatrix matrix);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipSetMatrixElements(GpMatrix matrix, float m11, float m12, float m21, float m22,
       float dx, float dy);



        [DllImport("gdiplus")]
        public static extern GpStatus
GdipGetMatrixElements(GpMatrix matrix, float[] matrixOut);
    }
}
