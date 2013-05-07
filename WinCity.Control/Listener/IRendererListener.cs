using System;
using System.Collections.Generic;
using System.Text;
using WinCity.Control.Renderer;

namespace WinCity.Control.Listener
{
    public interface IRendererListener
    {
        void ThemeLoaded(IRendererManager rdrMgr);
    }
}
