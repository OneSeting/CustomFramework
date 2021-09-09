using System;
using System.Collections.Generic;
using System.Text;

namespace LittleHourglass.Commonality.Base.Enum
{
    public enum PasswordFormat
    {
        /// <summary>
        /// 无加密方式
        /// </summary>
        Clear = 0,
        /// <summary>
        /// Hash256加密方式 
        /// </summary>
        Hashed = 1,
        /// <summary>
        /// Md5加密方式
        /// </summary>
        Md5ed = 2
    }
}
