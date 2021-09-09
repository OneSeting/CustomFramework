using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Unibone.Caching.Utilities;
using System.Linq;

namespace Unibone.Caching.UMemory
{
    /// <summary>
    /// 内部使用
    /// </summary>
    internal class UMemoryCaching
    {
        private static UMemoryCaching _default = new UMemoryCaching();
        public static UMemoryCaching Default { get { return _default; } }
        private ConcurrentDictionary<string, UCacheObject> CacheData = new ConcurrentDictionary<string, UCacheObject>();
        public T Get<T>(string key)
        {
            if (CheckKey(key))
                return default;
            UCacheObject cacheObject = CacheData.GetValueOrDefault(key, null);
            if (cacheObject == null)
                return default;
            return (T)cacheObject.CacheValue.Value;
        }
        public void SetOrUpdate<T>(string key, T value, TimeSpan expireTime, bool slide = false)
        {
            if (CheckKey(key))
                return;
            UCacheObject cacheObject = new UCacheObject();
            cacheObject.CacheInfo = new UCacheInfo(expireTime);
            cacheObject.CacheValue = new UCacheValue(key, value);
            CacheData.AddOrUpdate(key, cacheObject, (k, v) => cacheObject);
        }
        public void SetOrUpdate<T>(List<string> keys, List<T> values, TimeSpan expireTime, bool slide = false)
        {
            for (int i = 0; i < keys.Count; i++)
                SetOrUpdate(keys[i], values[i], expireTime, slide);
        }
        public bool IsSet(string key)
        {
            if (CheckKey(key))
                return false;
            return CacheData.ContainsKey(key);
        }

        public bool Remove(string key)
        {
            UCacheObject cacheObject;
            return CacheData.Remove(key, out cacheObject);
        }
        public bool RemovePattern(string pattern)
        {
            List<string> keys = CacheData.Keys.ToList();
            List<string> removeKeys = keys.Where(a => a.ToLower().Contains(pattern.ToLower())).ToList();
            for (int i = 0; i < removeKeys.Count; i++)
                Remove(removeKeys[i]);
            return true;
        }


        bool CheckKey(string key)
        {
            return string.IsNullOrEmpty(key);
        }
    }
}
