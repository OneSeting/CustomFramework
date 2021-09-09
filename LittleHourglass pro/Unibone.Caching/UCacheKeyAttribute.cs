using System;
using System.Collections.Generic;
using System.Text;

namespace Unibone.Caching
{
    [AttributeUsage(AttributeTargets.Property)]
    public class UCacheKeyAttribute : Attribute
    {
        public UCacheKeyAttribute(string key)
        {
            this.Key = key;
        }
        public UCacheKeyAttribute(string key, int index) : this(key)
        {
            this.Index = index;
        }
        /// <summary>
        /// 缓存key，例子：key{0}
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 排序，在连接缓存key 的时候先后顺序
        /// </summary>
        public int Index { get; set; }
    }
}
