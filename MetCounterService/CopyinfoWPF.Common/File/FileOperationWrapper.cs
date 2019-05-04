namespace CopyinfoWPF.Common.File
{
    public class FileOperationWrapper : IFileOperation
    {
        public bool Exists(string path) => System.IO.File.Exists(path);

        public string ReadAllText(string path) => System.IO.File.ReadAllText(path);

        public void WriteAllText(string path, string content) => System.IO.File.WriteAllText(path, content);

        public void Delete(string path) => System.IO.File.Delete(path);
    }
}
