using System;
using System.Collections.Generic;
using System.Text;
using WinCity.Control.Renderer;
using WinCity.Control.LayoutManager;
using PlatformAPI.GDIPlus;
using System.Drawing;

namespace WinCity.Control.Test
{
    public class RendererTestForm : RForm
    {
        public RendererTestForm()
        {
            RButton button = new RButton();
            button.Text = "Test";
            TopPanel.BindLayoutManager(new FlowLayout());
            TopPanel.AddControl(button, FlowLayoutEnum.Next);
        }
    }
}
