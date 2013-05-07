using System;
using System.Collections.Generic;
using System.Text;
using WinCity.Control.Event;

namespace WinCity.Control.Listener
{
    public abstract class AbstractListenerNotifier<TListener, TEvent>
        where TEvent : IEvent
    {
        public AbstractListenerNotifier()
        {
            listeners = new List<TListener>();
        }

        public virtual IRControl Control
        {
            get { return control; }
            set { control = value; }
        }

        public virtual int Count
        {
            get { return listeners.Count; }
        }

        public virtual void Clear()
        {
            listeners.Clear();
        }
        
        public virtual void AddListener(TListener listener)
        {
            if (listener == null)
            {
                throw new ArgumentNullException();
            }
            listeners.Add(listener);
        }

        public virtual void RemoveListener(TListener listener)
        {
            listeners.Remove(listener);
        }
        
        public abstract void Notify(TEvent e);

        protected IRControl control;
        protected List<TListener> listeners;
    }
}
