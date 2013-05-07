using System;
using System.Collections.Generic;
using System.Text;
using WinCity.Control.LayoutManager;
using System.Drawing;

namespace WinCity.Control.Test
{
    class ListViewGridTestForm : RForm
    {
        public ListViewGridTestForm()
        {
            Load += new EventHandler(GridLayoutTestForm_Load);
        }

        void GridLayoutTestForm_Load(object sender, EventArgs e)
        {
            RListView listView = new RListView();
            listView.ItemSize = new Size(60, 60);
            listView.PreferredSize = Size;
            listView.MinimumSize = Size;
            
            FlowLayout layout = new FlowLayout();
            TopPanel.BindLayoutManager(layout);
            TopPanel.AddControl(listView, FlowLayoutEnum.Next);
            
            for (int i = 0; i < 10; i++)
            {
                int row = 2, column = 3;
                RPanel panel = new RPanel();
                panel.BindLayoutManager(new GridLayout(row, column));
                for (int j = 0; j < row * column; j++)
                {
                    RLabel label = new RLabel();
                    label.Font = Font;
                    label.ForeColor = Common.RandomColor();
                    label.Transparent = false;
                    label.BackColor = Common.RandomColor();
                    label.Text = (i * 100 + j).ToString();
                    label.PreferredSize = new Size(50, 50);

                    panel.AddControl(label, GridLayoutEnum.Center);
                }

                listView.AddItem(panel);
            }
        }

    }
}
