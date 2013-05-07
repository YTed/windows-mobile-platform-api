using System;
using System.Collections.Generic;
using System.Text;
using WinCity.Control.Event;

namespace WinCity.Control.Listener
{
    public class ControlListenerNotifier 
        : AbstractListenerNotifier<IControlEventListener, IControlEvent>
    {
        public override void Notify(IControlEvent e)
        {
            switch (e.Type)
            {
                case ControlEventTypes.EnabledChanged:
                    OnEnabledChanged(e);
                    break;
                case ControlEventTypes.VisibleChanged:
                    OnVisibleChanged(e);
                    break;
            }
        }

        private void OnEnabledChanged(IControlEvent e)
        {
            foreach (IControlEventListener listener in listeners)
            {
                listener.EnabledChanged(Control, e);
            }
        }

        private void OnVisibleChanged(IControlEvent e)
        {
            foreach (IControlEventListener listener in listeners)
            {
                listener.VisibleChanged(Control, e);
            }
        }
    }
}
