namespace CopyinfoWPF.Security
{
    public interface IObjectEncryptor
    {
        string Encrypt<T>(T obj) where T : new();
        T Decrypt<T>(string json) where T : new();
    }
}
