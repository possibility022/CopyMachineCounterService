using MimeKit;
using System;
using System.IO;
using System.Text;

namespace CopyinfoWPF.Workflows.Email
{
    public class MimeReader
    {
        private const string SourceEmailNewLine = "\n";

        public MimeReader() { }

        public MimeReader(byte[] content)
        {
            DeserializeEmail(content);
        }

        public string From { get; private set; }
        public string To { get; private set; }
        public string Subject { get; private set; }

        public string TextBody { get; private set; }

        public void DeserializeEmail(byte[] content)
        {
            Stream stream = new MemoryStream();

            stream.Write(content, 0, content.Length);
            stream.Flush();
            stream.Position = 0;

            var message = MimeMessage.Load(stream);

            From = message.From[0].ToString();
            Subject = message.Subject;
            To = message.To.ToString();
            TextBody = FormatContent(message.TextBody);
        }

        private string FormatContent(string content)
        {
            var sb = new StringBuilder(content);
            sb.Replace(SourceEmailNewLine, Environment.NewLine);
            return sb.ToString();
        }
    }
}
