using System;
using System.Collections.Generic;
using System.Text;

namespace WinCity.Control.LayoutManager
{
    public interface ILayoutManager
    {
        /// <summary>
        /// 添加控件
        /// </summary>
        /// <param name="constraint"></param>
        /// <param name="control"></param>
        void AddLayoutComponent(object constraint, IRControl control);
        
        /// <summary>
        /// 移除控件
        /// </summary>
        /// <param name="control"></param>
        void RemoveLayoutComponent(IRControl control);
        
        /// <summary>
        /// 清除所有控件
        /// </summary>
        void Clear();
        
        /// <summary>
        /// 布局后控件的 Bounds 属性的坐标为相对坐标.
        /// </summary>
        /// <param name="container"></param>
        void Layout(IRContainer container);
    }
}
