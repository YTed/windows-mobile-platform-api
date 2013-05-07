using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace WinCity.Control
{
    public class RForm : Form
    {
        public RForm()
        {
            container = new RPanel(this);
            container.Bounds = new Rectangle(0, 0, Width, Height);
        }

        public Image BackgroundImage
        {
            get { return container.BackgroundImage; }
            set { container.BackgroundImage = value; }
        }

        public PictureSizeMode BackgroundImageMode
        {
            get { return container.BackgroundImageMode; }
            set { container.BackgroundImageMode = value; }
        }

        public IRContainer TopPanel
        {
            get { return container; }
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            container.Update(container.Bounds);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            container.Dispose();
        }

        private RPanel container;
    }
}
