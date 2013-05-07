using System;
using System.Collections.Generic;
using System.Text;

namespace WinCity.Control
{
    /// <summary>
    /// 控件中文字与图像的相对位置
    /// </summary>
    [Flags]
    public enum TextImageAlignment : int
    {
        /// <summary>
        /// 默认是文字居左, 在图片下方
        /// </summary>
        Default = TextHCenter | TextUnder,
        /// <summary>
        /// 文字在上方
        /// </summary>
        TextAbove = 0x0000,
        /// <summary>
        /// 文字在下方
        /// </summary>
        TextUnder = 0x0001,
        /// <summary>
        /// 文字在左侧
        /// </summary>
        TextLeft = 0x0002,
        /// <summary>
        /// 文字在右侧
        /// </summary>
        TextRight = 0x0004,
        /// <summary>
        /// 文字在上侧
        /// </summary>
        TextTop = 0x0008,
        /// <summary>
        /// 文字在下侧
        /// </summary>
        TextBottom = 0x0010,
        /// <summary>
        /// 文字垂直居中
        /// </summary>
        TextVCenter = TextTop | TextBottom,
        /// <summary>
        /// 文字水平居中
        /// </summary>
        TextHCenter= TextLeft | TextRight,
        /// <summary>
        /// 文字居中
        /// </summary>
        TextCenter = TextVCenter | TextHCenter,
        /// <summary>
        /// 只显示图像
        /// </summary>
        ImageOnly = 0x1000,
        /// <summary>
        /// 只显示文字
        /// </summary>
        TextOnly = 0x2000
    }
}
