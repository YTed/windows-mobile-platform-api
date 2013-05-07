using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using PlatformAPI.Imaging;
using WinCity.Control.Event;
using WinCity.Control.Listener;
using WinCity.Control.Renderer;

namespace WinCity.Control
{
    public class ImageButton : RControl
    {
        static ImageButton()
        {
            IRendererManager rdrMgr = RendererManager.Instance;
            IRenderer renderer = new ImageButtonRenderer();
            rdrMgr.AddRenderer(ImageButtonRenderer.NAME, renderer);
        }
        
        public ImageButton()
        {
            uiListener = new UIListener();
            uiListener.ui = this;
            AddMouseEventListener(uiListener);

            IRendererManager rdrMgr = RendererManager.Instance;
            IRenderer renderer = rdrMgr.GetRenderer(ImageButtonRenderer.NAME);
            foreColor = renderer.GetColor(ImageButtonRenderer.FORE_COLOR);
            font = renderer.GetFont(ImageButtonRenderer.FONT);
            bool showText = renderer.GetBoolean(ImageButtonRenderer.SHOW_TEXT);
            if (!showText)
            {
                TextAlignment |= TextImageAlignment.ImageOnly;
            }
        }

        public override Size PreferredSize
        {
            get
            {
                return base.PreferredSize;
            }
            set
            {
                base.PreferredSize = value;
                preferredSizeSet = true;
            }
        }

        public override Size MinimumSize
        {
            get
            {
                return base.MinimumSize;
            }
            set
            {
                base.MinimumSize = value;
                minimumSizeSet = true;
            }
        }

        public override Size MaximumSize
        {
            get
            {
                return base.MaximumSize;
            }
            set
            {
                base.MaximumSize = value;
                maximumSizeSet = true;
            }
        }
        
        public virtual AlphaImage NormalImage
        {
            get { return normalImage; }
            set { normalImage = value; }
        }

        public virtual AlphaImage DisableImage
        {
            get { return disableImage; }
            set { disableImage = value; }
        }

        public virtual AlphaImage PressedImage
        {
            get
            {
                if (pressedImage != null)
                {
                    return pressedImage;
                }
                else
                {
                    return NormalImage;
                }
            }
            set { pressedImage = value; }
        }

        public virtual string Text
        {
            get { return text; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    text = value;
                }
                else
                {
                    text = "";
                }
            }
        }

        public virtual Font Font
        {
            get { return font; }
            set { font = value; }
        }

        public virtual Color ForeColor
        {
            get { return foreColor; }
            set { foreColor = value; }
        }

        public virtual TextImageAlignment TextAlignment
        {
            get { return textAlignment; }
            set { textAlignment = value; }
        }

        public virtual void BufferOutdate()
        {
            pressedBufferPrepared = false;
            disableBufferPrepared = false;
            normalBufferPrepared = false;
        }

        public virtual void PerformClick()
        {

        }

        public override void CalculateSize(Graphics utility)
        {
            base.CalculateSize(utility);

            if (NormalImage == null)
            {
                return;
            }

            Rectangle imageRect = new Rectangle();
            Rectangle textRect = new Rectangle();
            Rectangle destRect = new Rectangle();
            Layout(utility, NormalImage, destRect, ref imageRect, ref textRect);
            destRect = Union(imageRect, textRect);

            int gap = 6;
            if (!maximumSizeSet)
            {
                maximumSize.Width = -1;
                maximumSize.Height = -1;
            }
            if (!minimumSizeSet)
            {
                minimumSize.Width = destRect.Width;
                minimumSize.Height = destRect.Height;
            }
            if (!preferredSizeSet)
            {
                preferredSize.Width = minimumSize.Width + gap;
                preferredSize.Height = minimumSize.Height + gap;
            }
        }
        
        #region IDisposable 成员

        public override void Dispose()
        {
            normalImage.Dispose();
        }

        #endregion
        
        protected override void PaintEnabled(Graphics graphics, Rectangle destRect)
        {
            base.PaintEnabled(graphics, destRect);

            if (uiListener.pressed && PressedImage != null)
            {
                destRect.Offset(1, 1);
                DrawImage(PressedImage, graphics, destRect, ref pressedBufferPrepared);
            }
            else
            {
                DrawImage(NormalImage, graphics, destRect, ref normalBufferPrepared);
            }
        }

        protected override void PaintDisabled(Graphics graphics, Rectangle destRect)
        {
            base.PaintDisabled(graphics, destRect);

            if (DisableImage != null)
            {
                DrawImage(DisableImage, graphics, destRect, ref disableBufferPrepared);
            }
        }

        private void DrawImage(
            AlphaImage image,
            Graphics graphics,
            Rectangle destRect,
            ref bool bufferPrepared)
        {
            Rectangle imgRect = new Rectangle(),
                txtRect = new Rectangle();

            Layout(graphics, image, destRect, ref imgRect, ref txtRect);

            if ((image != null) &&
                !Utility.TIAHasComponent(TextAlignment, TextImageAlignment.TextOnly))
            {
                if (!bufferPrepared)
                {
                    image.DrawImage(graphics, imgRect.X, imgRect.Y);
                    bufferPrepared = true;
                }
                else
                {
                    image.DrawBuffer(graphics, imgRect.X, imgRect.Y);
                }
            }
            if (!string.IsNullOrEmpty(Text) && (Font != null) &&
                !Utility.TIAHasComponent(TextAlignment, TextImageAlignment.ImageOnly))
            {
                using (Brush solidBrush = new SolidBrush(ForeColor))
                {
                    graphics.DrawString(Text, Font, solidBrush, txtRect.X, txtRect.Y);
                }
            }
        }

        private void Layout(
            Graphics graphics,
            AlphaImage image,
            Rectangle destRect,
            ref Rectangle imgRect,
            ref Rectangle txtRect)
        {
            if (image != null)
            {
                imgRect.Width = image.Width;
                imgRect.Height = image.Height;
                imgRect.X = 0;
                imgRect.Y = 0;
            }
            if (!string.IsNullOrEmpty(Text) && (Font != null))
            {
                SizeF size = graphics.MeasureString(Text, Font);
                txtRect.Width = (int)size.Width;
                txtRect.Height = (int)size.Height;
                txtRect.X = 0;
                txtRect.Y = 0;
            }
            
            if (Utility.TIAHasComponent(TextAlignment, TextImageAlignment.TextHCenter))
            {
                int deltaWidth = Math.Abs(txtRect.Width - imgRect.Width) >> 1;
                if (txtRect.Width > imgRect.Width)
                {
                    txtRect.X = 0;
                    imgRect.X = deltaWidth;
                }
                else
                {
                    imgRect.X = 0;
                    txtRect.X = deltaWidth;
                }
            }
            else if (Utility.TIAHasComponent(TextAlignment, TextImageAlignment.TextLeft))
            {
                imgRect.X = txtRect.Width;
            }
            else
            {
                txtRect.X = imgRect.Width;
            }
            
            if (Utility.TIAHasComponent(TextAlignment, TextImageAlignment.TextVCenter))
            {
                int deltaHeight = Math.Abs(txtRect.Height - imgRect.Height) >> 1;
                if (txtRect.Height > imgRect.Height)
                {
                    txtRect.Y = 0;
                }
                else
                {
                    imgRect.Y = 1;
                }
            }
            else if (Utility.TIAHasComponent(TextAlignment, TextImageAlignment.TextTop))
            {
                imgRect.Y = txtRect.Height;
            }
            else
            {
                txtRect.Y = imgRect.Height;
            }

            imgRect.Offset(destRect.X, destRect.Y);
            txtRect.Offset(destRect.X, destRect.Y);
            Rectangle unionRect = Union(imgRect, txtRect);
            if (destRect.Width > unionRect.Width)
            {
                int dx = (destRect.Width - unionRect.Width) >> 1;
                imgRect.X = imgRect.X + dx;
                txtRect.X = txtRect.X + dx;
            }
            if (destRect.Height > unionRect.Height)
            {
                int dy = (destRect.Height - unionRect.Height) >> 1;
                imgRect.Y = imgRect.Y + dy;
                txtRect.Y = txtRect.Y + dy;
            }
        }

        private Rectangle Union(Rectangle one, Rectangle two)
        {
            int minx = Math.Min(one.Left, two.Left),
                miny = Math.Min(one.Top, two.Top),
                maxx = Math.Max(one.Right, two.Right),
                maxy = Math.Max(one.Bottom, two.Bottom);
            return new Rectangle(minx, miny, maxx - minx, maxy - miny);
        }

        private TextImageAlignment textAlignment = TextImageAlignment.Default;
        
        private AlphaImage pressedImage, disableImage, normalImage;

        private bool pressedBufferPrepared = false,
            disableBufferPrepared = false,
            normalBufferPrepared = false;

        protected bool preferredSizeSet = false,
            minimumSizeSet = false,
            maximumSizeSet = false;
        
        private string text = "";

        private Color foreColor = Color.Black;

        private Font font;

        private UIListener uiListener;

        private class UIListener : MouseEventAdapter
        {
            #region IMouseEventListener 成员

            public override void OnMouseExit(IRControl control)
            {
                pressed = false;
                ui.Update();
            }

            public override void OnMouseDown(IRControl control, IMouseEvent me)
            {
                pressed = true;
                ui.Update();
            }

            public override void OnMouseUp(IRControl control, IMouseEvent me)
            {
                pressed = false;
                ui.Update();
            }

            public override void OnMouseEnter(IRControl control)
            {
                pressed = true;
                ui.Update();
            }

            public override void OnClick(IRControl control)
            {
                base.OnClick(control);
                ui.PerformClick();
            }
            
            #endregion

            public bool pressed = false;
            public ImageButton ui;
        }
    }
}
