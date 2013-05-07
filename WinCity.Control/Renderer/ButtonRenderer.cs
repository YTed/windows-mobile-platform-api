using System;
using System.Collections.Generic;
using System.Text;

namespace WinCity.Control.Renderer
{
    public class ButtonRenderer : BaseRenderer
    {
        public ButtonRenderer()
        {
            configFiles = new string[] { "common.xml", "button.xml" };
            configs = new RendererConfig[]{
                Config(FONT, "/ui/normal", "font", RendererValueTypes.Font),
                Config(CONTROL_USE_GRADIENT, "/ui/control", "use-gradient", RendererValueTypes.Boolean),
                Config(BACK_COLOR, "/ui/normal", "back-color", RendererValueTypes.Color),
                Config(FORE_COLOR, "/ui/normal", "fore-color", RendererValueTypes.Color),
                Config(BORDER_COLOR, "/ui/normal", "border-color", RendererValueTypes.Color),
                Config(BORDER_WIDTH, "/ui/normal", "border-width", RendererValueTypes.Integer),
                Config(TOP_GRADIENT_COLOR, "/ui/normal", "top-gradient-color", RendererValueTypes.Color),
                Config(TOP_GRADIENT_HEIGHT, "/ui/normal", "top-gradient-height", RendererValueTypes.Integer),
                Config(BOTTOM_GRADIENT_COLOR, "/ui/normal", "bottom-gradient-color", RendererValueTypes.Color),
                Config(BOTTOM_GRADIENT_HEIGHT, "/ui/normal", "bottom-gradient-height", RendererValueTypes.Integer),
                Config(PREFERRED_HEIGHT, "/ui/normal", "preferred-height", RendererValueTypes.Integer),
                Config(PRESSED_BACK_COLOR, "/ui/pressed", "back-color", RendererValueTypes.Color)
            };
        }
        
        protected override RendererConfig[] RendererConfigs
        {
            get { return configs; }
        }

        protected override string[] ExpectedConfigFiles
        {
            get { return configFiles; }
        }

        public const string
            CONTROL_USE_GRADIENT = "Button:Control:UseGradient",
            FONT = "Button:Normal:Font",
            BACK_COLOR = "Button:Normal:BackColor",
            FORE_COLOR = "Button:Normal:ForeColor",
            BORDER_WIDTH = "Button:Normal:BorderWidth",
            BORDER_COLOR = "Button:Normal:BorderColor",
            TOP_GRADIENT_COLOR = "Button:Normal:TopGradientColorBegin",
            BOTTOM_GRADIENT_COLOR = "Button:Normal:BottomGradientColorBegin",
            TOP_GRADIENT_HEIGHT = "Button:Normal:TopGradientHeight",
            BOTTOM_GRADIENT_HEIGHT = "Button:Normal:BottomGradientHeight",
            PREFERRED_HEIGHT = "Button:Normal:PreferredHeight",
            PRESSED_BACK_COLOR = "Button:Pressed:BackColor";
        
        public const string NAME = "ButtonRenderer";
        
        private RendererConfig[] configs;
        private string[] configFiles;
    }
}
