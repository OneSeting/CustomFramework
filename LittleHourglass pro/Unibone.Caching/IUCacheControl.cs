using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Unibone.Caching
{
    /// <summary>
    /// 对缓存的总线控制，可以指定具体要用那个缓存模块
    /// </summary>
    public interface IUCacheControl : IUCacheObject
    {
        /// <summary>
        /// 模块名
        /// </summary>
        string ModuleName { get; }
        T Get<T>(string key, string cacheSource = "Default");
        //Dictionary<string, string> Get(List<string> keys, string cacheSource = "Default");
        //Dictionary<string, T> Get<T>(List<string> keys, string cacheSource = "Default");

        Task<T> GetAsync<T>(string key, string cacheSource = "Default");
        //Task<Dictionary<string, string>> GetAsync(List<string> keys, string cacheSource = "Default");
        //Task<Dictionary<string, T>> GetAsync<T>(List<string> keys, string cacheSource = "Default");
        void Set<T>(T t, TimeSpan expireTime, bool slide = false, string cacheSource = "Default");
        void Set<T>(List<T> ts, TimeSpan expireTime, bool slide = false, string cacheSource = "Default");
        void Set<T>(string key, T t, TimeSpan expireTime, bool slide = false, string cacheSource = "Default");
        void Set<T>(List<string> keys, List<T> ts, TimeSpan expireTime, bool slide = false, string cacheSource = "Default");
        bool Remove(string key, string cacheSource = "Default");
        bool Remove(List<string> keys, string cacheSource = "Default");
        bool RemovePattern(string pattern, string cacheSource = "Default");
        bool RemovePattern(List<string> patterns, string cacheSource = "Default");
        bool IsSet(string key, string cacheSource = "Default");
        //Dictionary<string, bool> IsSet(List<string> keys, string cacheSource = "Default");
    }
}
