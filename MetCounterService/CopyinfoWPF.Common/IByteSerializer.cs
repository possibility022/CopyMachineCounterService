using System;

namespace CopyinfoWPF.Common
{
    public interface IByteSerializer
    {
        byte[] Serialize(object obj);

        object Deserialize(byte[] bytes, Type type);
    }
}
