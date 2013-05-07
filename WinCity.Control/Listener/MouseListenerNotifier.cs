using System;
using System.Collections.Generic;
using System.Text;
using WinCity.Control.Event;

namespace WinCity.Control.Listener
{
    public class MouseListenerNotifier
        : AbstractListenerNotifier<IMouseEventListener, IMouseEvent>
    {
        public virtual void OnMouseEnter(IMouseEvent me)
        {
            foreach (IMouseEventListener listener in listeners)
            {
                listener.OnMouseEnter(control);
            }
        }

        public virtual void OnMouseExit(IMouseEvent me)
        {
            foreach (IMouseEventListener listener in listeners)
            {
                listener.OnMouseExit(control);
            }
        }

        public virtual void OnMouseDown(IMouseEvent me)
        {
            foreach (IMouseEventListener listener in listeners)
            {
                listener.OnMouseDown(control, me);
            }
        }

        public virtual void OnMouseUp(IMouseEvent me)
        {
            foreach (IMouseEventListener listener in listeners)
            {
                listener.OnMouseUp(control, me);
            }
        }

        public virtual void OnMouseMove(IMouseEvent me)
        {
            foreach (IMouseEventListener listener in listeners)
            {
                listener.OnMouseMove(control, me);
            }
        }

        public virtual void OnClick(IMouseEvent me)
        {
            foreach (IMouseEventListener listener in listeners)
            {
                listener.OnClick(control);
            }
        }

        public override void Notify(IMouseEvent mouseEvent)
        {
            switch (mouseEvent.EventType)
            {
                case MouseEventTypes.MouseDown:
                    OnMouseDown(mouseEvent);
                    break;
                case MouseEventTypes.MouseEnter:
                    OnMouseEnter(mouseEvent);
                    break;
                case MouseEventTypes.MouseExit:
                    OnMouseExit(mouseEvent);
                    break;
                case MouseEventTypes.MouseMove:
                    OnMouseMove(mouseEvent);
                    break;
                case MouseEventTypes.MouseUp:
                    OnMouseUp(mouseEvent);
                    break;
                case MouseEventTypes.Click:
                    OnClick(mouseEvent);
                    break;
            }
        }
    }
}
