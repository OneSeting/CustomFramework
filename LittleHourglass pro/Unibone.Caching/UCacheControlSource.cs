using System;
using System.Collections.Generic;
using System.Text;

namespace Unibone.Caching
{
    public enum UCacheControlSource
    {
        /// <summary>
        /// 默认
        /// </summary>
        Default = 0,
        /// <summary>
        /// UCache内部的CacheControl
        /// </summary>
        UCacheSystemControl = 1,
    }
}
