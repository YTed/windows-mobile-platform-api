using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using WinCity.Control.LayoutManager;

namespace WinCity.Control
{
    public interface IRContainer
    {
        int Count { get;}
        Rectangle Bounds { get;set;}
        IRContainer Parent { get;set;}
        IRControl this[int index] { get;}
        /// <summary>
        /// 当子控件接收到事件后是否停止传播.true 停止, false 继续.
        /// </summary>
        bool BlockEvent { get;set;}
        
        void BindLayoutManager(ILayoutManager layoutManager);
        void AddControl(IRControl control, object constraint);
        void LayoutOutdate();
        void RemoveControl(IRControl control);
        void Clear();
        void Update(Rectangle rect);
    }
}
