using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Caching
{
    public class CacheAttribute : Attribute
    {
        public int CacheTime { get; set; }
        public CacheAttribute()
            : this(600)
        {

        }
        public CacheAttribute(int cacheTime)
        {
            this.CacheTime = cacheTime;
        }
    }

    public interface ICacheable
    {
        string CacheKey { get; }
    }
}
