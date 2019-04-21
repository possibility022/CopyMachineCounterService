using MimeKit;
using System;
using System.IO;
using System.Text;

namespace CopyinfoWPF.Workflows.Email
{
    public class EmailMessage
    {
        private const string SourceEmailNewLine = "\n";
        private const string DoubleSourceEmailNewLine = "\n\n";
        private readonly string WindowsNewLine = Environment.NewLine;

        public EmailMessage(byte[] content)
        {
            if (content == null)
                return;

            using (Stream stream = new MemoryStream())
            {
                stream.Write(content, 0, content.Length);
                stream.Flush();
                stream.Position = 0;

                var message = MimeMessage.Load(stream);

                From = message.From[0].ToString();
                Subject = message.Subject;
                To = message.To.ToString();
                TextBody = FormatContent(message.TextBody);
            }
        }

        public string From { get; private set; } = string.Empty;
        public string To { get; private set; } = string.Empty;
        public string Subject { get; private set; } = string.Empty;

        public string TextBody { get; private set; } = string.Empty;
        
        private string FormatContent(string content)
        {
            var sb = new StringBuilder(content);
            
            if (!content.Contains(WindowsNewLine))
            {
                sb = sb.Replace(DoubleSourceEmailNewLine, WindowsNewLine);
                sb = sb.Replace(SourceEmailNewLine, WindowsNewLine);
            }

            return sb.ToString();
        }
    }
}
