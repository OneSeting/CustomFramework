using System;
using System.Collections.Generic;
using System.Text;

namespace Unibone.Caching.ModuleConfiguration
{
    /// <summary>
    /// 注册你自己实现的<see cref="IUCache"/><see cref="IUCacheControl"/><see cref="IUCachePlus"/> 
    /// </summary>
    public interface IUModuleRegister
    {
        void CacheRegister(UCacheCollection collection);
        void CacheControlRegister(UCacheControlCollection collection);
        void CachePlusRegister(UCachePlusCollection collection);
    }
}
