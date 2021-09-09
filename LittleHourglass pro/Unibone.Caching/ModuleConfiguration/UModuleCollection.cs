using System;
using System.Collections.Generic;
using System.Text;

namespace Unibone.Caching.ModuleConfiguration
{
    /// <summary>
    /// 模块集合
    /// </summary>
    public abstract class UModuleCollection
    {
        /// <summary>
        /// 模块集合
        /// </summary>
        protected internal Dictionary<string, Type> collection = new Dictionary<string, Type>();
        public abstract bool Add(Type type);
        public abstract bool Add<T>();
    }
}
