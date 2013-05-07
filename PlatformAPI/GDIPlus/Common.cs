using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;

namespace PlatformAPI.GDIPlus
{
    public static partial class NativeMethods
    {
// GDI+ initialization. Must not be called from DllMain - can cause deadlock.
//
// Must be called before GDI+ API's or constructors are used.
//
// token  - may not be NULL - accepts a token to be passed in the corresponding
//          GdiplusShutdown call.
// input  - may not be NULL
// output - may be NULL only if input->SuppressBackgroundThread is FALSE.

        [DllImport("gdiplus")]
        extern static public GpStatus GdiplusStartup(
            out IntPtr token,
            GdiplusStartupInput input,
            out GdiplusStartupOutput output);

// GDI+ termination. Must be called before GDI+ is unloaded. 
// Must not be called from DllMain - can cause deadlock.
//
// GDI+ API's may not be called after GdiplusShutdown. Pay careful attention
// to GDI+ object destructors.

        [DllImport("gdiplus")]
        extern static public void GdiplusShutdown(IntPtr token);
 
    }
}
