using System;
using System.Collections.Generic;
using System.Text;
using WinCity.Control.Event;

namespace WinCity.Control.Listener
{
    public abstract class ControlEventAdapter : IControlEventListener
    {
        #region IControlEventListener 成员

        public virtual void EnabledChanged(IRControl sender, IControlEvent e)
        {

        }

        public virtual void VisibleChanged(IRControl sender, IControlEvent e)
        {

        }

        #endregion
    }
}
