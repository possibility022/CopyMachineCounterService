using System;
using System.Collections.Generic;

namespace CopyinfoWPF.Common.CustomCollections
{
    public interface IConditionalCache<T1, T2> : ICache<T1, T2>
    {
        void UpdateMany(Func<T2, T1> key, IEnumerable<T2> items, Func<T1, bool> addCondition);

        bool Add(T1 key, T2 value, Func<T1, bool> addCondition);
    }
}
