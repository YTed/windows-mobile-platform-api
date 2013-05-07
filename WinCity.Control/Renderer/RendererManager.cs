using System;
using System.Collections.Generic;
using System.Text;
using WinCity.Control.Listener;
using System.IO;

namespace WinCity.Control.Renderer
{
    public class RendererManager : IRendererManager
    {
        private RendererManager()
        {
            listeners = new List<IRendererListener>();
            rendererDict = new Dictionary<string, IRenderer>();
        }

        public static IRendererManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RendererManager();
                }
                return instance;
            }
        }

        #region IRendererManager 成员

        public string CurrentTheme
        {
            get { return currentTheme; }
        }

        public void AddRendererListener(IRendererListener listener)
        {
            if (listener != null)
            {
                listeners.Add(listener);
            }
        }

        public void RemoveRendererListener(IRendererListener listener)
        {
            if (listener != null)
            {
                listeners.Remove(listener);
            }
        }

        public void AddRenderer(string name, IRenderer renderer)
        {
            if (!string.IsNullOrEmpty(name) &&
                renderer != null)
            {
                try
                {
                    string themeDir = GetThemePath(CurrentTheme);
                    renderer.Load(themeDir);
                }
                catch (Exception exp)
                {
                    throw new RendererLoadException(exp);
                }
                rendererDict.Add(name, renderer);
            }
        }

        public IRenderer GetRenderer(string name)
        {
            IRenderer renderer;
            rendererDict.TryGetValue(name, out renderer);
            return renderer;
        }

        public void RemoveRenderer(string name)
        {
            if (!string.IsNullOrEmpty(name) &&
                rendererDict.ContainsKey(name))
            {
                rendererDict.Remove(name);
            }
        }

        public void Load(string theme)
        {
            Store();
            
            Exception innerExp = null;
            bool loadSuccess = true;
            string themeDir = GetThemePath(theme);
            if (Directory.Exists(themeDir))
            {
                foreach (IRenderer renderer in rendererDict.Values)
                {
                    try
                    {
                        renderer.Load(themeDir);
                    }
                    catch (Exception exp)
                    {
                        loadSuccess = false;
                        innerExp = exp;
                    }
                    if (!loadSuccess)
                    {
                        break;
                    }
                }
            }
            else
            {
                throw new RenderThemeNotFoundException();
            }

            if (loadSuccess)
            {
                currentTheme = theme;
            }
            else
            {
                Restore();
                if (innerExp != null)
                {
                    throw new RendererLoadException("加载失败", innerExp);
                }
                else
                {
                    throw new RendererLoadException();
                }
            }
        }

        #endregion

        private string GetThemePath(string theme)
        {
            string themeBasePath = Path.Combine(Utility.StartupPath, "theme");
            return Path.Combine(themeBasePath, theme);
        }
        
        private void Store()
        {
            foreach (IRenderer renderer in rendererDict.Values)
            {
                renderer.Store();
            }
        }

        private void Restore()
        {
            foreach (IRenderer renderer in rendererDict.Values)
            {
                renderer.Restore();
            }
        }

        private static IRendererManager instance;

        private string currentTheme = "default";

        private List<IRendererListener> listeners;

        private Dictionary<string, IRenderer> rendererDict;
    }
}
