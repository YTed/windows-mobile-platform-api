using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace PlatformAPI.Imaging
{
    public class AlphaImage : IDisposable
    {
        protected AlphaImage()
        {
        }
        
        public static AlphaImage CreateFromFile(string file)
        {
            if (!string.IsNullOrEmpty(file) &&
                File.Exists(file))
            {
                IImagingFactory factory = CreateImageFactory();
                AlphaImage alphaImage = new AlphaImage();
                factory.CreateImageFromFile(file, out alphaImage.image);
                alphaImage.Initialize();
                return alphaImage;
            }
            return null;
        }

        public static AlphaImage CreateFromStream(Stream stream, int dataLength)
        {
            if (stream != null)
            {
                IImagingFactory factory = CreateImageFactory();
                AlphaImage alphaImage = new AlphaImage();
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    byte[] data = reader.ReadBytes(dataLength);
                    factory.CreateImageFromBuffer(data,
                        (uint)dataLength,
                        BufferDisposalFlag.BufferDisposalFlagNone,
                        out alphaImage.image);
                }
                alphaImage.Initialize();
                return alphaImage;
            }
            return null;
        }

        public virtual int Width
        {
            get
            {
                if (initialized)
                {
                    return (int)imageInfo.Width;
                }
                return 0;
            }
        }

        public virtual int Height
        {
            get
            {
                if (initialized)
                {
                    return (int)imageInfo.Height;
                }
                return 0;
            }
        }

        public virtual void DirectDraw(Graphics graphics, int x, int y, int width, int height)
        {
            if (image != null)
            {
                Rectangle destRect = new Rectangle(x, y, x + width, y + height);
                IntPtr hdcorg = graphics.GetHdc();
                try
                {
                    image.Draw(hdcorg, ref destRect, IntPtr.Zero);
                }
                finally
                {
                    graphics.ReleaseHdc(hdcorg);
                }
            }
        }
        
        public virtual void DrawImage(Graphics graphics, int x, int y)
        {
            if (image == null)
            {
                return;
            }
            PrepareBuffer(graphics, x, y);
            DrawBuffer(graphics, x, y);
        }

        public virtual void DrawBuffer(Graphics graphics, int x, int y)
        {
            if (buffer != null)
            {
                graphics.DrawImage(buffer, x, y);
            }
        }

        public virtual AlphaImage GetThumnail(int width, int height)
        {
            if (image != null && 
                width > 0 &&
                height > 0 &&
                width < Width &&
                height < Height)
            {
                AlphaImage thumnail = new AlphaImage();
                image.GetThumbnail((uint)width, (uint)height, out thumnail.image);
                thumnail.Initialize();
                return thumnail;
            }
            return null;
        }
        
        public virtual void PrepareBuffer(Graphics graphics, int x , int y)
        {
            ImageInfo info;
            image.GetImageInfo(out info);
            if (buffer == null)
            {
                buffer = new Bitmap((int)info.Width, (int)info.Height);
            }
            using (Graphics graphicsBuffer = Graphics.FromImage(buffer))
            {
                IntPtr hdcorg = graphics.GetHdc();
                IntPtr hdcbuffer = graphicsBuffer.GetHdc();
                try
                {
                    DrawingAPI.BitBlt(hdcbuffer, 0, 0, buffer.Width, buffer.Height,
                        hdcorg, x, y, DrawingAPI.SRCCOPY);

                    Rectangle bufferRect = new Rectangle();
                    bufferRect.X = 0;
                    bufferRect.Y = 0;
                    bufferRect.Width = buffer.Width;
                    bufferRect.Height = buffer.Height;
                    image.Draw(hdcbuffer, ref bufferRect, IntPtr.Zero);
                }
                finally
                {
                    graphics.ReleaseHdc(hdcorg);
                    graphicsBuffer.ReleaseHdc(hdcbuffer);
                }
            }
        }

        #region IDisposable 成员

        public virtual void Dispose()
        {
            if (image != null)
            {
                Marshal.ReleaseComObject(image);
            }
            if (buffer != null)
            {
                buffer.Dispose();
            }
        }

        #endregion

        private void Initialize()
        {
            if (image != null)
            {
                image.GetImageInfo(out imageInfo);
                initialized = true;
            }
        }
        
        private static IImagingFactory CreateImageFactory()
        {
            if (factory == null)
            {
                Guid guid = new Guid("327ABDA8-072B-11D3-9D7B-0000F81EF32E");
                Type type = Type.GetTypeFromCLSID(guid);
                factory = Activator.CreateInstance(type) as IImagingFactory;
            }
            return factory;
        }

        protected ImageInfo imageInfo;
        protected IImage image;
        protected Image buffer;
        protected bool initialized = false;
        
        private static IImagingFactory factory;
    }
}
