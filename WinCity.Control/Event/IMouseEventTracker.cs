using System;
using System.Collections.Generic;
using System.Text;
using WinCity.Control.Event;

namespace WinCity.Control.Event
{
    public interface IMouseEventTracker
    {
        void Down(int x, int y);
        void MoveTo(int x, int y);
        void Up(int x, int y);
        void Clear();

        void AddGestureRecognizer(IMouseGestureRecognizer recognizer);
        void DoRecognize();
    }

    public interface IMouseGestureRecognizer
    {
        bool Recognize(IEnumerable<IMouseEvent> eventList);
    }
}
