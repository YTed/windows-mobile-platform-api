using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using WinCity.Control.Event;
using WinCity.Control.Listener;

namespace WinCity.Control
{
    public class RPanel : RContainer, IRControl
    {
        public RPanel()
        {
            Initialize();
        }

        public RPanel(System.Windows.Forms.Control peer)
            : base(peer)
        {
            Initialize();
        }

        public virtual bool BackgroundTransparent
        {
            get { return backgroundTransparent; }
            set { backgroundTransparent = value; }
        }
        
        public virtual Color BackColor
        {
            get { return backColor; }
            set { backColor = value; }
        }

        public virtual Image BackgroundImage
        {
            get { return backImage; }
            set { backImage = value; }
        }

        public virtual PictureSizeMode BackgroundImageMode
        {
            get { return backImageMode; }
            set { backImageMode = value; }
        }

        protected override void PaintBackground(Graphics graphics, Rectangle drawingArea)
        {
            if (!backgroundTransparent)
            {
                base.PaintBackground(graphics, drawingArea);

                graphics.Clear(BackColor);
                if (backImage != null)
                {
                    Rectangle destRect = new Rectangle(0, 0, backImage.Width, backImage.Height);
                    Rectangle srcRect = new Rectangle(0, 0, backImage.Width, backImage.Height);
                    switch (backImageMode)
                    {
                        case PictureSizeMode.Normal:
                            break;

                        case PictureSizeMode.Center:
                            destRect.X = (Bounds.Width - backImage.Width) >> 1;
                            destRect.Y = (Bounds.Height - backImage.Height) >> 1;
                            break;
                        case PictureSizeMode.Stretch:
                            destRect.Width = Bounds.Width;
                            destRect.Height = Bounds.Height;
                            srcRect.Width = Bounds.Width;
                            srcRect.Height = Bounds.Height;
                            break;
                    }
                    destRect.Intersect(drawingArea);
                    srcRect.Intersect(drawingArea);
                    graphics.DrawImage(backImage, destRect, srcRect, GraphicsUnit.Pixel);
                }
            }
        }

        private void Initialize()
        {
            meNotifier = new MouseListenerNotifier();
            meNotifier.Control = this;
        }
        
        protected Image backImage = null;
        
        protected PictureSizeMode backImageMode = PictureSizeMode.Normal;

        private bool backgroundTransparent = false;

        private Color backColor = Color.White;
    }
}
