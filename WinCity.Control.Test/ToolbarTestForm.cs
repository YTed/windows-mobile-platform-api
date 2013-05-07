using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using PlatformAPI.Imaging;
using System.IO;
using System.Drawing;
using Microsoft.WindowsMobile.Telephony;


namespace WinCity.Control.Test
{
    public class ToolbarTestForm : Form
    {
        public ToolbarTestForm()
        {
            MinimizeBox = false;

            AlphaImage background = GetResource(@"resource\Toolbar.png");
            
            container = new RPanel(this);
            container.BackgroundImage = GetResource2(@"resource\wallpaper.jpg");

            EasyCommand zoomin = new EasyCommand(GetResource(@"resource\11_a.png"));
            EasyCommand zoomout = new EasyCommand(GetResource(@"resource\12_a.png"));
            EasyCommand car = new CallCommand(GetResource(@"resource\13.png"), "13411162316", true);
            EasyCommand flag = new CallCommand(GetResource(@"resource\14.png"), "13411162316", false);
            EasyCommand dutyon = new EasyCommand(GetResource(@"resource\duty_on.png"));
            EasyCommand dutyoff = new EasyCommand(GetResource(@"resource\duty_off.png"));

            RToolbar toolbar = new RToolbar();
            toolbar.Font = Font;
            toolbar.Background = background;
            toolbar.Bounds = new Rectangle(
                0, Height - background.Height,
                background.Width, background.Height);
            toolbar.AddCommand(zoomin);
            toolbar.AddCommand(zoomout);
            toolbar.AddCommand(car);
            toolbar.AddCommand(flag);
            toolbar.AddCommand(dutyon);
            toolbar.AddCommand(dutyoff);
            
            container.AddControl(toolbar, null);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            container.Update(new Rectangle(0, 0, Width, Height));
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

    class EasyCommand : RBaseCommand
    {
        public EasyCommand(AlphaImage image)
        {
            NormalImage = image;
            Enabled = true;
        }
    }

    class CallCommand : EasyCommand
    {
        public CallCommand(AlphaImage image, string number, bool promt)
            : base(image)
        {
            this.number = number;
            this.phone = new Phone();
            this.promt = promt;
            this.Text = "Call";
        }

        public override void OnClick()
        {
            base.OnClick();
        }

        private bool promt;
        
        private Phone phone;
        
        private string number;
    }
}
