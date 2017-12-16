using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyinfoWPF.Database.LocalCache
{
    class CacheDevices : ICache
    {
        static List<Device> _cacheList = new List<Device>();

        private object @lock = new object();

        public void Add(int key, object value)
        {
            _cacheList.Add((Device)value);
        }

        public bool Contains(int key)
        {
            return _cacheList.Where(p => p.id == key).Count() > 0;
        }

        public object Get(int key)
        {
            return _cacheList.Where(p => p.id == key).First();
        }

        public void Preload()
        {
            UpdateMany(Firebird.GetAllDevices().ToArray());
        }

        public void Remove(int key)
        {
            Device dev = _cacheList.Where(p => p.id == key).First();
            _cacheList.Remove(dev);
        }

        public void Set(int key, object o)
        {
            Remove(key);
            Add(key, o);
        }

        public void UpdateMany(object[] items)
        {
            foreach (Device dev in items)
            {
                IEnumerable<Device> devicesInCache = _cacheList.Where(p => p.id == dev.id);
                if (devicesInCache.Count() > 0)
                {
                    Set(dev.id, dev);
                }
                else
                {
                    Add(dev.id, dev);
                }

            }
        }

        public List<Device> GetByClientID(int client_id)
        {
            return _cacheList.Where(p => p.client_id == client_id).ToList();
        }

        public Device GetBySerialNumber(string serialNumber)
        {
            IEnumerable<Device> list = _cacheList.Where(p => p.serial_number == serialNumber);
            if (list.Count() > 0)
                return list.First();
            else
                return null;
        }
    }
}
