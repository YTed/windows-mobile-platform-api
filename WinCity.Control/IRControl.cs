using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using WinCity.Control.Listener;
using WinCity.Control.Event;

namespace WinCity.Control
{
    public interface IRControl : IDisposable
    {
        bool Visible { get;set;}
        bool Enabled { get;set;}
        string Name { get;set;}
        object Tag { get;set;}
        IRContainer Parent { get;set;}
        Rectangle Bounds { get;set;}
        Size PreferredSize { get;set;}
        Size MinimumSize { get;set;}
        Size MaximumSize { get;set;}
        
        void Update();
        void Update(Rectangle rectangle);
        void MarkOutdate();
        void AddMouseEventListener(IMouseEventListener listener);
        void RemoveMouseEventListener(IMouseEventListener listener);
        void AddControlEventListener(IControlEventListener listener);
        void RemoveControlEventListener(IControlEventListener listener);
        void ProcessMouseEvent(IMouseEvent mouseEvent);
        void Paint(Graphics graphics, Rectangle destRect);
        bool HitTest(int x, int y);

        void CalculateSize(Graphics utility);
    }
}
