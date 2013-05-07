using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using PlatformAPI.Imaging;
using WinCity.Control.Listener;

namespace WinCity.Control.Renderer
{
    public interface IRenderer : IDisposable
    {
        /// <summary>
        /// 重置为默认
        /// </summary>
        void Reset();
        /// <summary>
        /// 保存配置
        /// </summary>
        void Store();
        /// <summary>
        /// 恢复配置
        /// </summary>
        void Restore();
        /// <summary>
        /// 加载配置
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        void Load(string themeDir);
        /// <summary>
        /// 指定的配置是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool IsConfigExist(string key);
        /// <summary>
        /// 获取图像
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        AlphaImage GetAlphaImage(string key);
        /// <summary>
        /// 获取图像
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Image GetImage(string key);
        /// <summary>
        /// 获取布尔配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool GetBoolean(string key);
        /// <summary>
        /// 获取文字配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetText(string key);
        /// <summary>
        /// 获取颜色配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Color GetColor(string key);
        /// <summary>
        /// 获取数字配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        int GetNumber(string key);
        /// <summary>
        /// 获取字体配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Font GetFont(string key);
        /// <summary>
        /// 获取配置的浮点数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        float GetFloat(string key);
    }
    
    public interface IRendererManager
    {
        string CurrentTheme { get;}
        void AddRendererListener(IRendererListener listener);
        void RemoveRendererListener(IRendererListener listener);
        /// <summary>
        /// 加载渲染主题
        /// </summary>
        /// <param name="theme"></param>
        /// <exception cref="RendererLoadException">加载错误时</exception>
        /// <exception cref="RendererThemeNotFoundException">找不到渲染文件时</exception>
        void Load(string theme);
        /// <summary>
        /// 添加渲染器
        /// </summary>
        /// <param name="name"></param>
        /// <param name="renderer"></param>
        void AddRenderer(string name, IRenderer renderer);
        /// <summary>
        /// 移除渲染器
        /// </summary>
        /// <param name="name"></param>
        void RemoveRenderer(string name);
        /// <summary>
        /// 获得渲染器
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IRenderer GetRenderer(string name);
    }

    public class RendererLoadException : Exception
    {
        public RendererLoadException()
            : base("配置加载错误") { }
        public RendererLoadException(string msg)
            : base(msg) { }
        public RendererLoadException(Exception innerExp)
            : base("配置加载错误", innerExp) { }
        public RendererLoadException(string msg, Exception innerExp)
            : base(msg, innerExp) { }
    }

    public class RenderThemeNotFoundException : RendererLoadException
    {
        public RenderThemeNotFoundException()
            : base("找不到指定的主题") { }
        public RenderThemeNotFoundException(string msg)
            : base(msg) { }
        public RenderThemeNotFoundException(Exception innerExp)
            : base("找不到指定的主题", innerExp) { }
    }

}
