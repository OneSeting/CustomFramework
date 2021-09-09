using System;
using System.Collections.Generic;
using System.Text;

namespace Unibone.Caching
{
    internal class UCacheInfo
    {
        public UCacheInfo()
        { }
        public UCacheInfo(TimeSpan expireTime)
        {
            this.InsertTime = DateTime.Now;
            this.ExpireTime = DateTime.Now.AddMinutes(expireTime.TotalMinutes);
        }
        /// <summary>
        /// 缓存添加的时间
        /// </summary>
        public DateTime InsertTime { get; set; }
        /// <summary>
        /// 缓存到期的时间
        /// </summary>
        public DateTime ExpireTime { get; set; }
    }
}
