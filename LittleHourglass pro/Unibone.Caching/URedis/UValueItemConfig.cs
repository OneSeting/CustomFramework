using System;
using System.Collections.Generic;
using System.Text;

namespace Unibone.Caching.URedis
{
    /// <summary>
    /// 设置具体缓存到redis 的那个database和特殊的前缀
    /// </summary>
    public class UValueItemConfig
    {
        public UValueItemConfig()
        { }
        /// <summary>
        /// 设置具体缓存到redis 的那个database和特殊的前缀
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <param name="fuzzyMatching">是否模糊匹配</param>
        /// <param name="useDefaultPrefix"></param>
        public UValueItemConfig(string key, int database, bool fuzzyMatching = false, bool useDefaultPrefix = true)
        {
            this.Key = key;
            this.Database = database;
            this.FuzzyMatching = fuzzyMatching;
            this.UseDefaultPrefix = useDefaultPrefix;
        }
        /// <summary>
        /// 设置具体缓存到redis 的那个database和特殊的前缀
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <param name="prefix"></param>
        /// <param name="fuzzyMatching">是否模糊匹配</param>
        /// <param name="useDefaultPrefix"></param>
        public UValueItemConfig(string key, int database, string prefix, bool fuzzyMatching = false, bool useDefaultPrefix = false) : this(key, database, fuzzyMatching, useDefaultPrefix)
        {
            this.Prefix = prefix;
        }
        /// <summary>
        /// key
        /// </summary>
        private string _key = null;
        public string Key
        {
            get
            {
                if (_key == null)
                    return _key;
                return _key.ToLower();
            }
            set { _key = value.ToLower(); }
        }
        /// <summary>
        /// 模糊匹配，则只要key 中包含此处设置的key 就用此配置
        /// </summary>
        public bool FuzzyMatching { get; set; } = false;
        /// <summary>
        /// redis database index
        /// </summary>
        public int Database { get; set; }
        /// <summary>
        /// 缓存前缀
        /// </summary>
        public string Prefix { get; set; }
        /// <summary>
        /// 是否使用默认模块的缓存前缀
        /// </summary>
        public bool UseDefaultPrefix { get; set; }

        public UValueItemConfig Clone()
        {
            UValueItemConfig config = new UValueItemConfig();
            config.Key = this.Key;
            config.FuzzyMatching = this.FuzzyMatching;
            config.Database = this.Database;
            config.Prefix = this.Prefix;
            config.UseDefaultPrefix = this.UseDefaultPrefix;
            return config;
        }
    }
}
