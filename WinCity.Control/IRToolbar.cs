using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using PlatformAPI.Imaging;

namespace WinCity.Control
{
    public interface IRToolbar : IRControl
    {
        int CommandCount { get;}
        Color ForeColor { get;set;}
        Color BackColor { get;set;}
        bool Transparent { get;set;}
        Font Font { get;set;}
        AlphaImage Background { get;set;}
        Size ItemSize { get;set;}

        void OnCreate(object hook);
        IRCommand GetCommand(int index);
        void AddCommand(IRCommand command);
        void RemoveCommand(IRCommand command);
        void Clear();
    }
}
