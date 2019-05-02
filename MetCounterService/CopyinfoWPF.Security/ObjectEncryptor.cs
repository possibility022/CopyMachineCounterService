using CopyinfoWPF.Common;
using CopyinfoWPF.Security.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CopyinfoWPF.Security
{
    public class ObjectEncryptor : IObjectEncryptor
    {

        IEncrypting _encrypting;
        IByteSerializer _byteSerializer;

        public ObjectEncryptor(IEncrypting encrypting, IByteSerializer byteSerializer)
        {
            _encrypting = encrypting;
            _byteSerializer = byteSerializer;
        }

        public T Decrypt<T>(string json) where T : new()
        {
            var jObject = JObject.Parse(json);
            var prop = GetPropertiesToEncrypt(typeof(T));

            var obj = new T();

            foreach(var p in prop)
            {
                if (p.CanRead && p.CanWrite)
                {
                    var bytes = (byte[])jObject[p.Name];
                    bytes = _encrypting.Unprotect(bytes);
                    var value = _byteSerializer.Deserialize(bytes, p.PropertyType);
                    p.SetValue(obj, value);
                }
            }

            return obj;
        }

        public string Encrypt<T>(T obj) where T : new()
        {
            var prop = GetPropertiesToEncrypt(typeof(T));
            var jObject = new JObject(obj);
            
            foreach(var p in prop)
            {
                if (p.CanRead && p.CanWrite)
                {
                    var value = p.GetValue(obj);
                    var bytes = _byteSerializer.Serialize(value);
                    bytes = _encrypting.Protect(bytes);

                    jObject[p.Name] = bytes;
                }
            }

            return jObject.ToString(Formatting.Indented);
        }

        public List<PropertyInfo> GetPropertiesToEncrypt(Type type)
        {
            var properties = type.GetProperties();

            var list = new List<PropertyInfo>();

            foreach(var p in properties)
            {
                var encrypt = p.CustomAttributes.Any(r => r.AttributeType == typeof(Encrypt));
                if (encrypt)
                    list.Add(p);
            }

            return list;
        }
    }
}
