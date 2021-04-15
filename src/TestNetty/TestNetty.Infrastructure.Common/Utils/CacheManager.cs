using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestNetty.Infrastructure.Common.Utils
{
    public class CacheManager
    {
        private static IMemoryCache memoryCache = null;
        private static CacheManager _instance;
        private static readonly object _lockObj = new object();

        public static CacheManager Instance 
        {
            get 
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new CacheManager();
                            memoryCache = new MemoryCache(Options.Create(new MemoryCacheOptions()));
                        }
                    }
                }
                return _instance;
            } 
        }

        public void Remove<TItem>(object key)
        {
            memoryCache.Remove(key);
        }

        public void Set<TItem>(object key, TItem item) 
        {
            memoryCache.Set(key, item);
        }

        public void Set<TItem>(object key, TItem item, TimeSpan timeSpan)
        {
            memoryCache.Set(key, item, timeSpan);
        }

        public void Get(object key,out object value) 
        {
            memoryCache.TryGetValue(key,out value);
        }
    }
}
