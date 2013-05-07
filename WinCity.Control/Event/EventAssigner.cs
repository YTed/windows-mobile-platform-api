using System;
using System.Collections.Generic;
using System.Text;

namespace WinCity.Control.Event
{
    public class EventAssigner
    {
        /// <summary>
        /// 分配事件
        /// </summary>
        /// <param name="controls">控件列表</param>
        /// <param name="sourceEvent">源事件</param>
        /// <param name="performClick">如果事件是 MouseUp ,是否要触发对应控件的 Click 事件</param>
        /// <param name="lastHitControl">上次被击中的控件</param>
        public static void Assign(
            IEnumerable<IRControl> controls,
            IMouseEvent sourceEvent,
            bool performClick,
            ref IRControl lastHitControl)
        {
            switch (sourceEvent.EventType)
            {
                case MouseEventTypes.MouseDown:
                    OnMouseDown(controls, sourceEvent, ref lastHitControl);
                    break;
                case MouseEventTypes.MouseMove:
                    OnMouseMove(controls, sourceEvent, ref lastHitControl);
                    break;
                case MouseEventTypes.MouseUp:
                    OnMouseUp(controls, sourceEvent, performClick, ref lastHitControl);
                    break;
            }
        }
        /// <summary>
        /// 分配事件
        /// </summary>
        /// <param name="controls">控件列表</param>
        /// <param name="sourceEvent">源事件</param>
        /// <param name="lastHitControl">上次被击中的控件</param>
        public static void OnMouseDown(
            IEnumerable<IRControl> controls,
            IMouseEvent sourceEvent,
            ref IRControl lastHitControl)
        {
            lastHitControl = null;
            foreach (IRControl control in controls)
            {
                if (control.Visible &&
                    control.Enabled &&
                    control.HitTest(sourceEvent.X, sourceEvent.Y))
                {
                    lastHitControl = control;
                    control.ProcessMouseEvent(sourceEvent);
                    break;
                }
            }
        }

        /// <summary>
        /// 分配事件
        /// </summary>
        /// <param name="controls">控件列表</param>
        /// <param name="sourceEvent">源事件</param>
        /// <param name="performClick">如果事件是 MouseUp ,是否要触发对应控件的 Click 事件</param>
        /// <param name="lastHitControl">上次被击中的控件</param>
        public static void OnMouseUp(
            IEnumerable<IRControl> controls,
            IMouseEvent sourceEvent,
            bool performClick,
            ref IRControl lastHitControl)
        {
            foreach (IRControl control in controls)
            {
                if (control.Visible &&
                    control.Enabled &&
                    control.HitTest(sourceEvent.X, sourceEvent.Y))
                {
                    lastHitControl = control;
                    control.ProcessMouseEvent(sourceEvent);
                    if (performClick &&
                        control.HitTest(sourceEvent.DownX, sourceEvent.DownY))
                    {
                        IMouseEvent me = new SimpleMouseEvent(
                            MouseEventTypes.Click, sourceEvent);
                        control.ProcessMouseEvent(me);
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// 分配事件
        /// </summary>
        /// <param name="controls">控件列表</param>
        /// <param name="sourceEvent">源事件</param>
        /// <param name="lastHitControl">上次被击中的控件</param>
        public static void OnMouseMove(
            IEnumerable<IRControl> controls,
            IMouseEvent sourceEvent,
            ref IRControl lastHitControl)
        {
            IRControl hitControl = null;
            foreach (IRControl control in controls)
            {
                if (control.Visible && control.Enabled)
                {
                    if (control.HitTest(sourceEvent.X, sourceEvent.Y))
                    {
                        if (lastHitControl != control)
                        {
                            IMouseEvent me = new SimpleMouseEvent(
                                MouseEventTypes.MouseEnter, sourceEvent);
                            control.ProcessMouseEvent(me);
                        }
                        else
                        {
                            control.ProcessMouseEvent(sourceEvent);
                        }
                        hitControl = control;
                    }
                    else if (lastHitControl == control)
                    {
                        IMouseEvent me = new SimpleMouseEvent(
                            MouseEventTypes.MouseExit, sourceEvent);
                        control.ProcessMouseEvent(me);
                    }
                }
            }
            lastHitControl = hitControl;
        }
    }
}
