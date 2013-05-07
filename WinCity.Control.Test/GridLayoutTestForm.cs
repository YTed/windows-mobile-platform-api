using System;
using System.Collections.Generic;
using System.Text;
using WinCity.Control.LayoutManager;
using System.Drawing;

namespace WinCity.Control.Test
{
    class GridLayoutTestForm : RForm
    {
        public GridLayoutTestForm()
        {
            Load += new EventHandler(GridLayoutTestForm_Load);
        }

        void GridLayoutTestForm_Load(object sender, EventArgs e)
        {
            int row = 2, column = 3;
            GridLayout layout = new GridLayout(row, column);
            TopPanel.BindLayoutManager(layout);

            for (int i = 0; i < row * column; i++)
            {
                RLabel label = new RLabel();
                label.Font = Font;
                label.ForeColor = Common.RandomColor();
                label.Transparent = false;
                //label.BackColor = Common.RandomColor();
                label.Text = string.Format("{0}00000", i.ToString());
                label.PreferredSize = new Size((i + 1) * 15, (i + 1) * 15);
                
                TopPanel.AddControl(label, GridLayoutEnum.Center);
            }
        }
    }
}
