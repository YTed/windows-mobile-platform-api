using System;
using System.Collections.Generic;
using System.Text;

namespace WinCity.Control.Event
{
    /// <summary>
    /// 鼠标事件
    /// </summary>
    public interface IMouseEvent : IEvent
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        MouseEventTypes EventType { get;}
        /// <summary>
        /// 当前鼠标 X 坐标
        /// </summary>
        int X { get;}
        /// <summary>
        /// 当前鼠标 Y 坐标
        /// </summary>
        int Y { get;}
        /// <summary>
        /// 上次鼠标 X 坐标
        /// </summary>
        int LastX { get;}
        /// <summary>
        /// 上次鼠标 Y 坐标
        /// </summary>
        int LastY { get;}
        /// <summary>
        /// 鼠标按下时 X 坐标
        /// </summary>
        int DownX { get;}
        /// <summary>
        /// 鼠标按下时 Y 坐标
        /// </summary>
        int DownY { get;}
    }

    public enum MouseEventTypes
    {
        MouseDown,
        MouseUp,
        MouseMove,
        MouseEnter,
        MouseExit,
        Click

    }
}
