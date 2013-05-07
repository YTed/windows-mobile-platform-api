using System;
using System.Collections.Generic;
using System.Text;
using WinCity.Control.LayoutManager;
using System.Drawing;
using WinCity.Control.Listener;
using WinCity.Control.Event;

namespace WinCity.Control
{
    public class RListView : RPanel
    {
        public RListView()
        {
            BlockEvent = false;
            BindLayoutManager(new FlowLayout());
            AddMouseEventListener(new ShiftListener(this));
        }
        
        public void AddItem(IRControl item)
        {
            AddControl(item, FlowLayoutEnum.Next);
        }

        public void RemoveItem(IRControl item)
        {
            RemoveControl(item);
        }

        public int ItemCount
        {
            get { return Count; }
        }

        public Size ItemSize
        {
            get { return itemSize; }
            set { itemSize = value; }
        }
        
        protected override void LayoutControl(Graphics utility)
        {
            Size size = new Size(Bounds.Width, ItemSize.Height);
            for (int i = 0; i < Count; i++)
            {
                this[i].PreferredSize = size;
                this[i].MinimumSize = size;
            }

            base.LayoutControl(utility);

            for (int i = 0; i < Count; i++)
            {
                Rectangle rect = this[i].Bounds;
                rect.Offset(0, yoffset);
                this[i].Bounds = rect;
            }
        }

        protected override void PaintSubControls(Graphics graphics, IRControl control)
        {
            base.PaintSubControls(graphics, control);

            maxHeight = 0;
            for (int i = 0; i < Count; i++)
            {
                maxHeight += this[i].Bounds.Height;
            }
        }

        private void BeginShift()
        {
            yoffset = stableYOffset;
            allowPreformClick = true;
        }
        
        private bool Shift(int deltaX, int deltaY)
        {
            if (maxHeight > Bounds.Height &&
                Math.Abs(deltaY) > (ItemSize.Height >> 2))
            {
                yoffset = stableYOffset + deltaY;
                logbuilder.Append(string.Format("yoffset:{0}\n", yoffset));
                if (yoffset > 0)
                {
                    yoffset = 0;
                }
                else if (maxHeight + yoffset < Bounds.Height)
                {
                    yoffset = Bounds.Height - maxHeight;
                }

                LayoutOutdate();
                Update();
                allowPreformClick = false;
            }
            return !allowPreformClick;
        }

        private void EndShift()
        {
            stableYOffset = yoffset;
        }

        private int maxHeight = 0;
        
        private int yoffset = 0, stableYOffset = 0;

        private Size itemSize;

        StringBuilder logbuilder = new StringBuilder();
        
        private class ShiftListener : MouseEventAdapter
        {
            public ShiftListener(RListView listView)
            {
                this.listView = listView;
            }
            
            public override void OnMouseDown(IRControl control, IMouseEvent e)
            {
                base.OnMouseDown(control, e);
                listView.BeginShift();
            }

            public override void OnMouseUp(IRControl control, IMouseEvent e)
            {
                base.OnMouseUp(control, e);
                listView.EndShift();
            }

            public override void OnMouseMove(IRControl control, IMouseEvent e)
            {
                base.OnMouseMove(control, e);
                listView.Shift(e.X - e.DownX, e.Y - e.DownY);
            }

            private RListView listView;
        }
    }
}
