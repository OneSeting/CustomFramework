using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;

namespace Unibone.Caching.ModuleConfiguration
{
    internal class UModuleContext
    {
        internal static UCacheCollection cacheCollection = new UCacheCollection();
        internal static UCacheControlCollection cacheControlCollection = new UCacheControlCollection();
        internal static UCachePlusCollection cachePlusCollection = new UCachePlusCollection();

        static UModuleContext()
        {
            Initialize();
        }
        private static void Initialize()
        {
            Type[] types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes().Where(b => b.BaseType != null && b.BaseType == typeof(UModuleRegister) || b.GetInterfaces().Contains(typeof(IUModuleRegister)))).ToArray();
            for (int i = 0; i < types.Length; i++)
            {
                IUModuleRegister cacheRegister = (IUModuleRegister)Activator.CreateInstance(types[i]);
                cacheRegister.CacheRegister(cacheCollection);
                cacheRegister.CacheControlRegister(cacheControlCollection);
                cacheRegister.CachePlusRegister(cachePlusCollection);
            }
        }

    }
}
