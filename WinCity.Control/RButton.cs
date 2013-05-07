using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using WinCity.Control.Renderer;
using WinCity.Control.Listener;
using WinCity.Control.Event;
using PlatformAPI.GDIPlus;

namespace WinCity.Control
{
    public class RButton : RControl
    {
        static RButton()
        {
            ButtonRenderer renderer = new ButtonRenderer();
            RendererManager.Instance.AddRenderer(ButtonRenderer.NAME, renderer);
            brush = new SolidBrush(Color.Black);
            textBrush = new SolidBrush(Color.Black);
            pen = new Pen(Color.Black);

            brushPlus = new SolidBrushPlus(Color.Black);
        }

        public RButton()
        {
            IRenderer renderer = RendererManager.Instance.GetRenderer(ButtonRenderer.NAME);
            font = renderer.GetFont(ButtonRenderer.FONT);
            backColor = renderer.GetColor(ButtonRenderer.BACK_COLOR);
            foreColor = renderer.GetColor(ButtonRenderer.FORE_COLOR);
            borderColor = renderer.GetColor(ButtonRenderer.BORDER_COLOR);
            gradientTop = renderer.GetColor(ButtonRenderer.TOP_GRADIENT_COLOR);
            gradientBot = renderer.GetColor(ButtonRenderer.BOTTOM_GRADIENT_COLOR);
            borderWidth = renderer.GetNumber(ButtonRenderer.BORDER_WIDTH);
            gradientTopHeight = renderer.GetNumber(ButtonRenderer.TOP_GRADIENT_HEIGHT);
            gradientBotHeight = renderer.GetNumber(ButtonRenderer.BOTTOM_GRADIENT_HEIGHT);
            preferredHeight = renderer.GetNumber(ButtonRenderer.PREFERRED_HEIGHT);
            pressedBackColor = renderer.GetColor(ButtonRenderer.PRESSED_BACK_COLOR);

            AddMouseEventListener(new UIListener(this));
        }
        
        public virtual Color BackColor
        {
            get { return backColor; }
            set { backColor = value; }
        }

        public virtual Color ForeColor
        {
            get { return foreColor; }
            set { foreColor = value; }
        }

        public virtual Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; }
        }

        public virtual Font Font
        {
            get { return font; }
            set { font = value; }
        }

        public virtual string Text
        {
            get { return text; }
            set { text = value; }
        }
        
        public virtual int BorderWidth
        {
            get { return borderWidth; }
            set { borderWidth = value; }
        }

        public override void Dispose()
        {
        }

        public override void CalculateSize(Graphics utility)
        {
            base.CalculateSize(utility);

            SizeF sizef = utility.MeasureString(Text, Font);
            sizef.Height = Math.Max(sizef.Height, preferredHeight);
            PreferredSize = new Size((int)sizef.Width, (int)sizef.Height);
            MinimumSize = PreferredSize;
        }

        public override void Paint(Graphics graphics, Rectangle destRect)
        {
            PaintBackground(graphics, destRect);
            PaintBorder(graphics, destRect);
            PaintContent(graphics, destRect);
            //PaintShadow(graphics, destRect);
        }

        protected override void PaintBackground(Graphics graphics, Rectangle destRect)
        {
            Color color = backColor;
            if (pressed)
            {
                color = pressedBackColor;
            }
            if (!enabled)
            {
                color = Utility.GrayScale(color);
            }
            brush.Color = color;
            graphics.FillRectangle(brush, destRect);
        }

        protected virtual void PaintBorder(Graphics graphics, Rectangle destRect)
        {
            Color color = borderColor;
            if (!enabled)
            {
                color = Utility.GrayScale(color);
            }
            pen.Color = color;
            pen.Width = BorderWidth;
            graphics.DrawRectangle(pen, destRect);
        }

        protected virtual void PaintContent(Graphics graphics, Rectangle destRect)
        {
            Color color = foreColor;
            if (!enabled)
            {
                color = Utility.GrayScale(color);
            }
            textBrush.Color = color;
            SizeF sizef = graphics.MeasureString(text, font);
            float x = destRect.X + ((destRect.Width - sizef.Width) / 2);
            float y = destRect.Y + ((destRect.Height - sizef.Height) / 2);
            graphics.DrawString(text, font, textBrush, x, y);
        }

        protected virtual void PaintShadow(Graphics graphics, Rectangle destRect)
        {
            brush.Color = gradientTop;
            graphics.FillRectangle(brush, destRect);

            brush.Color = gradientBot;
            graphics.FillRectangle(brush, destRect);
        }

        protected void PaintExp(Graphics graphics, GpRect destRect)
        {
            IntPtr handle = graphics.GetHdc();
            HDC hdc = new HDC();
            hdc.val = handle;
            try
            {
                using (GraphicsPlus gp = new GraphicsPlus(hdc))
                {
                    brushPlus.SetColor(Color.Brown);
                    gp.FillRectangle(brushPlus, destRect);
                }
            }
            finally
            {
                graphics.ReleaseHdc(handle);
            }
        }

        private void Press()
        {
            pressed = !pressed;
            Update();
        }
        
        protected bool pressed = false;

        protected Color
            backColor,
            foreColor,
            borderColor,
            gradientTop,
            gradientBot,
            pressedBackColor;

        private int borderWidth,
            gradientTopHeight,
            gradientBotHeight,
            preferredHeight;

        private Font font;

        private string text;
        
        private static SolidBrush brush;
        
        private static Pen pen;

        private static SolidBrush textBrush;

        private static SolidBrushPlus brushPlus;
        
        private class UIListener : MouseEventAdapter
        {
            public UIListener(RButton button)
            {
                this.button = button;
            }
            
            public override void OnMouseDown(IRControl control, IMouseEvent e)
            {
                base.OnMouseDown(control, e);
                button.Press();
            }

            public override void OnMouseExit(IRControl control)
            {
                base.OnMouseExit(control);
                button.Press();
            }

            public override void OnMouseEnter(IRControl control)
            {
                base.OnMouseEnter(control);
                button.Press();
            }

            public override void OnMouseUp(IRControl control, IMouseEvent e)
            {
                base.OnMouseUp(control, e);
                button.Press();
            }

            private RButton button;
        }
    }
}
