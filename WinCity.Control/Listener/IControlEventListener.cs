using System;
using System.Collections.Generic;
using System.Text;
using WinCity.Control.Event;

namespace WinCity.Control.Listener
{
    public interface IControlEventListener
    {
        void EnabledChanged(IRControl sender, IControlEvent e);
        void VisibleChanged(IRControl sender, IControlEvent e);
    }
}
