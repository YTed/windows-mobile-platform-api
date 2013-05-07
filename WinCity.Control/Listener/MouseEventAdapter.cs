using System;
using System.Collections.Generic;
using System.Text;
using WinCity.Control.Event;

namespace WinCity.Control.Listener
{
    public class MouseEventAdapter : IMouseEventListener
    {
        #region IMouseEventListener 成员

        public virtual void OnMouseDown(IRControl control, IMouseEvent e)
        {

        }

        public virtual void OnMouseUp(IRControl control, IMouseEvent e)
        {

        }

        public virtual void OnMouseMove(IRControl control, IMouseEvent e)
        {

        }

        public virtual void OnMouseEnter(IRControl control)
        {

        }

        public virtual void OnMouseExit(IRControl control)
        {

        }

        public virtual void OnClick(IRControl control)
        {

        }

        #endregion
    }
}
