using System;
using System.Collections.Generic;
using System.Text;

namespace Unibone.Caching
{
    /// <summary>
    /// 内部缓存名枚举，ToString()
    /// </summary>
    public enum UCacheSource
    {
        /// <summary>
        /// 默认
        /// </summary>
        Default = 0,
        /// <summary>
        /// 内存
        /// </summary>
        UCacheSystemMemory = 1,
        /// <summary>
        /// 临时
        /// </summary>
        UCacheSystemTemporary = 2,
        /// <summary>
        /// redis
        /// </summary>
        UCacheSystemRedis = 3,
    }
}
