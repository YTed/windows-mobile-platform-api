using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;

namespace PlatformAPI.GDIPlus
{
public delegate  int NotificationHookProc(out IntPtr token);
public delegate void NotificationUnhookProc(IntPtr token);


    // Input structure for GdiplusStartup()

    [StructLayout(LayoutKind.Sequential)]
    public class GdiplusStartupInput
    {
        public uint GdiplusVersion;             // Must be 1  (or 2 for the Ex version)
        int DebugEventCallback; // Ignored on free builds
        public bool SuppressBackgroundThread;     // FALSE unless you're prepared to call 
        // the hook/unhook functions properly
        public bool SuppressExternalCodecs;       // FALSE unless you want GDI+ only to use
        // its internal image codecs.

        public GdiplusStartupInput(
            int debugEventCallback,
            bool suppressBackgroundThread,
            bool suppressExternalCodecs)
        {
            GdiplusVersion = 1;
            DebugEventCallback = debugEventCallback;
            SuppressBackgroundThread = suppressBackgroundThread;
            SuppressExternalCodecs = suppressExternalCodecs;
        }
        public GdiplusStartupInput()
            : this(0, false, false)
        {
        }

    }

    public enum GdiplusStartupParams:uint
    {
        GdiplusStartupDefault = 0,
        GdiplusStartupNoSetRound = 1,
        GdiplusStartupSetPSValue = 2,
        GdiplusStartupTransparencyMask = 0xFF000000
    }

    // Output structure for GdiplusStartup()

    [StructLayout(LayoutKind.Sequential)]
    public struct GdiplusStartupOutput
    {
        // The following 2 fields are NULL if SuppressBackgroundThread is FALSE.
        // Otherwise, they are functions which must be called appropriately to
        // replace the background thread.
        //
        // These should be called on the application's main message loop - i.e.
        // a message loop which is active for the lifetime of GDI+.
        // "NotificationHook" should be called before starting the loop,
        // and "NotificationUnhook" should be called after the loop ends.

        IntPtr /*NotificationHookProc*/ NotificationHook;
        IntPtr /*NotificationUnhookProc*/ NotificationUnhook;
    };

//---------------------------------------------------------------------------
// Encoder Parameter structure
//---------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential)]
    public struct EncoderParameter
    {
        public Guid Guid;               // GUID of the parameter
        public uint NumberOfValues;     // Number of the parameter values
        public uint Type;               // Value type, like ValueTypeLONG  etc.
        IntPtr Value;              // A pointer to the parameter values
    }

//---------------------------------------------------------------------------
// Encoder Parameters structure
//---------------------------------------------------------------------------
public class EncoderParameters
{
    public uint Count;                      // Number of parameters in this structure
    [MarshalAs(UnmanagedType.ByValArray)]
    public EncoderParameter[] Parameters;          // Parameter values
};

    public class GpStatusPlus
    {
        public GpStatus Status;

        public static implicit operator GpStatusPlus(GpStatus status)
        {
            GpStatusPlus sp = new GpStatusPlus();
            sp.Status = status;
            if (status != GpStatus.Ok)
                throw new Exception(string.Format("Bad status : {0}", status));
            return sp;
        }
    }
}
