using System;
using System.Runtime.InteropServices;
using System.Text;

namespace PlatformAPI.GDIPlus
{
    public static partial class NativeMethods
    {
//----------------------------------------------------------------------------
// ImageAttributes APIs
//----------------------------------------------------------------------------

[DllImport("gdiplus")] public static extern GpStatus 
GdipCreateImageAttributes(out GpImageAttributes imageattr);

[DllImport("gdiplus")] public static extern GpStatus 
GdipCloneImageAttributes(GpImageAttributes imageattr,
                         out GpImageAttributes cloneImageattr);

[DllImport("gdiplus")] public static extern GpStatus 
GdipDisposeImageAttributes(GpImageAttributes imageattr);

[DllImport("gdiplus")] public static extern GpStatus 
GdipSetImageAttributesToIdentity(GpImageAttributes imageattr,
                                 ColorAdjustType type);
[DllImport("gdiplus")] public static extern GpStatus 
GdipResetImageAttributes(GpImageAttributes imageattr,
                         ColorAdjustType type);

//[DllImport("gdiplus")] public static extern GpStatus 
//GdipSetImageAttributesColorMatrix(GpImageAttributes imageattr,
//                               ColorAdjustType type,
//                               bool enableFlag,
//                               ColorMatrix* colorMatrix,
//                               ColorMatrix* grayMatrix,
//                               ColorMatrixFlags flags);

[DllImport("gdiplus")] public static extern GpStatus 
GdipSetImageAttributesThreshold(GpImageAttributes imageattr,
                                ColorAdjustType type,
                                bool enableFlag,
                                float threshold);

[DllImport("gdiplus")] public static extern GpStatus 
GdipSetImageAttributesGamma(GpImageAttributes imageattr,
                            ColorAdjustType type,
                            bool enableFlag,
                            float gamma);

[DllImport("gdiplus")] public static extern GpStatus 
GdipSetImageAttributesNoOp(GpImageAttributes imageattr,
                           ColorAdjustType type,
                           bool enableFlag);

[DllImport("gdiplus")] public static extern GpStatus 
GdipSetImageAttributesColorKeys(GpImageAttributes imageattr,
                                ColorAdjustType type,
                                bool enableFlag,
                                int colorLow,
                                int colorHigh);
    }
}
