using System;
using System.Collections.Generic;
using System.Text;
using PlatformAPI.Runtime.InteropServices.ComTypes;

namespace PlatformAPI.GDIPlus
{
    public class ImagePlus : IDisposable
    {


        public ImagePlus(string filename, bool useEmbeddedColorManagement)
        {
        }

        public ImagePlus(
            IStream stream,
            bool useEmbeddedColorManagement
        )
        {
            NativeMethods.GdipLoadImageFromStream(stream, out nativeImage);
        }

        //static public ImagePlus FromFile(
        //    string filename,
        //    bool useEmbeddedColorManagement 
        //)
        //{
        //}

        static public ImagePlus FromStream(
            IStream stream,
            bool useEmbeddedColorManagement
        )
        {
            return new ImagePlus(stream, useEmbeddedColorManagement);
        }

        ~ImagePlus()
        {
        }

        //    public virtual ImagePlus Clone()
        //    {
        //    }

        //GpStatus Save(string filename,
        //            ref Guid clsidEncoder,
        //            EncoderParameters encoderParams )
        //{
        //}
        //GpStatus Save(IStream stream,
        //            ref Guid clsidEncoder,
        //            EncoderParameters encoderParams )
        //{
        //}

        //    GpStatus SaveAdd(EncoderParameters encoderParams)
        //    {
        //    }
        //GpStatus SaveAdd(ImagePlus newImage,
        //               EncoderParameters encoderParams)
        //{
        //}

        //ImageType GetType() {}
        public GpStatus GetPhysicalDimension(out GpSizeF size)
        {

            float width, height;
            GpStatus status;

            status = SetStatus(NativeMethods.GdipGetImageDimension(nativeImage,
                                                                 out width, out height));

            size.Width = width;
            size.Height = height;

            return status;
        }

        public GpStatus GetBounds(out GpRectF srcRect,
                         out Unit srcUnit)
        {
            return SetStatus(NativeMethods.GdipGetImageBounds(nativeImage,
                                                    out srcRect, out srcUnit));
        }

        public uint GetWidth()
        {
            GpRectF rc;
            Unit unit;
            GetBounds(out rc, out unit);
            return (uint)rc.Width;
        }
        public uint GetHeight()
        {
            GpRectF rc;
            Unit unit;
            GetBounds(out rc, out unit);
            return (uint)rc.Height;
        }
        //float GetHorizontalResolution();
        //float GetVerticalResolution();
        //uint GetFlags();
        public GpStatus GetRawFormat(out Guid format)
        {
            return SetStatus(NativeMethods.GdipGetImageRawFormat(nativeImage, out format));
        }

        public System.Drawing.Imaging.PixelFormat GetPixelFormat()
        {

            System.Drawing.Imaging.PixelFormat format;

            SetStatus(NativeMethods.GdipGetImagePixelFormat(nativeImage, out format));

            return format;

        }

        //int GetPaletteSize();
        //Status GetPalette(OUT ColorPalette* palette,
        //                  int size);
        //Status SetPalette(ColorPalette* palette);

        //ImagePlus GetThumbnailImage(uint thumbWidth,
        //                         uint thumbHeight);
        //uint GetFrameDimensionsCount();
        //GpStatus GetFrameDimensionsList(Guid[] dimensionIDs,
        //                              uint count);
        //uint GetFrameCount(ref Guid dimensionID);
        //GpStatus SelectActiveFrame(ref Guid dimensionID,
        //                         uint frameIndex);
        //GpStatus RotateFlip(RotateFlipType rotateFlipType);
        //uint GetPropertyCount();
        //GpStatus GetPropertyIdList(uint numOfProperty,
        //                         out PROPID[] list);
        //uint GetPropertyItemSize(PROPID propId);
        //Status GetPropertyItem(PROPID propId,
        //                       uint propSize,
        //                       OUT PropertyItem* buffer);
        //Status GetPropertySize(OUT UINT* totalBufferSize,
        //                       OUT UINT* numProperties);
        //Status GetAllPropertyItems(uint totalBufferSize,
        //                           uint numProperties,
        //                           OUT PropertyItem* allItems);
        //Status RemovePropertyItem(PROPID propId);
        //Status SetPropertyItem(PropertyItem* item);

        //uint  GetEncoderParameterListSize(CLSID* clsidEncoder);
        //Status GetEncoderParameterList(CLSID* clsidEncoder,
        //                               uint size,
        //                               OUT EncoderParameters* buffer);

        public GpStatus GetLastStatus()
        {
            return lastResult;
        }



        internal ImagePlus() { }

        internal ImagePlus(GpImage nativeImage, GpStatus status)
        {
            SetNativeImage(nativeImage);
        }

        internal void SetNativeImage(GpImage nativeImage)
        {
            this.nativeImage = nativeImage;
        }

        protected GpStatus SetStatus(GpStatus status)
        {
            if (status != GpStatus.Ok)
                return (lastResult = status);
            else
                return status;
        }

        internal GpImage nativeImage;
        protected GpStatus lastResult;
        protected GpStatus loadStatus;


        private ImagePlus(ImagePlus C)
        {

            NativeMethods.GdipCloneImage(C.nativeImage, out this.nativeImage);
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (nativeImage != IntPtr.Zero)
            {
                NativeMethods.GdipDisposeImage(nativeImage);
                nativeImage = null;
            }
        }

        #endregion
    }
}
