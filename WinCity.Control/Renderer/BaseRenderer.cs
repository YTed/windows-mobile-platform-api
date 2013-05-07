using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using PlatformAPI.Imaging;
using PlatformAPI.Managed;
using System.Xml;
using System.IO;

namespace WinCity.Control.Renderer
{
    /// <summary>
    /// may throw all kinds of FormatExceptions ...
    /// </summary>
    public abstract class BaseRenderer : IRenderer
    {
        protected BaseRenderer()
        {
            rdrConfig = new RendererStorage();
            defaultConfig = new RendererStorage();
            storage = new RendererStorage();
        }

        #region IDisposable 成员

        public void Dispose()
        {
            rdrConfig.Dispose();
            defaultConfig.Dispose();
            storage.Dispose();
        }

        #endregion

        #region IRenderer 成员

        public virtual void Reset()
        {
            defaultConfig.CopyTo(rdrConfig);
        }

        public virtual void Store()
        {
            rdrConfig.CopyTo(storage);
        }

        public virtual void Restore()
        {
            storage.CopyTo(rdrConfig);
        }

        public virtual AlphaImage GetAlphaImage(string key)
        {
            return TryGetValue(rdrConfig.key2AlphaImage, key);
        }

        public virtual Image GetImage(string key)
        {
            return TryGetValue(rdrConfig.key2Image, key);
        }

        public virtual bool GetBoolean(string key)
        {
            return TryGetValue(rdrConfig.key2Boolean, key);
        }

        public virtual string GetText(string key)
        {
            return TryGetValue(rdrConfig.key2Text, key);
        }

        public virtual Color GetColor(string key)
        {
            return TryGetValue(rdrConfig.key2Color, key);
        }

        public virtual int GetNumber(string key)
        {
            return TryGetValue(rdrConfig.key2Number, key);
        }

        public virtual Font GetFont(string key)
        {
            return TryGetValue(rdrConfig.key2Font, key);
        }

        public virtual float GetFloat(string key)
        {
            return TryGetValue(rdrConfig.key2Float, key);
        }

        public virtual bool IsConfigExist(string key)
        {
            return rdrConfig.ExistConfig(key);
        }

        public virtual void Load(string themeDir)
        {
            if (string.IsNullOrEmpty(themeDir))
            {
                throw new ArgumentNullException();
            }
            foreach (string cfgfile in ExpectedConfigFiles)
            {
                string file = Path.Combine(themeDir, cfgfile);
                if (File.Exists(file))
                {
                    LoadConfig(file);
                }
            }
        }

        #endregion

        protected static RendererConfig Config(
            string key,
            string xpath,
            string att,
            RendererValueTypes type)
        {
            xpath =string.Format("{0}[@{1}]", xpath, att);
            return new RendererConfig(key, xpath, att, type);
        }
        
        protected virtual void LoadConfig(string file)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(file);
            RendererConfig[] configs = RendererConfigs;
            foreach (RendererConfig config in configs)
            {
                XmlNode node = xmldoc.SelectSingleNode(config.xpath);
                if (node != null)
                {
                    XmlAttribute xmlAttr = node.Attributes[config.attribute];
                    if (xmlAttr == null)
                    {
                        continue;
                    }
                    rdrConfig.AddConfig(config.key);
                    string text = xmlAttr.Value;
                    string key = config.key;
                    switch (config.type)
                    {
                        case RendererValueTypes.Text:
                            AddOrReplaceValue(rdrConfig.key2Text, key, text);
                            break;
                        case RendererValueTypes.Boolean:
                            bool boolean = TranslateBoolean(text);
                            AddOrReplaceValue(rdrConfig.key2Boolean, key, boolean);
                            break;
                        case RendererValueTypes.Integer:
                            int integerNumber = TranslateInteger(text);
                            AddOrReplaceValue(rdrConfig.key2Number, key, integerNumber);
                            break;
                        case RendererValueTypes.Color:
                            Color color = TranslateColor(text);
                            AddOrReplaceValue(rdrConfig.key2Color, key, color);
                            break;
                        case RendererValueTypes.Font:
                            Font font = TranslateFont(text);
                            AddOrReplaceValue(rdrConfig.key2Font, key, font);
                            break;
                        case RendererValueTypes.Image:
                            Image image = TranslateImage(text);
                            AddOrReplaceValue(rdrConfig.key2Image, key, image);
                            break;
                        case RendererValueTypes.AlphaImage:
                            AlphaImage alphaImage = TranslateAlphaImage(text);
                            AddOrReplaceValue(rdrConfig.key2AlphaImage, key, alphaImage);
                            break;
                        case RendererValueTypes.Float:
                            float floatNumber = TranslateFloat(text);
                            AddOrReplaceValue(rdrConfig.key2Float, key, floatNumber);
                            break;
                    }
                }
            }
        }

        protected virtual AlphaImage TranslateAlphaImage(string text)
        {
            return Utility.GetAlphaImage(text);
        }

        protected virtual bool TranslateBoolean(string text)
        {
            return text.Equals("t", StringComparison.CurrentCultureIgnoreCase) ||
                text.Equals("true", StringComparison.CurrentCultureIgnoreCase);
        }

        protected virtual int TranslateInteger(string text)
        {
            int value = 0;
            // only accept the form of #XXXXXXXX
            if (text.StartsWith("#") && text.Length <= 9)
            {
                value = Converter.Hex2Int32(text.Substring(1));
            }
            else
            {
                value = int.Parse(text);
            }
            return value;
        }

        protected virtual Color TranslateColor(string text)
        {
            int argb = Converter.Hex2Int32(text.Substring(1));
            return Color.FromArgb(argb);
        }

        protected virtual Image TranslateImage(string text)
        {
            return Utility.GetImage(text);
        }

        protected virtual float TranslateFloat(string text)
        {
            return float.Parse(text);
        }

        protected virtual Font TranslateFont(string text)
        {
            string[] fontConfig = text.Split(',');
            if (fontConfig.Length == 2)
            {
                float size = float.Parse(fontConfig[1].Trim());
                string fontFamily = fontConfig[0].Trim();
                return new Font(fontFamily, size, FontStyle.Regular);
            }
            return null;
        }

        protected abstract RendererConfig[] RendererConfigs { get;}

        protected abstract string[] ExpectedConfigFiles { get;}
        
        private static TValue TryGetValue<TKey, TValue>(
            Dictionary<TKey, TValue> dict, TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException();
            }
            TValue value;
            dict.TryGetValue(key, out value);
            return value;
        }

        private static void AddOrReplaceValue<TKey, TValue>(
            Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict.ContainsKey(key))
            {
                dict[key] = value;
            }
            else
            {
                dict.Add(key, value);
            }
        }
        
        protected RendererStorage rdrConfig, defaultConfig, storage;

        protected class RendererConfig
        {
            public RendererConfig(string key, string xpath, string attribute, RendererValueTypes type)
            {
                this.key = key;
                this.xpath = xpath;
                this.attribute = attribute;
                this.type = type;
            }
            
            public string key;
            public string xpath;
            public string attribute;
            public RendererValueTypes type;
        }
        
        protected class RendererStorage : IDisposable
        {
            public RendererStorage()
            {
                key2Text = new Dictionary<string, string>();
                key2Boolean = new Dictionary<string, bool>();
                key2Number = new Dictionary<string, int>();
                key2Color = new Dictionary<string, Color>();
                key2Font = new Dictionary<string, Font>();
                key2Image = new Dictionary<string, Image>();
                key2AlphaImage = new Dictionary<string, AlphaImage>();
                key2Float = new Dictionary<string, float>();
                configKeyList = new List<string>();
            }

            public void Dispose()
            {
                Clear();
            }
            
            public void Clear()
            {
                Dispose(key2Font);
                Dispose(key2Image);
                Dispose(key2AlphaImage);
                key2Text.Clear();
                key2Boolean.Clear();
                key2Color.Clear();
                key2Number.Clear();
                key2Font.Clear();
                key2Image.Clear();
                key2AlphaImage.Clear();
                key2Float.Clear();
                configKeyList.Clear();
            }

            public bool ExistConfig(string key)
            {
                return configKeyList.Contains(key);
            }

            public void AddConfig(string key)
            {
                configKeyList.Add(key);
            }
            
            public void CopyTo(RendererStorage storage)
            {
                if (storage == null)
                {
                    throw new ArgumentNullException();
                }
                storage.Clear();
                CopyDictionary(key2Text, storage.key2Text);
                CopyDictionary(key2Boolean, storage.key2Boolean);
                CopyDictionary(key2Number, storage.key2Number);
                CopyDictionary(key2Color, storage.key2Color);
                CopyDictionary(key2Font, storage.key2Font);
                CopyDictionary(key2Image, storage.key2Image);
                CopyDictionary(key2AlphaImage, storage.key2AlphaImage);
                CopyDictionary(key2Float, storage.key2Float);
                storage.configKeyList.AddRange(configKeyList);
            }
            
            private void CopyDictionary<TKey, TValue>(
                Dictionary<TKey, TValue> source,
                Dictionary<TKey, TValue> destination)
            {
                foreach (TKey key in source.Keys)
                {
                    TValue value = source[key];
                    destination.Add(key, value);
                }
            }

            private void Dispose<TKey, TValue>(Dictionary<TKey, TValue> dict)
                where TValue : IDisposable
            {
                foreach (TValue value in dict.Values)
                {
                    if (value != null)
                    {
                        value.Dispose();
                    }
                }
            }
            
            public Dictionary<string, string> key2Text;
            public Dictionary<string, bool> key2Boolean;
            public Dictionary<string, int> key2Number;
            public Dictionary<string, Color> key2Color;
            public Dictionary<string, Font> key2Font;
            public Dictionary<string, Image> key2Image;
            public Dictionary<string, AlphaImage> key2AlphaImage;
            public Dictionary<string, float> key2Float;

            private List<string> configKeyList;
        }

        protected enum RendererValueTypes
        {
            Text,
            Boolean,
            Integer,
            Color,
            Font,
            Image,
            AlphaImage,
            Float
        }
    }
}
