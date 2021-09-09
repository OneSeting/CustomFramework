using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Unibone.Caching
{
    /// <summary>
    /// 使用此接口则是明确知道要用那个缓存模块，一般推荐使用<see cref="IUCacheControl"/> 
    /// </summary>
    public interface IUCache : IUCacheObject
    {
        /// <summary>
        /// 缓存模块名内部的缓存名请勿占用<see cref="UCacheSource"/>
        /// </summary>
        string ModuleName { get; }
        T Get<T>(string key);
        //Dictionary<string, string> Get(List<string> keys);
        //Dictionary<string, T> Get<T>(List<string> keys);
        Task<T> GetAsync<T>(string key);
        //Task<Dictionary<string, string>> GetAsync(List<string> keys);
        //Task<Dictionary<string, T>> GetAsync<T>(List<string> keys);
        void Set<T>(T t, TimeSpan expireTime, bool slide = false);
        void Set<T>(List<T> ts, TimeSpan expireTime, bool slide = false);
        void Set<T>(string key, T t, TimeSpan expireTime, bool slide = false);
        void Set<T>(List<string> keys, List<T> ts, TimeSpan expireTime, bool slide = false);
        bool Remove(string key);
        bool Remove(List<string> keys);
        bool RemovePattern(string pattern);
        bool RemovePattern(List<string> patterns);
        bool IsSet(string key);
        //Dictionary<string, bool> IsSet(List<string> keys);
    }
}
