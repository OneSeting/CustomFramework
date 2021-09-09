using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Unibone.Caching
{
    /// <summary>
    /// 使用此接口你无需知道缓存到哪里或者怎样缓存，因为它会帮你自动处理并放到合适的缓存地
    /// </summary>
    public interface IUCachePlus : IUCache
    {
        
    }
}
