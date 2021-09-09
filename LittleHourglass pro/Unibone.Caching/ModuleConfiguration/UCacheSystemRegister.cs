using System;
using System.Collections.Generic;
using System.Text;
using Unibone.Caching.UMemory;
using Unibone.Caching.URedis;
using Unibone.Caching.UTemporary;

namespace Unibone.Caching.ModuleConfiguration
{
    /// <summary>
    /// IUCache 内部的实现注册
    /// </summary>
    internal class UCacheSystemRegister : UModuleRegister
    {
        public override void CacheRegister(UCacheCollection collection)
        {
            collection.Add<UCacheRedis>();
            collection.Add<UCacheMemory>();
            collection.Add<UCacheTemporary>();
            base.CacheRegister(collection);
        }
        public override void CacheControlRegister(UCacheControlCollection collection)
        {
            collection.Add<UCacheControl>();
            base.CacheControlRegister(collection);
        }
        public override void CachePlusRegister(UCachePlusCollection collection)
        {
            collection.Add<UCachePlus>();
            base.CachePlusRegister(collection);
        }
    }
}
