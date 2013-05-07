using System;
using System.Collections.Generic;
using System.Text;
using WinCity.Control.LayoutManager;

namespace WinCity.Control.Test
{
    public class RPanelTestForm : RForm
    {
        public RPanelTestForm()
        {
            TopPanel.BindLayoutManager(new FlowLayout());
            
            RLabel label = new RLabel();
            label.Font = Font;
            label.Text = "hello, world";
            TopPanel.AddControl(label, FlowLayoutEnum.Next);
            BackgroundImage = Common.GetResource2(@"resource\wallpaper.jpg");
        }
    }
}
