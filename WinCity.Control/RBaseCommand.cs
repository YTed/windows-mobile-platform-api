using System;
using System.Collections.Generic;
using System.Text;
using PlatformAPI.Imaging;

namespace WinCity.Control
{
    public abstract class RBaseCommand : IRCommand
    {
        #region IRCommand 成员

        public virtual AlphaImage NormalImage
        {
            get
            {
                return normalImage;
            }
            set
            {
                normalImage = value;
            }
        }

        public virtual AlphaImage DisableImage
        {
            get
            {
                return disableImage;
            }
            set
            {
                disableImage = value;
            }
        }

        public virtual bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }
        
        public virtual string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }

        public virtual string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public virtual void OnCreate(object hook)
        {

        }

        public virtual void OnClick()
        {

        }

        public virtual void OnActive()
        {

        }

        public virtual void OnDeactive()
        {

        }

        #endregion

        protected AlphaImage normalImage, disableImage;

        protected string text, name;

        protected bool enabled;
    }
}
