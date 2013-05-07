using System;
using System.Collections.Generic;
using System.Text;

namespace WinCity.Control.Event
{
    public interface IControlEvent : IEvent
    {
        ControlEventTypes Type { get;}
    }
    
    public class ControlEvent : IControlEvent
    {
        public ControlEvent(ControlEventTypes type)
        {
            this.type = type;
        }

        #region IControlEvent 成员

        public ControlEventTypes Type
        {
            get { return type; }
        }

        #endregion

        private ControlEventTypes type;
    }

    public enum ControlEventTypes
    {
        VisibleChanged,
        EnabledChanged
    }
}
