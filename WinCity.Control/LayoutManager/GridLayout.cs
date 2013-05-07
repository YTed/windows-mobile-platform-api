using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace WinCity.Control.LayoutManager
{
    public class GridLayout : ILayoutManager
    {
        public GridLayout(int rows, int columns)
        {
            if (rows <= 0 || columns <= 0)
            {
                throw new ArgumentException("rows or columns should larger than zero");
            }
            this.rows = rows;
            this.columns = columns;
            this.widthPref = new int[columns];
            this.heightPref = new int[rows]; 
            this.xs = new int[columns];
            this.ys = new int[rows];
            this.components = new GridLayoutComponent[rows * columns];
        }
        
        #region ILayoutManager 成员

        public void AddLayoutComponent(object constraint, IRControl control)
        {
            if (control == null)
            {
                throw new ArgumentNullException();
            }
            if (count > Capacity)
            {
                throw new GridLayoutException("容器已满");
            }
            GridLayoutComponent glc = new GridLayoutComponent();
            glc.Control = control;
            glc.Index = count;
            if (constraint is GridLayoutEnum)
            {
                glc.Layout = (GridLayoutEnum)constraint;
                if (glc.Layout == GridLayoutEnum.None)
                {
                    throw new GridLayoutException("Unsupported Layout : GridLayoutEnum.None");
                }
            }
            else
            {
                glc.Layout = GridLayoutEnum.Default;
            }
            components[count] = glc;
            count++;
        }

        public void RemoveLayoutComponent(IRControl control)
        {
            if (control == null)
            {
                return;
            }
            for (int i = 0; i < count; i++)
            {
                if (components[i] != null &&
                    components[i].Control == control)
                {
                    components[i] = null;
                }
            }
        }

        public void Clear()
        {
            Fill(components, null);
            count = 0;
        }

        public void Layout(IRContainer container)
        {
            CalculateSize(container);
            
            // so for control of index = r * rows + c, it's rectangle is
            //  (x=xs[r], y=ys[c], width=widthPref[r], height=heightPref[c]).
            foreach (GridLayoutComponent comp in components)
            {
                Size size = comp.Control.PreferredSize;
                int index = comp.Index,
                    rowIndex = index / columns,
                    colIndex = index % columns;
                int x = xs[colIndex],
                    y = ys[rowIndex],
                    width = widthPref[colIndex],
                    height = heightPref[rowIndex];
                comp.Control.Bounds = new Rectangle(x, y, width, height);
            }
        }

        #endregion

        private int Capacity
        {
            get
            {
                return rows * columns - 1;
            }
        }

        private void Fill<T>(T[] array, T item)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = item;
            }
        }

        private void CalculateSize(IRContainer container)
        {
            Fill(widthPref, 0);
            Fill(heightPref, 0);
            Fill(xs, 0);
            Fill(ys, 0);

            foreach (GridLayoutComponent comp in components)
            {
                int rowIdx = comp.Index / columns;
                int colIdx = comp.Index % columns;
                Size size = comp.Control.PreferredSize;
                heightPref[rowIdx] = Math.Max(size.Height, heightPref[rowIdx]);
                widthPref[colIdx] = Math.Max(size.Width, widthPref[colIdx]);
            }

            for (int i = 1; i < columns; i++)
            {
                xs[i] = xs[i - 1] + widthPref[i - 1];
            }
            for (int i = 1; i < rows; i++)
            {
                ys[i] = ys[i - 1] + heightPref[i - 1];
            }

            // zoom , not implemented.
            Rectangle destRect = container.Bounds;
            int totalWidth = xs[columns - 1] + widthPref[columns - 1],
                totalHeight = ys[rows - 1] + heightPref[rows - 1],
                deltaWidth = destRect.Width - totalWidth,
                deltaHeight = destRect.Height - totalHeight;

            if (deltaWidth > 0)
            {

            }
            else if(deltaWidth < 0)
            {

            }

            if (deltaHeight > 0)
            {

            }
            else if(deltaHeight < 0)
            {

            }
        }

        private GridLayoutComponent[] components;

        private int count, rows, columns;

        int[] heightPref;
        int[] widthPref;
        int[] xs;
        int[] ys;

        private class GridLayoutComponent
        {
            public IRControl Control;

            public int Index;

            public GridLayoutEnum Layout;
        }
    }

    [Flags]
    public enum GridLayoutEnum
    {
        None = 0x00,
        Default = 0x11,
        Left = 0x01,
        Right = 0x02,
        Top = 0x10,
        Bottom = 0x20,
        HCenter = 0x03,
        VCenter = 0x30,
        Center = 0x33
    }
}
