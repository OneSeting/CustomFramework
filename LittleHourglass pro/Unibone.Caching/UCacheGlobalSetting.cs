using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unibone.Caching.ModuleConfiguration;
using Unibone.Caching.URedis;

namespace Unibone.Caching
{
    /// <summary>
    /// 内部的设置不公开
    /// </summary>
    internal class UCacheGlobalSetting
    {
        internal static URedisConfigCollection redisConfigCollection = new URedisConfigCollection();
        internal static UCacheDefaultConfiguration cacheDefaultConfig = new UCacheDefaultConfiguration();
        static UCacheGlobalSetting()
        {
            Initialize();
        }
        static void Initialize()
        {
            Type[] types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes().Where(b => b.BaseType != null && b.BaseType == typeof(UCacheSettingBase))).ToArray();
            for (int i = 0; i < types.Length; i++)
            {
                UCacheSettingBase setting = (UCacheSettingBase)Activator.CreateInstance(types[i]);
                setting.DefaultCacheConfiguration(cacheDefaultConfig);
                setting.RedisConfiguration(redisConfigCollection);
            }
        }
    }
}
