using System;
using System.Collections.Generic;
using System.Text;

namespace Unibone.Caching.ModuleConfiguration
{
    /// <summary>
    /// 继承此类注册你自己实现的<see cref="IUCache"/><see cref="IUCacheControl"/><see cref="IUCachePlus"/> 
    /// </summary>
    public class UModuleRegister : IUModuleRegister
    {
        public virtual void CacheControlRegister(UCacheControlCollection collection)
        {
        }

        public virtual void CachePlusRegister(UCachePlusCollection collection)
        {
        }

        public virtual void CacheRegister(UCacheCollection collection)
        {
        }
    }
}
