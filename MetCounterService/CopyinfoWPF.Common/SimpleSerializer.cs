using System;
using System.Text;

namespace CopyinfoWPF.Common
{
    public class SimpleSerializer : IByteSerializer
    {
        public object Deserialize(byte[] bytes, Type type)
        {
            if (type == typeof(string))
            {
                return Encoding.UTF8.GetString(bytes);
            }
            else if (type == typeof(int))
            {
                return BitConverter.ToInt32(bytes, 0);
            }
            else
                throw new NotSupportedException($"{type} is not supported.");

        }

        public byte[] Serialize(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            var type = obj.GetType();

            if (type == typeof(string))
            {
                string t = obj as string;
                return Encoding.UTF8.GetBytes(t);
            }
            else if (type == typeof(int))
            {
                int t = (int)obj;
                return BitConverter.GetBytes(t);
            }
            else
                throw new NotSupportedException($"Cannot convert {type} to bytes.");
        }
    }
}
