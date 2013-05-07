using System;
using System.Collections.Generic;
using System.Text;
using WinCity.Control.LayoutManager;
using System.Drawing;

namespace WinCity.Control.Test
{
    class ListViewTestForm : RForm
    {
        public ListViewTestForm()
        {
            FlowLayout layout = new FlowLayout();
            TopPanel.BindLayoutManager(layout);
            
            RListView listView = new RListView();
            listView.PreferredSize = this.Size;
            listView.MinimumSize = this.Size;

            Color[] colors = new Color[]
            {
                Color.Green, Color.Yellow, Color.Red, Color.Pink
            };
            for (int i = 0; i < 20; i++)
            {
                RLabel label = new RLabel();
                label.Transparent = false;
                label.Text = i.ToString();
                label.Font = Font;
                label.ForeColor = Color.Black;
                label.BackColor = colors[i % colors.Length];
                listView.AddItem(label);
            }
            listView.ItemSize = new Size(Width, 40);

            TopPanel.AddControl(listView, FlowLayoutEnum.Next);
        }
    }
}
