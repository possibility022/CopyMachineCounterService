namespace CopyinfoWPF.Common.File
{
    public interface IFileOperation
    {
        string ReadAllText(string path);
        bool Exists(string path);

        void WriteAllText(string path, string content);
        void Delete(string settingsPath);
    }
}
