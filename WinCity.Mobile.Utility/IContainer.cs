using System;
using System.Collections.Generic;
using System.Text;

namespace WinCity.Mobile.Utility.SpatialIndex
{
    public interface IContainer<T> : IEnumerable<T>
    {
        void Add(T item);
        bool Remove(T item);
        void Clear();
    }
}
