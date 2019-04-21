using System;
using System.Collections.Generic;

namespace CopyinfoWPF.Common.CustomCollections
{
    public interface ICache<T1, T2>
    {
        void Add(T1 key, T2 value);
        void UpdateMany(Func<T2, T1> key, IEnumerable<T2> items);
        bool Contains(T1 key);
        T2 Get(T1 key);
        void Remove(T1 key);
    }
}
