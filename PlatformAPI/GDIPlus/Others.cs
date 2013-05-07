using System;
using System.Runtime.InteropServices;
using System.Text;

namespace PlatformAPI.GDIPlus
{
    public static partial class NativeMethods
    {
        //----------------------------------------------------------------------------
        // CustomLineCap APIs
        //----------------------------------------------------------------------------

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipCreateCustomLineCap(GpPath fillPath, GpPath strokePath,
LineCap baseCap, float baseInset, out GpCustomLineCap customCap);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipDeleteCustomLineCap(GpCustomLineCap customCap);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipCloneCustomLineCap(GpCustomLineCap customCap,
        out GpCustomLineCap clonedCap);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipGetCustomLineCapType(GpCustomLineCap customCap,
        out CustomLineCapType capType);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipSetCustomLineCapStrokeCaps(GpCustomLineCap customCap,
                GpLineCap startCap, GpLineCap endCap);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipGetCustomLineCapStrokeCaps(GpCustomLineCap customCap,
                out GpLineCap startCap, out GpLineCap endCap);

        //----------------------------------------------------------------------------
        // String format APIs
        //----------------------------------------------------------------------------

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipCreateStringFormat(
int formatAttributes,
int language,
out GpStringFormat format
);


        [DllImport("gdiplus")]
        public static extern GpStatus
GdipDeleteStringFormat(GpStringFormat format);

    }
}
