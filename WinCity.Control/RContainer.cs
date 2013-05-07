using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using WinCity.Control.Event;
using WinCity.Control.LayoutManager;
using WinCity.Control.Listener;

namespace WinCity.Control
{
    public class RContainer : RControl, IRContainer, IEnumerable<IRControl>
    {
        public RContainer()
        {
            controlList = new List<IRControl>();
            paintCallback = new RefreshCallback(Update);
            tracker = new MouseEventTracker();
        }

        public RContainer(System.Windows.Forms.Control peer)
            : this()
        {
            if (peer == null)
            {
                throw new ArgumentNullException();
            }
            this.peer = peer;
            this.peer.MouseDown += new MouseEventHandler(peer_MouseDown);
            this.peer.MouseUp += new MouseEventHandler(peer_MouseUp);
            this.peer.MouseMove += new MouseEventHandler(peer_MouseMove);
        }

        #region IRContainer 成员

        public virtual IRControl this[int index]
        {
            get { return controlList[index]; }
        }

        public virtual int Count
        {
            get { return controlList.Count; }
        }

        public virtual bool BlockEvent
        {
            get { return blockEvent; }
            set { blockEvent = value; }
        }

        public virtual void BindLayoutManager(ILayoutManager layoutManager)
        {
            this.layoutManager = layoutManager;
        }

        public virtual void AddControl(IRControl control, object constraint)
        {
            control.Parent = this;
            controlList.Add(control);
            if (layoutManager != null)
            {
                layoutManager.AddLayoutComponent(constraint, control);
            }
            hasLayout = false;
        }

        public virtual void RemoveControl(IRControl control)
        {
            controlList.Remove(control);
            if (layoutManager != null)
            {
                layoutManager.RemoveLayoutComponent(control);
            }
            hasLayout = false;
        }

        public virtual void LayoutOutdate()
        {
            hasLayout = false;
        }
        
        public virtual void Clear()
        {
            controlList.Clear();
            if (layoutManager != null)
            {
                layoutManager.Clear();
            }
        }

        public override void Update(Rectangle rect)
        {
            if (parent != null)
            {
                base.Update(rect);
            }
            else if (peer != null)
            {
                if (peer.InvokeRequired)
                {
                    peer.Invoke(paintCallback, rect);
                }
                else
                {
                    if (buffer == null)
                    {
                        buffer = new Bitmap(peer.Width, peer.Height);
                    }
                    using (Graphics graphicsBuffer = Graphics.FromImage(buffer))
                    {
                        Region orgRegion = graphicsBuffer.Clip;
                        Region newRegion = new Region(rect);
                        graphicsBuffer.Clip = newRegion;

                        Paint(graphicsBuffer, rect);

                        graphicsBuffer.Clip = orgRegion;
                    }
                    using (Graphics graphics = peer.CreateGraphics())
                    {
                        graphics.DrawImage(buffer, 0, 0);
                    }
                }
            }
        }

        #endregion

        #region IEnumerable<IRControl> 成员

        public IEnumerator<IRControl> GetEnumerator()
        {
            return controlList.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (controlList as IEnumerable).GetEnumerator();
        }

        #endregion

        public override void ProcessMouseEvent(IMouseEvent mouseEvent)
        {
            switch (mouseEvent.EventType)
            {
                case MouseEventTypes.MouseDown:
                    PerformMouseDown(mouseEvent.X, mouseEvent.Y);
                    break;
                case MouseEventTypes.MouseMove:
                    PerformMouseMove(mouseEvent.X, mouseEvent.Y);
                    break;
                case MouseEventTypes.MouseUp:
                    PerformMouseUp(mouseEvent.X, mouseEvent.Y);
                    break;
            }
            if (!BlockEvent || (lastHitControl == null))
            {
                base.ProcessMouseEvent(mouseEvent);
            }
        }
        
        public override void Dispose()
        {
            foreach (IRControl control in controlList)
            {
                control.Dispose();
            }
            if (buffer != null)
            {
                buffer.Dispose();
            }
        }

        public override void Paint(Graphics graphics, Rectangle destRect)
        {
            base.Paint(graphics, destRect);

            if (!hasLayout)
            {
                LayoutControl(graphics);
                hasLayout = true;
            }
            foreach (IRControl control in controlList)
            {
                if (control.Bounds.IntersectsWith(destRect))
                {
                    PaintSubControls(graphics, control);
                }
            }
        }

        protected virtual void PaintSubControls(Graphics graphics, IRControl control)
        {
            Region region = graphics.Clip;
            try
            {
                graphics.Clip = new Region(control.Bounds);

                control.Paint(graphics, control.Bounds);
            }
            finally
            {
                graphics.Clip = region;
            }
        }

        protected virtual void LayoutControl(Graphics utility)
        {
            for (int i = 0; i < Count; i++)
            {
                IRControl control = this[i];
                control.CalculateSize(utility);
            }
            if (layoutManager != null)
            {
                layoutManager.Layout(this);
            }
        }

        protected void PerformMouseDown(int x, int y)
        {
            tracker.Down(x, y);
            EventAssigner.OnMouseDown(controlList, tracker, ref lastHitControl);
        }

        protected void PerformMouseUp(int x, int y)
        {
            tracker.Up(x, y);
            EventAssigner.OnMouseUp(controlList, tracker, allowPreformClick, ref lastHitControl);
        }

        protected void PerformMouseMove(int x, int y)
        {
            tracker.MoveTo(x, y);
            EventAssigner.OnMouseMove(controlList, tracker, ref lastHitControl);
        }

        private void peer_MouseMove(object sender, MouseEventArgs e)
        {
            PerformMouseMove(e.X, e.Y);
        }

        private void peer_MouseUp(object sender, MouseEventArgs e)
        {
            PerformMouseUp(e.X, e.Y);
        }

        private void peer_MouseDown(object sender, MouseEventArgs e)
        {
            PerformMouseDown(e.X, e.Y);
        }

        
        protected ILayoutManager layoutManager;

        protected Image buffer;

        protected bool allowPreformClick = true, blockEvent = true;

        protected MouseEventTracker tracker;

        protected IRControl lastHitControl;

        private System.Windows.Forms.Control peer;

        private List<IRControl> controlList;

        protected bool hasLayout = false;

        private RefreshCallback paintCallback;

        private delegate void RefreshCallback(Rectangle rectangle);
    }
}