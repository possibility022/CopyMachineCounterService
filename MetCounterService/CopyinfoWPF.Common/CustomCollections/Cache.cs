using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace CopyinfoWPF.Common.CustomCollections
{
    public class Cache<T1, T2> : ICache<T1, T2>, IConditionalCache<T1, T2>
    {

        private readonly ConcurrentDictionary<T1, T2> concurrentDictionary = new ConcurrentDictionary<T1, T2>();

        public void Add(T1 key, T2 value)
        {
            concurrentDictionary.AddOrUpdate(key, value, (f1, f2) => f2);
        }

        public bool Add(T1 key, T2 value, Func<T1, bool> addCondition)
        {
            if (addCondition(key))
            {
                Add(key, value);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Contains(T1 key)
        {
            return concurrentDictionary.ContainsKey(key);
        }

        public T2 Get(T1 key)
        {
            var taken = concurrentDictionary.TryGetValue(key, out var val);
            return val;
        }

        public void Remove(T1 key)
        {
            concurrentDictionary.TryRemove(key, out var _);
        }

        public void UpdateMany(Func<T2, T1> key, IEnumerable<T2> items)
        {
            foreach (var item in items)
            {
                AddOrUpdate(key, item);
            }
        }

        private void AddOrUpdate(Func<T2, T1> key, T2 item)
        {
            concurrentDictionary.AddOrUpdate(key(item), item, (f1, f2) => f2);
        }

        public void  UpdateMany(Func<T2, T1> key, IEnumerable<T2> items, Func<T1, bool> addCondition)
        {
            foreach(var item in items)
            {
                if (addCondition(key(item)))
                    AddOrUpdate(key, item);
            }
        }
    }
}
