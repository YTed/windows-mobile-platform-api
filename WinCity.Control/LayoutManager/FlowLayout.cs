using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace WinCity.Control.LayoutManager
{
    /// <summary>
    /// 流布局会计算所有控件的最适大小(IRControl.PerferredSize, P)
    /// 及最小大小(IRControl.IMinimumSize, Min), 计算总体最适大小
    /// S = min((ΣPi) / N , max(Mini)) ,然后令所有控件大小为 S ,
    /// 从左到右, 从上到下布局.
    /// </summary>
    public class FlowLayout : ILayoutManager
    {
        public FlowLayout()
        {
            hash = new Dictionary<IRControl, FlowLayoutEnum>();
        }

        public FlowLayoutOption Option
        {
            get { return option; }
            set { option = value; }
        }
        
        #region ILayoutManager 成员

        public void AddLayoutComponent(object constraint, IRControl control)
        {
            // TODO: add other layout constraints
            // here we assume all constraint are FlowLayoutEnum.Next
            hash.Add(control, FlowLayoutEnum.Next);
        }

        public void RemoveLayoutComponent(IRControl control)
        {
            if (control != null)
            {
                hash.Remove(control);
            }
        }

        public void Clear()
        {
            hash.Clear();
        }

        public void Layout(IRContainer container)
        {
            if (container.Count == 0)
            {
                return;
            }
            int start = 0, count = 0;
            int yoffset = 0;
            while (start + count < container.Count)
            {
                start += count;
                Size fitSize = CalculateSize(container, ref start, out count);
                int boundWidth = container.Bounds.Width;
                int boundHeight = container.Bounds.Height;
                if (boundWidth > fitSize.Width && boundWidth != 0)
                {
                    int gap = 0;
                    if (Option != FlowLayoutOption.Spring && fitSize.Width != 0)
                    {
                        int hcount = boundWidth / fitSize.Width;
                        if (Option == FlowLayoutOption.Default)
                        {
                            hcount = Math.Min(hcount, container.Count);
                        }
                        int rest = boundWidth - hcount * fitSize.Width;
                        gap = rest / hcount;
                    }
                    fitSize.Width = fitSize.Width + gap;
                }
                int x = container.Bounds.X, y = yoffset + container.Bounds.Y;

                Rectangle tmpRect = new Rectangle();
                for (int i = 0; i < count; i++)
                {
                    IRControl control = container[start + i];
                    if (control.Visible)
                    {
                        tmpRect.X = x;
                        tmpRect.Y = y;
                        tmpRect.Width = fitSize.Width;
                        tmpRect.Height = fitSize.Height;
                        control.Bounds = tmpRect;
                        x += fitSize.Width;
                    }
                }
                yoffset += fitSize.Height;
            }
        }

        #endregion

        private Size CalculateSize(IRContainer container, ref int start, out int count)
        {
            int maxMinWidth = int.MinValue,
                maxMinHeight = int.MinValue;
            int sumPreWidth = 0, sumPreHeight = 0;
            int totalWidth = container.Bounds.Width;
            count = 0;
            for (int i = start; i < container.Count; i++)
            {
                IRControl control = container[i];
                Size minimumSize = control.MinimumSize;
                Size preferredSize = control.PreferredSize;
                if (sumPreWidth + preferredSize.Width <= totalWidth)
                {
                    sumPreWidth += preferredSize.Width;
                    sumPreHeight += preferredSize.Height;
                    maxMinWidth = Math.Max(minimumSize.Width, maxMinWidth);
                    maxMinHeight = Math.Max(minimumSize.Height, maxMinHeight);
                    count++;
                }
                else
                {
                    break;
                }
            }
            sumPreWidth /= count;
            sumPreHeight /= count;
            int width = Math.Min(sumPreWidth, maxMinWidth);
            int height = Math.Max(sumPreHeight, maxMinHeight);
            return new Size(width, height);
        }

        private Dictionary<IRControl, FlowLayoutEnum> hash;

        private FlowLayoutOption option = FlowLayoutOption.Default;
    }

    public enum FlowLayoutEnum
    {
        Next
    }

    public enum FlowLayoutOption
    {
        /// <summary>
        /// 默认处理方式,即平均分配空间
        /// </summary>
        Default,
        /// <summary>
        /// 尾部填充,以计算出的控件大小填充尾部空间
        /// </summary>
        Fill,
        /// <summary>
        /// 尾部伸展,压缩前面控件大小
        /// </summary>
        Spring
    }
    
}
