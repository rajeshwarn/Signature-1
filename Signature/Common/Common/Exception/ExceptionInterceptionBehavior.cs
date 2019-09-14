using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using Common.Caching;
using Common.Object;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Common.Exception
{
    public class CachingInterceptionBehavior : IInterceptionBehavior
    {
        private static ConcurrentDictionary<string, MemoryCache> Cache = new ConcurrentDictionary<string, MemoryCache>();
        private static IList<string> SyncLock = new List<string>();
        private static object SyncRoot = new object();

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public bool WillExecute
        {
            get { return true; }
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            CacheAttribute attr = input.MethodBase.GetCustomAttribute<CacheAttribute>();
            if (attr == null) return getNext()(input, getNext);
            var arguments = input.MethodBase.GetParameters().ToString(input);
            string key = $"{arguments}";
            string methodName = $"{input.MethodBase.Name}|{input.MethodBase.DeclaringType.FullName}";
            var mc = Cache.GetOrAdd(methodName, x => { return new MemoryCache(x); });
            var data = mc.Get(key);
            if (data != null) return input.CreateMethodReturn(data);
            IMethodReturn result = getNext()(input, getNext);
            if (result.ReturnValue == null) return result;
            mc.Add(key, result.ReturnValue, new CacheItemPolicy { AbsoluteExpiration = new DateTimeOffset(DateTime.Now.AddSeconds(attr.CacheTime)) });
            return result;
        }


    }
}
