using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copyinfo.Database.LocalCache
{
    interface ICache
    {
        void Add(int key, object value);
        void UpdateMany(object[] items);
        bool Contains(int key);
        object Get(int key);
        void Remove(int key);
        void Set(int key, object value);
        void Preload();
    }
}
