using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

using WinCity.Control;
using PlatformAPI.Imaging;
using WinCity.Control.Listener;
using PlatformAPI;
using System.Threading;
using System.ComponentModel;

namespace WinCity.Control.Test
{
    public class TestForm : Form
    {
        public TestForm()
        {
            this.Menu = new MainMenu();
            this.MinimizeBox = false;
            
            container = new RPanel(this);

            string path = Path.GetDirectoryName(
                GetType().Assembly.GetModules()[0].FullyQualifiedName);
            string backimg = @"resource\background.png";
            backimg = Path.Combine(path, backimg);
            Bitmap image = new Bitmap(backimg);
            container.BackgroundImage = image;

            AddButton(@"resource\12_a.png", @"resource\12_a.png", 150, 50, 42, 40);
            AddButton(@"resource\11_a.png", @"resource\11_a.png", 50, 50, 42, 40);
            
            ThreadStart start = new ThreadStart(SystemIdleTimeReset);
            Thread thread = new Thread(start);
            thread.Start();

            Closing += new CancelEventHandler(ExitIdleTimeReset);

        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            container.Update(new Rectangle(0, 0, Width, Height));
        }

        private void AddButton(
            string normalImageFile,
            string pressedImageFile,
            int x, int y,
            int width, int height)
        {
            string path = Path.GetDirectoryName(
                GetType().Assembly.GetModules()[0].FullyQualifiedName);

            normalImageFile = Path.Combine(path, normalImageFile);
            pressedImageFile = Path.Combine(path, pressedImageFile);
            
            ImageButton button = new ImageButton();
            button.NormalImage = AlphaImage.CreateFromFile(normalImageFile);
            button.PressedImage = AlphaImage.CreateFromFile(pressedImageFile);
            button.Bounds = new Rectangle(x, y, width, height);
            button.AddMouseEventListener(new Listener());
            container.AddControl(button, null);
        }

        private void ExitIdleTimeReset(object sender, CancelEventArgs e)
        {
            exit = true;
        }

        private void SystemIdleTimeReset()
        {
            while(!exit)
            {
                Thread.Sleep(10000);
                WMSystem.SystemIdleTimerReset();
            }
        }

        private bool exit = false;

        private RPanel container;
        
        private class Listener : MouseEventAdapter
        {
            public override void OnClick(IRControl control)
            {
                base.OnClick(control);
            }
        }
    }
}
