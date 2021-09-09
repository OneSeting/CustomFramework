using System;
using System.Collections.Generic;
using System.Text;
using Unibone.Caching.ModuleConfiguration;

namespace Unibone.Caching
{
    /// <summary>
    /// 创建<see cref="IUCache"/> <see cref="IUCacheControl"/> <see cref="IUCachePlus"/> 实例 也只能创建这三种接口的实例T必须为接口
    /// </summary>
    public class UCacheActivator
    {
        /// <summary>
        /// 创建默认的实例，默认的配置请继承此类<see cref="UCacheSettingBase"/>进行配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>T不是接口
        public static T CreateInstance<T>() where T : class, IUCacheObject
        {
            return Create<T>("default");
        }
        /// <summary>
        /// 创建指定模块名的实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        public static T CreateInstance<T>(string moduleName) where T : class, IUCacheObject
        {
            return Create<T>(moduleName);
        }
        private static T Create<T>(string moduleName) where T : class, IUCacheObject
        {
            Type type = typeof(T);
            if (type == typeof(IUCache))
            {
                if (moduleName.ToLower() == "default")
                    moduleName = UCacheGlobalSetting.cacheDefaultConfig.DefaultCache;
                return (T)UModuleContext.cacheCollection[moduleName];
            }
            else if (type == typeof(IUCacheControl))
            {
                if (moduleName.ToLower() == "default")
                    moduleName = UCacheGlobalSetting.cacheDefaultConfig.DefaultCacheControl;
                return (T)UModuleContext.cacheControlCollection[moduleName];
            }
            else if (type == typeof(IUCachePlus))
            {
                if (moduleName.ToLower() == "default")
                    moduleName = UCacheGlobalSetting.cacheDefaultConfig.DefaultCachePlus;
                return (T)UModuleContext.cachePlusCollection[moduleName];
            }
            return default;
        }
    }
}
