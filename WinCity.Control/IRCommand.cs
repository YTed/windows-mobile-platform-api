using System;
using System.Collections.Generic;
using System.Text;
using PlatformAPI.Imaging;

namespace WinCity.Control
{
    public interface IRCommand
    {
        bool Enabled { get;set;}
        AlphaImage NormalImage { get;set;}
        AlphaImage DisableImage { get;set;}
        string Text { get;set;}
        string Name { get;set;}

        void OnCreate(object hook);
        void OnClick();
        void OnActive();
        void OnDeactive();
    }
}
