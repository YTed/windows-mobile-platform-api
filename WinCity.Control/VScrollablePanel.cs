using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using WinCity.Control.Listener;
using WinCity.Control.Event;

namespace WinCity.Control
{
    public class VScrollablePanel : RPanel
    {
        public VScrollablePanel()
        {
            BlockEvent = false;
            AddMouseEventListener(new UIListener(this));
        }
        
        public override void Paint(Graphics graphics, Rectangle destRect)
        {
            PaintBackground(graphics, destRect);

            if (!hasLayout)
            {
                LayoutControl(graphics);
                hasLayout = true;
            }
            for (int i = 0; i < Count; i++)
            {
                IRControl control = this[i];
                if (control.Bounds.IntersectsWith(destRect))
                {
                    control.Paint(graphics, control.Bounds);
                }
            }

            if (totalHeight > bounds.Height)
            {
                using (Pen pen = new Pen(Color.LightGray))
                {
                    pen.Width = 4;
                    
                    int height = bounds.Height;
                    int tempy = Math.Abs(yoffset);
                    int ybegin = height * tempy / totalHeight + destRect.Y;
                    int yend = (tempy + height) * height / totalHeight + destRect.Y;
                    int right = destRect.Right,
                        top = destRect.Top,
                        bottom = destRect.Bottom;
                    
                    graphics.DrawLine(pen, right, top, right, bottom);
                    pen.Color = Color.Gold;
                    graphics.DrawLine(pen, right, ybegin, right, yend);
                }
            }
        }

        protected override void LayoutControl(Graphics utility)
        {
            base.LayoutControl(utility);

            int totalHeight = 0, height = Bounds.Height;
            for (int i = 0; i < Count; i++)
            {
                totalHeight += this[i].Bounds.Height;
            }

            if (yoffset < 0) // then means this.totalHeight > Bounds.height
            {
                if (height > totalHeight)
                {
                    yoffset = 0;
                }
                else if(yoffset + totalHeight < height)
                {
                    yoffset = height - totalHeight;
                }
            }
            this.totalHeight = totalHeight;

            for (int i = 0; i < Count; i++)
            {
                IRControl control = this[i];
                Rectangle newBounds = control.Bounds;
                newBounds.Offset(0, yoffset);
                control.Bounds = newBounds;
            }
        }

        private void BeginShift()
        {
            yoffset = stableYOffset;
        }

        private void Shift(int deltaX, int deltaY)
        {
            int tolerate = 30;
            if (totalHeight > Bounds.Height)
            {
                if (Math.Abs(deltaY) > tolerate)
                {
                    yoffset = stableYOffset + deltaY;
                    if (yoffset > 0)
                    {
                        yoffset = 0;
                    }
                    else if (yoffset + totalHeight < bounds.Height)
                    {
                        yoffset = bounds.Height - totalHeight;
                    }
                    LayoutOutdate();
                    Update();
                    allowPreformClick = false;
                }
            }
            else
            {
                yoffset = 0;
            }
        }

        private void EndShift()
        {
            stableYOffset = yoffset;
            allowPreformClick = true;
        }

        private int yoffset = 0;

        private int stableYOffset = 0;

        private int totalHeight = 0;

        private class UIListener : MouseEventAdapter
        {
            public UIListener(VScrollablePanel panel)
            {
                this.panel = panel;
            }

            public override void OnMouseDown(IRControl control, IMouseEvent e)
            {
                base.OnMouseDown(control, e);
                panel.BeginShift();
            }

            public override void OnMouseMove(IRControl control, IMouseEvent e)
            {
                base.OnMouseMove(control, e);
                panel.Shift(e.X - e.DownX, e.Y - e.DownY);
            }

            public override void OnMouseUp(IRControl control, IMouseEvent e)
            {
                base.OnMouseUp(control, e);
                panel.EndShift();
            }

            private VScrollablePanel panel;
        }

    }
}
