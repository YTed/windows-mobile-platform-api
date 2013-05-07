using System;
using System.Collections.Generic;
using System.Text;

namespace WinCity.Control.Renderer
{
    public class ToolbarRenderer : BaseRenderer
    {
        public ToolbarRenderer()
        {
            configFiles = new string[] { "common.xml", "toolbar.xml"};
            configs = new RendererConfig[]{
                Config(SCROLL_VISIBLE, "/ui/scroll", "visible", RendererValueTypes.Boolean),
                Config(SCROLL_BACK_COLOR, "/ui/scroll", "back-color", RendererValueTypes.Color),
                Config(SCROLL_FORE_COLOR, "/ui/scroll", "fore-color", RendererValueTypes.Color),
                Config(SCROLL_WIDTH, "/ui/scroll", "width", RendererValueTypes.Integer),
                Config(FONT, "/ui/normal", "font", RendererValueTypes.Font),
                Config(BG_TRANSPARENT, "/ui/normal", "bg-transparent", RendererValueTypes.Boolean),
                Config(BACK_COLOR, "/ui/normal", "back-color", RendererValueTypes.Color),
                Config(FORE_COLOR, "/ui/normal", "fore-color", RendererValueTypes.Color),
                Config(ITEM_PADDING, "/ui/normal", "item-padding", RendererValueTypes.Integer),
                Config(SHOW_TEXT, "/ui/normal", "show-text", RendererValueTypes.Boolean),
                Config(ITEM_WIDTH, "/ui/normal", "item-width", RendererValueTypes.Integer),
                Config(ITEM_HEIGHT, "/ui/normal", "item-height", RendererValueTypes.Integer)
            };
        }
        
        protected override BaseRenderer.RendererConfig[] RendererConfigs
        {
            get { return configs; }
        }

        protected override string[] ExpectedConfigFiles
        {
            get { return configFiles; }
        }

        public const string
            BG_TRANSPARENT = "Toolbar:Normal:BackgroundTransparent",
            FONT = "Toolbar:Normal:Font",
            BACK_COLOR = "Toolbar:Normal:BackColor",
            FORE_COLOR = "Toolbar:Normal:ForeColor",
            SHOW_TEXT = "Toolbar:Normal:ShowText",
            ITEM_PADDING = "Toolbar:Normal:ItemHPadding",
            ITEM_WIDTH = "Toolbar:Normal:ItemWidth",
            ITEM_HEIGHT = "Toolbar:Normal:ItemHeight",
            SCROLL_BACK_COLOR = "Toolbar:Scroll:BackColor",
            SCROLL_FORE_COLOR = "Toolbar:Scroll:ForeColor",
            SCROLL_WIDTH = "Toolbar:Scroll:Width",
            SCROLL_VISIBLE = "Toolbar:Scroll:Visible";
        
        public const string NAME = "ToolbarRenderer";

        private RendererConfig[] configs;
        private string[] configFiles;
    }
}
