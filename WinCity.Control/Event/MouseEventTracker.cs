using System;
using System.Collections.Generic;
using System.Text;

namespace WinCity.Control.Event
{
    public class MouseEventTracker : IMouseEvent, IMouseEventTracker
    {
        public MouseEventTracker()
        {
            xList = new List<int>();
            yList = new List<int>();
        }
        
        #region IMouseEvent 成员

        public MouseEventTypes EventType
        {
            get { return type; }
        }

        public int X
        {
            get
            {
                if (xList.Count > 0)
                {
                    return xList[xList.Count - 1];
                }
                return 0;
            }
        }

        public int Y
        {
            get
            {
                if (yList.Count > 0)
                {
                    return yList[yList.Count - 1];
                }
                return 0;
            }
        }

        public int LastX
        {
            get
            {
                if (xList.Count > 1)
                {
                    return xList[xList.Count - 2];
                }
                return X;
            }
        }

        public int LastY
        {
            get
            {
                if (yList.Count > 1)
                {
                    return yList[yList.Count - 2];
                }
                return Y;
            }
        }

        public int DownX
        {
            get
            {
                if (xList.Count > 0)
                {
                    return xList[0];
                }
                return 0;
            }
        }

        public int DownY
        {
            get
            {
                if (yList.Count > 0)
                {
                    return yList[0];
                }
                return 0;
            }
        }

        #endregion

        #region IMouseEventTracker 成员

        public void Down(int x, int y)
        {
            Clear();
            type = MouseEventTypes.MouseDown;
            xList.Add(x);
            yList.Add(y);
        }

        public void MoveTo(int x, int y)
        {
            type = MouseEventTypes.MouseMove;
            xList.Add(x);
            yList.Add(y);
        }

        public void Up(int x, int y)
        {
            type = MouseEventTypes.MouseUp;
            xList.Add(x);
            yList.Add(y);
        }

        public void Clear()
        {
            xList.Clear();
            yList.Clear();
        }

        public void AddGestureRecognizer(IMouseGestureRecognizer recognizer)
        {

        }

        public void DoRecognize()
        {

        }

        #endregion

        private List<int> xList, yList;
        private MouseEventTypes type;
    }
}
