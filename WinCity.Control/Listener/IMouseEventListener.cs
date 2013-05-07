using System;
using System.Collections.Generic;
using System.Text;
using WinCity.Control.Event;

namespace WinCity.Control.Listener
{
    /// <summary>
    /// 鼠标事件
    /// </summary>
    public interface IMouseEventListener
    {
        /// <summary>
        /// 鼠标按键按下时触发
        /// </summary>
        /// <param name="control">事件触发控件</param>
        /// <param name="e">方法被调用时,参数 e 中的坐标总是相对坐标</param>
        void OnMouseDown(IRControl control, IMouseEvent e);
        /// <summary>
        /// 鼠标按键松开时触发
        /// </summary>
        /// <param name="control">事件触发控件</param>
        /// <param name="e">方法被调用时,参数 e 中的坐标总是相对坐标</param>
        void OnMouseUp(IRControl control, IMouseEvent e);
        /// <summary>
        /// 鼠标移动时触发
        /// </summary>
        /// <param name="control">事件触发控件</param>
        /// <param name="e">方法被调用时,参数 e 中的坐标总是相对坐标</param>
        void OnMouseMove(IRControl control, IMouseEvent e);
        /// <summary>
        /// 鼠标进入控件
        /// </summary>
        /// <param name="control"></param>
        void OnMouseEnter(IRControl control);
        /// <summary>
        /// 鼠标离开控件
        /// </summary>
        /// <param name="control"></param>
        void OnMouseExit(IRControl control);
        /// <summary>
        /// 单击控件时触发
        /// </summary>
        /// <param name="control">事件触发控件</param>
        void OnClick(IRControl control);
    }
}
