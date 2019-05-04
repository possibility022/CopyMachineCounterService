using CopyinfoWPF.Security.Attributes;

namespace CopyinfoWPF.Settings
{
    public class BasicSettings
    {
        [Encrypt]
        public string AsystentDatabase { get; set; }

        [Encrypt]
        public string CopyInfoDatabase { get; set; }
    }
}
