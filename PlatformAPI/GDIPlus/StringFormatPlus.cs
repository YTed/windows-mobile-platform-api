using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformAPI.GDIPlus
{
    public class StringFormatPlus
    {
        StringFormatPlus(
             int formatFlags,
             int language
        )
        {
            nativeFormat = new GpStringFormat();
            lastError = NativeMethods.GdipCreateStringFormat(
                formatFlags,
                language,
                out nativeFormat
            );
        }



        ~StringFormatPlus()
        {
            NativeMethods.GdipDeleteStringFormat(nativeFormat);
        }

        GpStatus GetLastStatus()
        {
            GpStatus lastStatus = lastError;
            lastError = GpStatus.Ok;

            return lastStatus;
        }


        internal GpStatus SetStatus(GpStatus newStatus)
        {
            if (newStatus == GpStatus.Ok)
            {
                return GpStatus.Ok;
            }
            else
            {
                return lastError = newStatus;
            }
        }


        StringFormatPlus(GpStringFormat clonedStringFormat, GpStatus status)
        {
            lastError = status;
            nativeFormat = clonedStringFormat;

        }

        internal GpStringFormat nativeFormat;
        private GpStatus lastError;

        //static byte GenericTypographicStringFormatBuffer[sizeof(StringFormatPlus)] = {0};
        //static byte GenericDefaultStringFormatBuffer[sizeof(StringFormatPlus)] = {0};
        static StringFormatPlus genericDefault;
        static StringFormatPlus GenericDefault
        {
            get
            {
                if (genericDefault == null)
                {
                    GpStringFormat fmt;
                    NativeMethods.GdipCreateStringFormat(0, 0, out fmt);
                    genericDefault = new StringFormatPlus(fmt, GpStatus.Ok);
                }
                return genericDefault;
            }
        }
    }

}

