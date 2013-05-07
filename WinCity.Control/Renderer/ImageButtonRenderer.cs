using System;
using System.Collections.Generic;
using System.Text;

namespace WinCity.Control.Renderer
{
    public class ImageButtonRenderer : BaseRenderer
    {
        public ImageButtonRenderer()
        {
            configFiles = new string[] { "common.xml", "image-button.xml" };
            configs = new RendererConfig[]{
                Config(FONT, "/ui/normal", "font", RendererValueTypes.Font),
                Config(FORE_COLOR, "/ui/normal", "fore-color", RendererValueTypes.Color),
                Config(SHOW_TEXT, "/ui/normal", "show-text", RendererValueTypes.Boolean)
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

        public const string NAME = "ImageButtonRenderer";

        public const string
            FONT = "ImageButton:Normal:Font",
            FORE_COLOR = "ImageButton:Normal:ForeColor",
            SHOW_TEXT = "ImageButton:Normal:ShowText";
        
        private RendererConfig[] configs;
        private string[] configFiles;
    }
}
