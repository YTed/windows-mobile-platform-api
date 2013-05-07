using System;
using System.Collections.Generic;
using System.Text;
using PlatformAPI.Runtime.InteropServices.ComTypes;
using Color = System.Drawing.Color;
using PixelFormat = System.Drawing.Imaging.PixelFormat;
using BitmapData = System.Drawing.Imaging.BitmapData;

namespace PlatformAPI.GDIPlus
{
    public class BitmapPlus : ImagePlus
    {
        public BitmapPlus(string filename)
        {
            GpBitmap bitmap = new GpBitmap();

            lastResult = NativeMethods.GdipCreateBitmapFromFileICM(filename, out bitmap);

            SetNativeImage((GpImage)(IntPtr)bitmap);
        }


        public BitmapPlus(IStream stream)
        {
            GpBitmap bitmap = new GpBitmap();

            lastResult = NativeMethods.GdipCreateBitmapFromStreamICM(stream, out bitmap);

            SetNativeImage((GpImage)(IntPtr)bitmap);
        }


        public BitmapPlus(
            int width,
            int height,
            int stride,
            PixelFormat format,
            IntPtr scan0
            )
        {
            GpBitmap bitmap = new GpBitmap();

            lastResult = NativeMethods.GdipCreateBitmapFromScan0(width,
                                                               height,
                                                               stride,
                                                               format,
                                                               scan0,
                                                               out bitmap);

            SetNativeImage((GpImage)(IntPtr)bitmap);
        }


        public BitmapPlus(
            int width,
            int height,
            PixelFormat format
            )
        {
            GpBitmap bitmap = new GpBitmap();

            lastResult = NativeMethods.GdipCreateBitmapFromScan0(width,
                                                               height,
                                                               0,
                                                               format,
                                                               IntPtr.Zero,
                                                               out bitmap);

            SetNativeImage((GpImage)(IntPtr)bitmap);
        }


        public BitmapPlus(
            int width,
            int height,
            GraphicsPlus target)
        {
            GpBitmap bitmap = new GpBitmap();

            lastResult = NativeMethods.GdipCreateBitmapFromGraphics(width,
                                                                  height,
                                                                  target.nativeGraphics,
                                                                  out bitmap);

            SetNativeImage((GpImage)(IntPtr)bitmap);
        }

        /* 
        public Bitmap(
            BITMAPINFO* gdiBitmapInfo, 
            VOID* gdiBitmapData
            )
        {
            GpBitmap bitmap = new GpBitmap();

            lastResult = NativeMethods.GdipCreateBitmapFromGdiDib(gdiBitmapInfo,
                                                                gdiBitmapData,
                                                                out bitmap);

            SetNativeImage((GpImage)(IntPtr)bitmap);
        }
        */

        public BitmapPlus(
            HBITMAP hbm,
            IntPtr hpal
            )
        {
            GpBitmap bitmap = new GpBitmap();

            lastResult = NativeMethods.GdipCreateBitmapFromHBITMAP(hbm, hpal, out bitmap);

            SetNativeImage((GpImage)(IntPtr)bitmap);
        }




        public BitmapPlus FromFile(
            string filename)
        {
            return new BitmapPlus(filename);
        }


        public BitmapPlus FromStream(IStream stream)
        {
            return new BitmapPlus(stream);
        }



        public BitmapPlus FromHBITMAP(
            HBITMAP hbm,
            IntPtr hpal
            )
        {
            return new BitmapPlus(hbm, hpal);
        }



        public GpStatus GetHBITMAP(
            Color colorBackground,
            out HBITMAP hbmReturn
            )
        {
            return SetStatus(NativeMethods.GdipCreateHBITMAPFromBitmap(
                                            (GpBitmap)(IntPtr)nativeImage,
                                                out hbmReturn,
                                                colorBackground.ToArgb()));
        }




        public BitmapPlus(GpBitmap nativeBitmap)
        {
            lastResult = GpStatus.Ok;

            SetNativeImage((IntPtr)nativeBitmap);
        }


        public GpStatus LockBits(
            GpRect rect,
            uint flags,
            PixelFormat format,
            BitmapData lockedBitmapData
        )
        {
            return SetStatus(NativeMethods.GdipBitmapLockBits(
                                            (GpBitmap)(IntPtr)nativeImage,
                                            rect,
                                            flags,
                                            format,
                                            lockedBitmapData));
        }


        public GpStatus UnlockBits(BitmapData lockedBitmapData)
        {
            return SetStatus(NativeMethods.GdipBitmapUnlockBits(
                                            (GpBitmap)(IntPtr)nativeImage,
                                            lockedBitmapData));
        }


        public GpStatus GetPixel(
            int x,
            int y,
            out Color color)
        {
            int argb;
            color = Color.Transparent;
            GpStatus status = SetStatus(NativeMethods.GdipBitmapGetPixel(
                                            (GpBitmap)(IntPtr)nativeImage,
                x, y,
                out argb));

            if (status == GpStatus.Ok)
            {
                color = Color.FromArgb(argb);
            }

            return status;
        }


        public GpStatus SetPixel(
            int x,
            int y,
            Color color)
        {
            return SetStatus(NativeMethods.GdipBitmapSetPixel(
                                            (GpBitmap)(IntPtr)nativeImage,
                x, y,
                color.ToArgb()));
        }




    }
}
