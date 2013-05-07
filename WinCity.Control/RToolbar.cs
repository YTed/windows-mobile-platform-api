using System;
using System.Collections.Generic;
using System.Text;
using PlatformAPI.Imaging;
using System.Drawing;
using WinCity.Control.Listener;
using WinCity.Control.Event;
using WinCity.Control.LayoutManager;
using WinCity.Control.Renderer;

namespace WinCity.Control
{
    public class RToolbar : RContainer, IRToolbar
    {
        static RToolbar()
        {
            IRenderer renderer = new ToolbarRenderer();
            RendererManager.Instance.AddRenderer(ToolbarRenderer.NAME, renderer);
        }
        
        public RToolbar()
        {
            BlockEvent = false;
            IMouseEventListener uiListener = new ToolbarUIListener(this);
            AddMouseEventListener(uiListener);
            
            IRendererManager rdrMgr = RendererManager.Instance;
            IRenderer rdr = rdrMgr.GetRenderer(ToolbarRenderer.NAME);
            font = rdr.GetFont(ToolbarRenderer.FONT);
            padding = rdr.GetNumber(ToolbarRenderer.ITEM_PADDING);
            backColor = rdr.GetColor(ToolbarRenderer.BACK_COLOR);
            foreColor = rdr.GetColor(ToolbarRenderer.FORE_COLOR);
            scrollBackColor = rdr.GetColor(ToolbarRenderer.SCROLL_BACK_COLOR);
            scrollForeColor = rdr.GetColor(ToolbarRenderer.SCROLL_FORE_COLOR);
            scrollWidth = rdr.GetNumber(ToolbarRenderer.SCROLL_WIDTH);
            scrollVisible = rdr.GetBoolean(ToolbarRenderer.SCROLL_VISIBLE);
            int itemWidth = rdr.GetNumber(ToolbarRenderer.ITEM_WIDTH);
            int itemHeight = rdr.GetNumber(ToolbarRenderer.ITEM_HEIGHT);
            if (itemWidth > 0 && itemHeight > 0)
            {
                ItemSize = new Size(itemWidth, itemHeight);
            }
            bool showText = rdr.GetBoolean(ToolbarRenderer.SHOW_TEXT);
            if (!showText)
            {
                itemTextAlign |= TextImageAlignment.ImageOnly;
            }
        }

        public override Rectangle Bounds
        {
            get
            {
                if (bounds.Width == 0 && Background != null)
                {
                    bounds.Width = Background.Width;
                    bounds.Height = Background.Height;
                }
                return bounds;
            }
            set
            {
                base.Bounds = value;
            }
        }
        
        #region IDisposable 成员

        public override void Dispose()
        {
            if (background != null)
            {
                background.Dispose();
            }
        }

        #endregion

        #region IRToolbar 成员

        public Color ForeColor
        {
            get { return foreColor; }
            set { foreColor = value; }
        }

        public Color BackColor
        {
            get { return backColor; }
            set { backColor = value; }
        }

        public bool Transparent
        {
            get { return transparent; }
            set { transparent = value; }
        }

        public Font Font
        {
            get { return font; }
            set { font = value; }
        }
        
        public AlphaImage Background
        {
            get { return background; }
            set { background = value; }
        }

        public IRCommand GetCommand(int index)
        {
            return (this[index] as CommandUI).Command; 
        }

        public Size ItemSize
        {
            get { return itemSize; }
            set { itemSize = value; }
        }
        
        public int CommandCount
        {
            get { return Count; }
        }

        public void AddCommand(IRCommand command)
        {
            if (command != null)
            {
                CommandUI ui = new CommandUI(command);
                ui.Tag = CommandCount;
                ui.Font = font;
                ui.ForeColor = foreColor;
                ui.TextAlignment = itemTextAlign;
                AddControl(ui, null);
            }
        }

        public void RemoveCommand(IRCommand command)
        {
            if (command != null)
            {
                int index = 0;
                for(int i = 0 ; i<CommandCount; i++)
                {
                    IRControl control = this[i];
                    CommandUI ui = control as CommandUI;
                    if (ui.Command.Equals(command))
                    {
                        RemoveControl(ui);
                        index = (int)ui.Tag;
                        break;
                    }
                }
                for (int i = index - 1; i < CommandCount; i++)
                {
                    this[i].Tag = i;
                }
            }
        }

        public void OnCreate(object hook)
        {
            for (int i = 0; i < Count; i++)
            {
                IRControl control = this[i];
                CommandUI ui = control as CommandUI;
                IRCommand command = ui.Command;
                command.OnCreate(hook);
            }
        }

        #endregion

        public override void BindLayoutManager(ILayoutManager layoutManager)
        {

        }

        public override void MarkOutdate()
        {
            base.MarkOutdate();
            for (int i = 0; i < Count; i++)
            {
                IRControl control = this[i];
                CommandUI ui = control as CommandUI;
                ui.BufferOutdate();
            }
        }

        protected override void PaintBackground(Graphics graphics, Rectangle destRect)
        {
            base.PaintBackground(graphics, destRect);

            if (background != null)
            {
                if (isOutdate)
                {
                    background.PrepareBuffer(graphics, destRect.X, destRect.Y);
                    isOutdate = false;
                }
                background.DrawBuffer(graphics, destRect.X, destRect.Y);
            }
        }

        protected override void PaintSubControls(Graphics graphics, IRControl control)
        {
            base.PaintSubControls(graphics, control);
        }
        
        protected override void PaintEnabled(Graphics graphics, Rectangle destRect)
        {
            base.PaintEnabled(graphics, destRect);

            Rectangle uiRect = new Rectangle();
            for(int i = 0 ; i<Count ;i++)
            {
                IRControl ui = this[i];
                Layout(ui, destRect, ref uiRect);
                uiRect.X += destRect.X;
                uiRect.Y += destRect.Y;
                ui.Bounds = uiRect;
            }

            maxWidth = uiRect.Right + padding - xoffset;

            if (scrollVisible && maxWidth > destRect.Width)
            {
                using (Pen pen = new Pen(scrollBackColor))
                {
                    int xbegin = destRect.Left, xend = destRect.Right;
                    pen.Width = scrollWidth;
                    graphics.DrawLine(pen, xbegin, destRect.Bottom, xend, destRect.Bottom);
                    pen.Color = scrollForeColor;

                    xbegin = -xoffset;
                    xend = -xoffset + destRect.Width;
                    xbegin = xbegin * destRect.Width / maxWidth;
                    xend = xend * destRect.Width / maxWidth;
                    graphics.DrawLine(pen, xbegin, destRect.Bottom, xend, destRect.Bottom);
                }
            }
        }

        private void Layout(IRControl ui, Rectangle destRect, ref Rectangle uiBounds)
        {
            int index = (int)ui.Tag;
            uiBounds.X = (ItemSize.Width + padding) * index + padding + xoffset + destRect.X;
            uiBounds.Y = (Bounds.Height - itemSize.Height + (padding >> 1)) >> 1;
            uiBounds.Width = ItemSize.Width;
            uiBounds.Height = ItemSize.Height;
        }

        private void BeginShift()
        {
            xoffset = stableXOffset;
        }
        
        private void Shift(int deltaX, int deltaY)
        {
            if (maxWidth > Bounds.Width &&
                Math.Abs(deltaX) > (ItemSize.Width >> 2))
            {
                xoffset = stableXOffset + deltaX;
                if (xoffset > 0)
                {
                    xoffset = 0;
                }
                else if (maxWidth + xoffset < Bounds.Width)
                {
                    xoffset = Bounds.Width - maxWidth;
                }

                for (int i = 0; i < Count; i++)
                {
                    IRControl control = this[i];
                    CommandUI ui = control as CommandUI;
                    ui.BufferOutdate();
                }
                Update();
                allowPreformClick = false;
            }
        }

        private void EndShift()
        {
            stableXOffset = xoffset;
            allowPreformClick = true;
        }
        
        private AlphaImage background;
        
        private Font font;

        private Size itemSize = new Size(50, 50);

        private int padding = 12;

        private int xoffset, stableXOffset;

        private int maxWidth = 0;

        private int scrollWidth = 4;

        private bool transparent = false,
            scrollVisible = true;

        private TextImageAlignment itemTextAlign = TextImageAlignment.Default;

        private Color
            foreColor = Color.Black,
            backColor = Color.White,
            scrollBackColor = Color.Black,
            scrollForeColor = Color.Black;
        
        private class CommandUI : ImageButton
        {
            public CommandUI(IRCommand command)
            {
                this.command = command;

                Text = command.Text;
                Name = command.Name;
                NormalImage = command.NormalImage;
                PressedImage = command.NormalImage;
                DisableImage = command.DisableImage;
                Enabled = command.Enabled;
                TextAlignment = TextImageAlignment.TextBottom |
                    TextImageAlignment.TextHCenter;
            }
            
            public IRCommand Command
            {
                get { return command; }
                set { command = value; }
            }

            public override bool Enabled
            {
                get
                {
                    return command.Enabled;
                }
                set
                {
                    //command.Enabled = value;
                }
            }

            public override void Paint(Graphics graphics, Rectangle destRect)
            {
                if (destRect.Width > NormalImage.Width)
                {
                    destRect.X = destRect.X - ((destRect.Width - NormalImage.Width) >> 1);
                }
                if (destRect.Height > NormalImage.Height)
                {
                    destRect.Y = destRect.Y + ((destRect.Height - NormalImage.Height) >> 1);
                }
                base.Paint(graphics, destRect);
            }

            public override void PerformClick()
            {
                if (Command != null)
                {
                    Command.OnClick();
                }
            }

            private IRCommand command;
        }

        private class ToolbarUIListener : MouseEventAdapter
        {
            public ToolbarUIListener(RToolbar toolbar)
            {
                this.toolbar = toolbar;
            }
            
            public override void OnMouseDown(IRControl control, IMouseEvent me)
            {
                toolbar.BeginShift();
            }

            public override void OnMouseUp(IRControl control, IMouseEvent me)
            {
                toolbar.EndShift();
            }

            public override void OnMouseMove(IRControl control, IMouseEvent me)
            {
                int deltaX = me.X - me.DownX,
                    deltaY = me.Y - me.DownY;
                toolbar.Shift(deltaX, deltaY);
            }

            private RToolbar toolbar;
        }

    }
}
