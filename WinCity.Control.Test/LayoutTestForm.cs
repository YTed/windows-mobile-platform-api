using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using WinCity.Control.LayoutManager;
using System.Drawing;
using PlatformAPI.Imaging;
using System.IO;

namespace WinCity.Control.Test
{
    class LayoutTestForm : Form
    {
        public LayoutTestForm()
        {
            container = new RPanel(this);
            container.Bounds = new Rectangle(0, 0, Width, Height);
            container.BackgroundImage = GetResource2(@"resource\wallpaper.jpg");
            
            FlowLayout layout = new FlowLayout();
            layout.Option = FlowLayoutOption.Default;
            container.BindLayoutManager(layout);

            AlphaImage alphaImage = GetResource(@"resource\duty_on.png");
            for (int i = 0; i < 7; i++)
            {
                ImageButton button = new ImageButton();
                button.NormalImage = alphaImage;
                button.PreferredSize = new Size(78, 88);
                button.MinimumSize = button.PreferredSize;
                button.Text = i.ToString();
                //button.Font = Font;
                button.TextAlignment =
                    TextImageAlignment.TextBottom |
                    TextImageAlignment.TextHCenter;
                container.AddControl(button, FlowLayoutEnum.Next);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            container.Update(container.Bounds);
        }


        private AlphaImage GetResource(string rltpath)
        {
            string path = Path.GetDirectoryName(
                GetType().Assembly.GetModules()[0].FullyQualifiedName);
            string abspath = Path.Combine(path, rltpath);
            return AlphaImage.CreateFromFile(abspath);
        }

        private Image GetResource2(string rltpath)
        {
            string path = Path.GetDirectoryName(
                GetType().Assembly.GetModules()[0].FullyQualifiedName);
            string abspath = Path.Combine(path, rltpath);
            return new Bitmap(abspath);
        }

        
        private RPanel container;
    }
}
