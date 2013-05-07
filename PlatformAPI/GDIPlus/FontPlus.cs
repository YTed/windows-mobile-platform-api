using System;
using System.Runtime.InteropServices;
using System.Text;
using Color = System.Drawing.Color;

namespace PlatformAPI.GDIPlus
{
    public class FontPlus
    {

        //FontPlus(HDC hdc)
        //{
        //    GpFont font = new GpFont();
        //    lastResult = NativeMethods.GdipCreateFontFromDC(hdc, out font);

        //    SetNativeFont(font);
        //}

        /*
        FontPlus(HDC hdc,
                    HFONT hfont)
        {
            GpFont font = null;

            if (hfont != IntPtr.Zero)
            {
                LOGFONTA lf;

                if(GetObjectA(hfont, sizeof(LOGFONTA), &lf))
                    lastResult = NativeMethods.GdipCreateFontFromLogfontA(hdc, &lf, &font);
                else
                    lastResult = NativeMethods.GdipCreateFontFromDC(hdc, &font);
            }
            else
            {
                lastResult = NativeMethods.GdipCreateFontFromDC(hdc, &font);
            }

            SetNativeFont(font);
        }

        */
//        FontPlus(HDC hdc,
//                    LOGFONTW logfont)
//        {
//            GpFont font = null;
//            if (logfont != null)
//            {
//                lastResult = NativeMethods.GdipCreateFontFromLogfontW(hdc, ref logfont, out font);
//            }
//            else
//            {
//                lastResult = NativeMethods.GdipCreateFontFromDC(hdc, out font);
//            }

//            SetNativeFont(font);
//        }


//        FontPlus(HDC hdc,
//                    LOGFONTA logfont)
//        {
//            GpFont font = null;

//            if (logfont)
//            {
//                lastResult = NativeMethods.GdipCreateFontFromLogfontA(hdc, ref logfont, out font);
//            }
//            else
//            {
//                lastResult = NativeMethods.GdipCreateFontFromDC(hdc, out font);
//            }

//            SetNativeFont(font);
//        }


//        FontPlus(
//              FontFamilyPlus family,
//             float emSize,
//             int style,
//             Unit unit
//        )
//        {
//            GpFont font = null;

//            lastResult = NativeMethods.GdipCreateFont(family != null ? family.nativeFamily : null,
//                            emSize,
//                            style,
//                            unit,
//                            out font);

//            SetNativeFont(font);
//        }


//        FontPlus(
//              string familyName,
//             float emSize,
//             int style,
//             Unit unit
//        )
//{
//    nativeFont = null;


//    lastResult = NativeMethods.GdipCreateFont(nativeFamily,
//                            emSize,
//                            style,
//                            unit,
//                            out nativeFont);

//    if (lastResult != GpStatus.Ok)
//    {
//        nativeFamily = FontFamily.GenericSansSerif().nativeFamily;
//        lastResult = FontFamily.GenericSansSerif().lastResult;
//        if (lastResult != GpStatus.Ok)
//            return;

//        lastResult = NativeMethods.GdipCreateFont(
//            nativeFamily,
//            emSize,
//            style,
//            unit,
//            &nativeFont);
//    }
//}

//        GpStatus
//       GetLogFontA(GraphicsPlus g,
//                         out LOGFONTA logfontA)
//        {
//            return SetStatus(NativeMethods.GdipGetLogFontA(nativeFont, g != null ? g.nativeGraphics : null, logfontA));

//        }

//        GpStatus
//       GetLogFontW(GraphicsPlus g,
//                         out LOGFONTW logfontW)
//        {
//            return SetStatus(NativeMethods.GdipGetLogFontW(nativeFont, g != null ? g.nativeGraphics : null, logfontW));
//        }


//        FontPlus
//       Clone()
//        {
//            GpFont cloneFont = null;

//            SetStatus(NativeMethods.GdipCloneFont(nativeFont, out cloneFont));

//            return new FontPlus(cloneFont, lastResult);
//        }


//        ~FontPlus()
//        {
//            NativeMethods.GdipDeleteFont(nativeFont);
//        }

//        // Operations

//        bool
//       IsAvailable()
//        {
//            return (nativeFont == null ? true : false);
//        }

//        GpStatus
//       GetFamily(out GpFontFamily family)
//        {
//            GpStatus status = NativeMethods.GdipGetFamily(nativeFont, out (family.nativeFamily));
//            family.SetStatus(status);

//            return SetStatus(status);
//        }

//        int
//       GetStyle()
//        {
//            int style;

//            SetStatus(NativeMethods.GdipGetFontStyle(nativeFont, out style));

//            return style;
//        }

//        float
//       GetSize()
//        {
//            float size;
//            SetStatus(NativeMethods.GdipGetFontSize(nativeFont, out size));
//            return size;
//        }

//        Unit
//       GetUnit()
//        {
//            Unit unit;
//            SetStatus(NativeMethods.GdipGetFontUnit(nativeFont, out unit));
//            return unit;
//        }

//        float
//       GetHeight(GraphicsPlus graphics)
//        {
//            float height;
//            SetStatus(NativeMethods.GdipGetFontHeight(
//                nativeFont,
//                graphics ? graphics.nativeGraphics : null,
//                &height
//            ));
//            return height;
//        }


//        float
//       GetHeight(float dpi)
//        {
//            float height;
//            SetStatus(NativeMethods.GdipGetFontHeightGivenDPI(nativeFont, dpi, &height));
//            return height;
//        }


//        FontPlus(GpFont font,
//                   GpStatus status)
//        {
//            lastResult = status;
//            SetNativeFont(font);
//        }

//        void
//       SetNativeFont(GpFont FontPlus)
//        {
//            nativeFont = FontPlus;
//        }

//        GpStatus
//       GetLastStatus()
//        {
//            return lastResult;
//        }

//        GpStatus
//       SetStatus(GpStatus status)
//        {
//            if (status != GpStatus.Ok)
//                return (lastResult = status);
//            else
//                return status;
//        }
//        internal GpFont nativeFont;
//        private GpStatus lastResult;
    }


//    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansii)]
//    struct LOGFONTA
//    {
//        int lfHeight;
//        int lfWidth;
//        int lfEscapement;
//        int lfOrientation;
//        int lfWeight;
//        byte lfItalic;
//        byte lfUnderline;
//        byte lfStrikeOut;
//        byte lfCharSet;
//        byte lfOutPrecision;
//        byte lfClipPrecision;
//        byte lfQuality;
//        byte lfPitchAndFamily;
//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
//        string lfFaceName;
//    }
//    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
//    struct LOGFONTW
//    {
//        int lfHeight;
//        int lfWidth;
//        int lfEscapement;
//        int lfOrientation;
//        int lfWeight;
//        byte lfItalic;
//        byte lfUnderline;
//        byte lfStrikeOut;
//        byte lfCharSet;
//        byte lfOutPrecision;
//        byte lfClipPrecision;
//        byte lfQuality;
//        byte lfPitchAndFamily;
//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
//        string lfFaceName;
//    }
}