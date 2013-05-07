using System;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Text;
using PlatformAPI.Runtime.InteropServices.ComTypes;

namespace PlatformAPI.GDIPlus
{
    public static partial class NativeMethods
    {
        //----------------------------------------------------------------------------
        // Image APIs
        //----------------------------------------------------------------------------

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipLoadImageFromStream(IStream stream, out GpImage image);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipLoadImageFromFile([MarshalAs(UnmanagedType.BStr)]string filename, out GpImage image);

        //[DllImport("gdiplus")] public static extern GpStatus 
        //GdipLoadImageFromStreamICM(IStream* stream, out GpImage image);

        //[DllImport("gdiplus")] public static extern GpStatus 
        //GdipLoadImageFromFileICM(string  filename, out GpImage image);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipCloneImage(GpImage image, out GpImage cloneImage);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipDisposeImage(GpImage image);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipSaveImageToFile(GpImage image, string filename,
     ref Guid clsidEncoder,
     EncoderParameters encoderParams);

        //[DllImport("gdiplus")] public static extern GpStatus 
        //GdipSaveImageToStream(GpImage image, IStream* stream,
        //                      ref Guid clsidEncoder, 
        //                      EncoderParameters* encoderParams);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipSaveAdd(GpImage image, EncoderParameters encoderParams);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipSaveAddImage(GpImage image, GpImage newImage,
  EncoderParameters encoderParams);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipGetImageGraphicsContext(GpImage image, out GpGraphics graphics);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipGetImageBounds(GpImage image, out GpRectF srcRect, out Unit srcUnit);
        [DllImport("gdiplus")]
        public static extern GpStatus
GdipGetImageBounds(GpImage image, float[] srcRect, Unit srcUnit);
        [DllImport("gdiplus")]
        public static extern GpStatus
GdipGetImageBounds(GpImage image, byte[] srcRect, Unit srcUnit);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipGetImageDimension(GpImage image, out float width, out float height);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipGetImageType(GpImage image, out ImageType type);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipGetImageWidth(GpImage image, out uint width);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipGetImageHeight(GpImage image, out uint height);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipGetImageHorizontalResolution(GpImage image, out float resolution);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipGetImageVerticalResolution(GpImage image, out float resolution);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipGetImageFlags(GpImage image, out uint flags);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipGetImageRawFormat(GpImage image, out Guid format);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipGetImagePixelFormat(GpImage image, out PixelFormat format);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipGetImageThumbnail(GpImage image, uint thumbWidth, uint thumbHeight,
       out GpImage thumbImage,
       IntPtr /*GetThumbnailImageAbort*/ callback, IntPtr callbackData);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipGetEncoderParameterListSize(GpImage image, ref Guid clsidEncoder,
                 out uint size);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipGetEncoderParameterList(GpImage image, ref Guid clsidEncoder,
             uint size, EncoderParameters buffer);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipImageGetFrameDimensionsCount(GpImage image, out uint count);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipImageGetFrameDimensionsList(GpImage image, Guid[] dimensionIDs,
                 uint count);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipImageGetFrameCount(GpImage image, ref Guid dimensionID,
        out uint count);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipImageSelectActiveFrame(GpImage image, ref Guid dimensionID,
            uint frameIndex);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipImageRotateFlip(GpImage image, RotateFlipType rfType);

        //[DllImport("gdiplus")] public static extern GpStatus 
        //GdipGetImagePalette(GpImage image, ColorPalette *palette, int size);

        //[DllImport("gdiplus")] public static extern GpStatus 
        //GdipSetImagePalette(GpImage image, ColorPalette *palette);

        //[DllImport("gdiplus")] public static extern GpStatus 
        //GdipGetImagePaletteSize(GpImage image, out int size);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipGetPropertyCount(GpImage image, out uint numOfProperty);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipGetPropertyIdList(GpImage image, uint numOfProperty, PROPID[] list);

        [DllImport("gdiplus")]
        public static extern GpStatus
GdipGetPropertyItemSize(GpImage image, PROPID propId, out uint size);

        /*
        [DllImport("gdiplus")] public static extern GpStatus 
GdipGetPropertyItem(GpImage image, PROPID propId, uint propSize,
                    PropertyItem* buffer);

[DllImport("gdiplus")] public static extern GpStatus 
GdipGetPropertySize(GpImage image, out uint totalBufferSize, 
                    out uint numProperties);

[DllImport("gdiplus")] public static extern GpStatus 
GdipGetAllPropertyItems(GpImage image, uint totalBufferSize,
                        uint numProperties, PropertyItem* allItems);

[DllImport("gdiplus")] public static extern GpStatus 
GdipRemovePropertyItem(GpImage image, PROPID propId);

[DllImport("gdiplus")] public static extern GpStatus 
GdipSetPropertyItem(GpImage image, PropertyItem* item);

[DllImport("gdiplus")] public static extern GpStatus 
GdipImageForceValidation(GpImage image);
         */
        [DllImport("gdiplus")] public static extern GpStatus 
GdipDrawImage(GpGraphics graphics, GpImage image, float x, float y);

[DllImport("gdiplus")] public static extern GpStatus 
GdipDrawImageI(GpGraphics graphics, GpImage image, int x, int y);

[DllImport("gdiplus")] public static extern GpStatus 
GdipDrawImageRect(GpGraphics graphics, GpImage image, float x, float y,
                           float width, float height);

[DllImport("gdiplus")] public static extern GpStatus 
GdipDrawImageRectI(GpGraphics graphics, GpImage image, int x, int y,
                           int width, int height);

[DllImport("gdiplus")] public static extern GpStatus 
GdipDrawImagePoints(GpGraphics graphics, GpImage image,
                             GpPointF[] dstpoints, int count);

[DllImport("gdiplus")] public static extern GpStatus 
GdipDrawImagePointsI(GpGraphics graphics, GpImage image,
                             GpPoint[] dstpoints, int count);

[DllImport("gdiplus")] public static extern GpStatus 
GdipDrawImagePointRect(GpGraphics graphics, GpImage image, float x,
                                float y, float srcx, float srcy, float srcwidth,
                                float srcheight, Unit srcUnit);

[DllImport("gdiplus")] public static extern GpStatus 
GdipDrawImagePointRectI(GpGraphics graphics, GpImage image, int x,
                                int y, int srcx, int srcy, int srcwidth,
                                int srcheight, Unit srcUnit);

[DllImport("gdiplus")] public static extern GpStatus 
GdipDrawImageRectRect(GpGraphics graphics, GpImage image, float dstx,
                      float dsty, float dstwidth, float dstheight,
                      float srcx, float srcy, float srcwidth, float srcheight,
                      Unit srcUnit,
                      GpImageAttributes imageAttributes,
                      IntPtr callback, IntPtr callbackData);

[DllImport("gdiplus")] public static extern GpStatus 
GdipDrawImageRectRectI(GpGraphics graphics, GpImage image, int dstx,
                       int dsty, int dstwidth, int dstheight,
                       int srcx, int srcy, int srcwidth, int srcheight,
                       Unit srcUnit,
                       GpImageAttributes imageAttributes,
                       IntPtr callback, IntPtr callbackData);

[DllImport("gdiplus")] public static extern GpStatus 
GdipDrawImagePointsRect(GpGraphics graphics, GpImage image,
                        GpPointF[] points, int count, float srcx,
                        float srcy, float srcwidth, float srcheight,
                        Unit srcUnit,
                        GpImageAttributes imageAttributes,
                        IntPtr callback, IntPtr callbackData);

[DllImport("gdiplus")] public static extern GpStatus 
GdipDrawImagePointsRectI(GpGraphics graphics, GpImage image,
                         GpPoint[] points, int count, int srcx,
                         int srcy, int srcwidth, int srcheight,
                         Unit srcUnit,
                         GpImageAttributes imageAttributes,
IntPtr callback, IntPtr callbackData);

    }
}
