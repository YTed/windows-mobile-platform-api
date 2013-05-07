using System;
using System.Collections.Generic;
using System.Text;

namespace WinCity.Control.Event
{
    public class SimpleMouseEvent: IMouseEvent
    {
        public SimpleMouseEvent(
            MouseEventTypes type,
            int x, int y,
            int lastX, int lastY,
            int downX, int downY)
        {
            this.type = type;
            this.x = x;
            this.y = y;
            this.lastx = lastX;
            this.lasty = lastY;
            this.downx = downX;
            this.downy = downY;
        }

        public SimpleMouseEvent(MouseEventTypes type, IMouseEvent sourceEvent)
            : this(type, sourceEvent.X, sourceEvent.Y, 
                    sourceEvent.LastX, sourceEvent.LastY,
                    sourceEvent.DownX, sourceEvent.DownY)
        {
        }

        public SimpleMouseEvent(MouseEventTypes type, MouseEventTracker tracker)
            : this(type, tracker.X, tracker.Y,
                    tracker.LastX, tracker.LastY,
                    tracker.DownX, tracker.DownY)
        {
        }
        
        #region IMouseEvent 成员
        
        public MouseEventTypes EventType
        {
            get { return type; }
        }

        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get { return y; }
        }

        public int LastX
        {
            get { return lastx; }
        }

        public int LastY
        {
            get { return lasty; }
        }

        public int DownX
        {
            get { return downx; }
        }

        public int DownY
        {
            get { return downy; }
        }
        
        #endregion

        private int x, y, lastx, lasty, downx, downy;
        
        private MouseEventTypes type;
    }
}
