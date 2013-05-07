using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace WinCity.Control
{
    public class RLabel : RControl
    {
        public override void Dispose()
        {
            
        }

        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                sizeCalculated = false;
            }
        }

        public Color ForeColor
        {
            get { return foreColor; }
            set { foreColor = value; }
        }

        public Color BackColor
        {
            get { return backColor; }
            set { backColor = value; }
        }
        
        public Font Font
        {
            get { return font; }
            set
            {
                font = value;
                sizeCalculated = false;
            }
        }

        public bool Transparent
        {
            get { return transparent; }
            set { transparent = value; }
        }

        public override void CalculateSize(Graphics utility)
        {
            base.CalculateSize(utility);

            if (!string.IsNullOrEmpty(Text) &&
                Font != null)
            {
                SizeF sizef = utility.MeasureString(Text, Font);
                Size size = new Size(
                    (int)Math.Ceiling(sizef.Width),
                    (int)Math.Ceiling(sizef.Height));
                MinimumSize = size;

                if (!sizeCalculated)
                {
                    PreferredSize = size;
                    MaximumSize = size;
                    sizeCalculated = true;
                }
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
                sizeCalculated = true;
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
                sizeCalculated = true;
            }
        }

        public TextAlignment Alignment
        {
            get { return alignment; }
            set { alignment = value; }
        }
        
        protected override void PaintBackground(Graphics graphics, Rectangle destRect)
        {
            base.PaintBackground(graphics, destRect);

            if (!Transparent)
            {
                Region orgRegion = graphics.Clip;
                Region newRegion = new Region(destRect);
                graphics.Clip = newRegion;
                graphics.Clear(backColor);
                graphics.Clip = orgRegion;
            }
        }
        
        protected override void PaintEnabled(Graphics graphics, Rectangle destRect)
        {
            base.PaintEnabled(graphics, destRect);
            if (!string.IsNullOrEmpty(Text) && Font != null)
            {
                using (SolidBrush brush = new SolidBrush(ForeColor))
                {
                    int x = destRect.X;
                    if (alignment == TextAlignment.Center)
                    {
                        x = destRect.X + ((destRect.Width - Bounds.Width) >> 1);
                    }
                    else if (alignment == TextAlignment.Right)
                    {
                        x = destRect.Right - Bounds.Width;
                    }
                    int y = destRect.Y + ((destRect.Height - Bounds.Height) >> 1);
                    graphics.DrawString(Text, Font, brush, x, y);
                }
            }
        }

        protected override void PaintDisabled(Graphics graphics, Rectangle destRect)
        {
            base.PaintDisabled(graphics, destRect);
            Color color = ForeColor;
            ForeColor = Color.Gray;
            PaintEnabled(graphics, destRect);
            ForeColor = color;
        }
        
        private string text;

        private Color foreColor, backColor;

        private bool transparent = true;

        private Font font;

        private bool sizeCalculated;

        private TextAlignment alignment;
    }
}
