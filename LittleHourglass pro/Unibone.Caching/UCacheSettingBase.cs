using System;
using System.Collections.Generic;
using System.Text;
using Unibone.Caching.ModuleConfiguration;
using Unibone.Caching.URedis;

namespace Unibone.Caching
{
    /// <summary>
    /// 对缓存组件进行配置
    /// </summary>
    public class UCacheSettingBase
    {
        /// <summary>
        /// 为你的redis模块配置连接地址
        /// </summary>
        /// <param name="config"></param>
        public virtual void RedisConfiguration(URedisConfigCollection config)
        {

        }
        /// <summary>
        /// 配置默认使用的缓存 <see cref="IUCache"/> ,<see cref="IUCacheControl"/> ,<see cref="IUCachePlus"/>
        /// </summary>
        /// <param name="config"></param>
        public virtual void DefaultCacheConfiguration(UCacheDefaultConfiguration config)
        {

        }


        /// <summary>
        /// 默认缓存在那个缓存之中
        /// </summary>
        public virtual void WhereIsDefaultCache()
        {

        }
        /// <summary>
        /// string 类型缓存在那个缓存之中
        /// </summary>
        public virtual void WhereIsStringCache()
        {

        }
        /// <summary>
        /// int 类型缓存在那个缓存之中
        /// </summary>
        public virtual void WhereIsIntCache()
        {
        }
        /// <summary>
        /// list 类型缓存在那个缓存之中
        /// </summary>
        public virtual void WhereIsListCache()
        {
        }
        /// <summary>
        /// 指定类型缓存在那个缓存之中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public virtual void WhereIsTCache<T>()
        {
        }
    }
}
