using System;
using System.Collections.Generic;
using System.Text;

namespace WinCity.Control.LayoutManager
{
    public class GridLayoutException : Exception
    {
        public GridLayoutException()
            : base()
        {
        }

        public GridLayoutException(string msg)
            : base(msg)
        {
        }

        public GridLayoutException(string msg, Exception innerExp)
            : base(msg, innerExp)
        {
        }
    }
}
