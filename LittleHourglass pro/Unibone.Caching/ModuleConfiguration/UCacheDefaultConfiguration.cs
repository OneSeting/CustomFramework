using System;
using System.Collections.Generic;
using System.Text;

namespace Unibone.Caching.ModuleConfiguration
{
    public class UCacheDefaultConfiguration
    {
        /// <summary>
        /// 默认使用的缓存模块
        /// </summary>
        public string DefaultCache { get; set; } = UCacheSource.UCacheSystemTemporary.ToString();
        /// <summary>
        /// 默认使用的缓存总控模块
        /// </summary>
        public string DefaultCacheControl { get; set; } = UCacheControlSource.UCacheSystemControl.ToString();
        /// <summary>
        /// 默认使用的缓存plus模块
        /// </summary>
        public string DefaultCachePlus { get; set; } = UCachePlusSource.UCacheSystemPlus.ToString();
    }
}
