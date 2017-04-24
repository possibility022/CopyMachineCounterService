using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copyinfo.Database.LocalCache
{
    class CacheClients : ICache
    {
        static private Dictionary<int, Client> _cache = new Dictionary<int, Client>();
        private object @lock = new object();

        public void Add(int key, object value)
        {
            _cache.Add(key, (Client)value);
        }

        public bool Contains(int key)
        {
            return _cache.ContainsKey(key);
        }

        public void Flush()
        {
            _cache.Clear();
        }

        public object Get(int key)
        {
            return _cache[key];
        }

        public void Preload()
        {
            this.UpdateMany(Firebird.GetAllClients().ToArray());
        }

        public void Remove(int key)
        {
            _cache.Remove(key);
        }

        public void Set(int key, object value)
        {
            _cache[key] = (Client)value;
        }

        public void UpdateMany(object[] list)
        {
            foreach(Client c in list)
            {
                if (_cache.ContainsKey(c.id))
                    _cache[c.id] = c;
                else
                    _cache.Add(c.id, c);
            }
        }
    }
}
