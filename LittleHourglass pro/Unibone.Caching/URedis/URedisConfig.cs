using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Unibone.Caching.URedis
{
    public class URedisConfig : UCacheConfiguration
    {
        public URedisConfig()
        { }
        public URedisConfig(string moduleName)
        {
            this.ModuleName = moduleName;
        }
        public URedisConfig(string moduleName, string ipAddress, string port) : this(moduleName)
        {
            this.IpAddress = ipAddress;
            this.Port = port;
        }
        public URedisConfig(string moduleName, string ipAddress, string port, int defaultDatabase) : this(moduleName, ipAddress, port)
        {
            this.DefaultDatabase = defaultDatabase;
        }
        public URedisConfig(string moduleName, string ipAddress, string port, int defaultDatabase, string keyPrefix) : this(moduleName, ipAddress, port, defaultDatabase)
        {
            this.KeyPrefix = keyPrefix;
        }
        /// <summary>
        /// 模块名
        /// </summary>
        public string ModuleName { get; set; } = UCacheSource.UCacheSystemRedis.ToString();
        /// <summary>
        /// redis 连接地址
        /// </summary>
        public string IpAddress { get; set; } = "127.0.0.1";
        /// <summary>
        /// redis 端口
        /// </summary>
        public string Port { get; set; } = "6379";
        /// <summary>
        /// 默认缓存的db
        /// </summary>
        public int DefaultDatabase { get; set; } = 0;
        /// <summary>
        /// Redis密码
        /// </summary>
        public string Password { get; set; } = "123456";
        private ConnectionMultiplexer _connection = null;
        internal ConnectionMultiplexer Connection
        {
            get
            {
                if (_connection == null)
                {
                    try
                    {
                        _connection = ConnectionMultiplexer.Connect(string.Join(":", this.IpAddress, this.Port));
                    }
                    catch
                    { }
                }
                return _connection;
            }
            set
            {
                _connection = value;
            }
        }
        /// <summary>
        /// 克隆配置对象
        /// </summary>
        /// <returns></returns>
        internal URedisConfig Clone()
        {
            URedisConfig config = new URedisConfig();
            config.ModuleName = this.ModuleName;
            config.IpAddress = this.IpAddress;
            config.Port = this.Port;
            config.DefaultDatabase = this.DefaultDatabase;
            config.KeyPrefix = this.KeyPrefix;
            config.Password = this.Password;
            config.Connection = this.Connection;
            return config;
        }
    }
}
