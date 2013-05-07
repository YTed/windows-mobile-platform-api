using System;
using System.Collections.Generic;
using System.Text;
using WinCity.Control.Listener;
using WinCity.Control.Event;
using System.Drawing;

namespace WinCity.Control
{
    public abstract class RControl : IRControl
    {
        protected RControl()
        {
            meNotifier = new MouseListenerNotifier();
            ceNotifier = new ControlListenerNotifier();
            ceNotifier.Control = meNotifier.Control = this;
        }

        #region IRControl 成员

        public virtual bool Visible
        {
            get { return visible; }
            set
            {
                bool old = visible;
                visible = value;
                if (old ^ visible)
                {
                    IControlEvent ce = new ControlEvent(ControlEventTypes.VisibleChanged);
                    ceNotifier.Notify(ce);
                }
            }
        }

        public virtual bool Enabled
        {
            get { return enabled; }
            set
            {
                bool old = enabled;
                enabled = value;
                if (old ^ enabled)
                {
                    IControlEvent ce = new ControlEvent(ControlEventTypes.EnabledChanged);
                    ceNotifier.Notify(ce);
                }
            }
        }

        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual object Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        public virtual Rectangle Bounds
        {
            get
            {
                return bounds;
            }
            set
            {
                bounds = value;
            }
        }

        public virtual IRContainer Parent
        {
            get
            {
                return parent;
            }
            set
            {
                if (parent != this)
                {
                    parent = value;
                }
            }
        }
        
        public virtual void Update(Rectangle rectangle)
        {
            if (parent != null)
            {
                parent.Update(rectangle);
            }
        }

        public virtual void Update()
        {
            if (parent != null)
            {
                parent.Update(Bounds);
            }
        }

        public virtual void MarkOutdate()
        {
            isOutdate = true;
        }
        
        public void AddControlEventListener(IControlEventListener listener)
        {
            if (listener != null)
            {
                ceNotifier.AddListener(listener);
            }
        }

        public void RemoveControlEventListener(IControlEventListener listener)
        {
            ceNotifier.RemoveListener(listener);
        }

        public virtual void AddMouseEventListener(IMouseEventListener listener)
        {
            meNotifier.AddListener(listener);
        }

        public virtual void RemoveMouseEventListener(IMouseEventListener listener)
        {
            meNotifier.RemoveListener(listener);
        }

        public virtual void ProcessMouseEvent(IMouseEvent mouseEvent)
        {
            meNotifier.Notify(mouseEvent);
        }
        
        public virtual bool HitTest(int x, int y)
        {
            return bounds.Contains(x, y);
        }

        public virtual void Paint(Graphics graphics, Rectangle destRect) 
        {
            if (Visible)
            {
                PaintBackground(graphics, destRect);
                if (Enabled)
                {
                    PaintEnabled(graphics, destRect);
                }
                else
                {
                    PaintDisabled(graphics, destRect);
                }
            }
        }

        public virtual Size PreferredSize
        {
            get
            {
                return preferredSize;
            }
            set
            {
                preferredSize = value;
            }
        }

        public virtual Size MinimumSize
        {
            get
            {
                return minimumSize;
            }
            set
            {
                minimumSize = value;
            }
        }

        public virtual Size MaximumSize
        {
            get
            {
                return maximumSize;
            }
            set
            {
                maximumSize = value;
            }
        }

        public virtual void CalculateSize(Graphics utility)
        {
        }

        #endregion

        #region IDisposable 成员

        public abstract void Dispose();

        #endregion

        protected virtual void PaintBackground(Graphics graphics, Rectangle destRect)
        {

        }
        
        protected virtual void PaintEnabled(Graphics graphics, Rectangle destRect)
        {
            
        }

        protected virtual void PaintDisabled(Graphics graphics, Rectangle destRect)
        {

        }

        protected Size preferredSize, minimumSize, maximumSize;

        protected bool visible = true, enabled = true, isOutdate = true;

        protected ControlListenerNotifier ceNotifier;

        protected MouseListenerNotifier meNotifier;

        protected IRContainer parent;

        protected Rectangle bounds;

        protected string name;
        
        protected object tag;
    }
}
