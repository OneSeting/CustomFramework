using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Unibone.Caching.ModuleConfiguration;

namespace Unibone.Caching
{
    internal class UCacheControl : IUCacheControl
    {
        public string ModuleName { get; } = UCacheControlSource.UCacheSystemControl.ToString();

        public virtual T Get<T>(string key, string cacheSource = "Default")
        {
            cacheSource = GetDefaultCacheSource(cacheSource);
            IUCache cache = UModuleContext.cacheCollection[cacheSource];
            if (cache == null)
                return default;
            return cache.Get<T>(key);
        }

        //public Dictionary<string, string> Get(List<string> keys, string cacheSource = "Default")
        //{
        //    IUCache cache = UModuleContext.Collection[cacheSource];
        //    if (cache == null)
        //        return default;
        //    return cache.Get(keys);
        //}

        //public Dictionary<string, T> Get<T>(List<string> keys, string cacheSource = "Default")
        //{
        //    IUCache cache = UModuleContext.Collection[cacheSource];
        //    if (cache == null)
        //        return default;
        //    return cache.Get<T>(keys);
        //}

        public virtual Task<T> GetAsync<T>(string key, string cacheSource = "Default")
        {
            return Task.Run(() =>
            {
                return this.Get<T>(key, cacheSource);
            });
        }

        //public Task<Dictionary<string, string>> GetAsync(List<string> keys, string cacheSource = "Default")
        //{
        //    return Task.Run(() =>
        //    {
        //        return this.Get(keys, cacheSource);
        //    });
        //}

        //public Task<Dictionary<string, T>> GetAsync<T>(List<string> keys, string cacheSource = "Default")
        //{
        //    return Task.Run(() =>
        //    {
        //        return this.Get<T>(keys, cacheSource);
        //    });
        //}

        public virtual bool IsSet(string key, string cacheSource = "Default")
        {
            cacheSource = GetDefaultCacheSource(cacheSource);
            IUCache cache = UModuleContext.cacheCollection[cacheSource];
            if (cache == null)
                return default;
            return cache.IsSet(key);
        }

        //public Dictionary<string, bool> IsSet(List<string> keys, string cacheSource = "Default")
        //{
        //    IUCache cache = UModuleContext.Collection[cacheSource];
        //    if (cache == null)
        //        return default;
        //    return cache.IsSet(keys);
        //}

        public virtual bool Remove(string key, string cacheSource = "Default")
        {
            cacheSource = GetDefaultCacheSource(cacheSource);
            IUCache cache = UModuleContext.cacheCollection[cacheSource];
            if (cache == null)
                return default;
            return cache.Remove(key);
        }

        public virtual bool Remove(List<string> keys, string cacheSource = "Default")
        {
            cacheSource = GetDefaultCacheSource(cacheSource);
            IUCache cache = UModuleContext.cacheCollection[cacheSource];
            if (cache == null)
                return default;
            return cache.Remove(keys);
        }

        public virtual bool RemovePattern(string pattern, string cacheSource = "Default")
        {
            cacheSource = GetDefaultCacheSource(cacheSource);
            IUCache cache = UModuleContext.cacheCollection[cacheSource];
            if (cache == null)
                return default;
            return cache.RemovePattern(pattern);
        }

        public virtual bool RemovePattern(List<string> patterns, string cacheSource = "Default")
        {
            cacheSource = GetDefaultCacheSource(cacheSource);
            IUCache cache = UModuleContext.cacheCollection[cacheSource];
            if (cache == null)
                return default;
            return cache.RemovePattern(patterns);
        }

        public virtual void Set<T>(T t, TimeSpan expireTime, bool slide = false, string cacheSource = "Default")
        {
            cacheSource = GetDefaultCacheSource(cacheSource);
            IUCache cache = UModuleContext.cacheCollection[cacheSource];
            if (cache == null)
                return;
            cache.Set(t, expireTime, slide);
        }

        public virtual void Set<T>(List<T> ts, TimeSpan expireTime, bool slide = false, string cacheSource = "Default")
        {
            cacheSource = GetDefaultCacheSource(cacheSource);
            IUCache cache = UModuleContext.cacheCollection[cacheSource];
            if (cache == null)
                return;
            cache.Set(ts, expireTime, slide);
        }

        public void Set<T>(string key, T t, TimeSpan expireTime, bool slide = false, string cacheSource = "Default")
        {
            cacheSource = GetDefaultCacheSource(cacheSource);
            IUCache cache = UModuleContext.cacheCollection[cacheSource];
            if (cache == null)
                return;
            cache.Set(key, t, expireTime, slide);
        }

        public void Set<T>(List<string> keys, List<T> ts, TimeSpan expireTime, bool slide = false, string cacheSource = "Default")
        {
            cacheSource = GetDefaultCacheSource(cacheSource);
            IUCache cache = UModuleContext.cacheCollection[cacheSource];
            if (cache == null)
                return;
            cache.Set(keys, ts, expireTime, slide);
        }

        /// <summary>
        /// 如果传入的是默认则取设置的默认缓存模块
        /// </summary>
        /// <param name="cacheSource"></param>
        /// <returns></returns>
        private string GetDefaultCacheSource(string cacheSource)
        {
            if (cacheSource.ToLower() == "default")
                return UCacheGlobalSetting.cacheDefaultConfig.DefaultCache;
            return cacheSource;
        }
    }
}
