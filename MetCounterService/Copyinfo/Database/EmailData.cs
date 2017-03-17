using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;
using MimeKit;
using System.IO;

namespace Copyinfo.Database
{
    public class EmailData
    {
        public BsonBinaryData id { get; set; }
        public BsonArray mail { get; set; }
        public string from = "";
        public string subject = "";

        MimeMessage mes;

        private List<byte[]> emailmessage = null;
        private List<byte[]> emailheader = null;

        private string encoding = null;
        private string charset = null;

        public EmailData()
        {
            emailheader = new List<byte[]>();
            emailmessage = new List<byte[]>();
            charset = "iso-8859-2";
        }

        public string GetEmail()
        {
            return "Od: " + from + "\r\n" + "Temat: " + subject + "\r\n\r\n\r\n" + ReadEmail();
        }

        public void ParseUsingMime()
        {
            Stream stream = new MemoryStream();
            byte[] newline = Encoding.UTF8.GetBytes("\r\n");

            for (int i = 0; i < mail.AsBsonArray.Count; i++)
            {
                stream.Write(mail[i].AsByteArray, 0, mail[i].AsByteArray.Length);
                stream.Write(newline, 0, newline.Length);
            }
            stream.Flush();
            stream.Position = 0;

            mes = MimeKit.MimeMessage.Load(stream);

            from = mes.From[0].ToString();
            subject = mes.Subject;

            var parserOptions = new ParserOptions();
        }

        public void Parse()
        {
            ParseUsingMime();
            //emailheader = new List<byte[]>();
            //emailmessage = new List<byte[]>();

            //// Dla kazdej linijki tekstu: przekonwertuj na bytearray. Dodatkowo oddziel header od body
            //split_header_and_message();
            //setEncoding();
            //setCharset();
        }

        private string ReadEmail()
        {

            //string message = "";
            //for(int i = 0; i < emailmessage.Count; i++)
            //{
            //    byte[] bytes = get_bytes(i);
            //    message += convert_line(bytes) + "\r\n";
            //}

            //return message;

            return mes.TextBody;
        }

        public List<Other.EmailAttachment> GetAttachments()
        {
            List<Other.EmailAttachment> attachments = new List<Other.EmailAttachment>();

            foreach(var attachment in mes.Attachments)
            {
                Other.EmailAttachment att = new Other.EmailAttachment(attachment);
                attachments.Add(att);
            }

            return attachments;
        }

        private byte[] GetBytes(int index)
        {
            byte[] b = new byte[] { };
            switch(encoding)
            {
                case "8bit":
                    b = emailmessage[index];
                    break;
                default:
                    b = emailmessage[index];
                    break;
            }

            return b;
        }

        private string ConvertLine(byte[] line)
        {
            string text = null;
            Encoding en = Encoding.GetEncoding(charset);
            text = en.GetString(line);

            return text;
        }

        private void SplitHeaderAndBody()
        {
            bool isHeader = true;
            for (int i = 0; i < mail.AsBsonArray.Count; i++)
            {
                byte[] array = mail[i].AsByteArray;
                string line = System.Text.Encoding.Default.GetString(array);

                if (line.Length < 1)
                {
                    isHeader = false;
                }

                if (isHeader)
                    emailheader.Add(mail[i].AsByteArray);
                else
                    emailmessage.Add(mail[i].AsByteArray);
            }
        }

        private void SetEncoding()
        {
            foreach (byte[] bline in emailheader)
            {
                string line = System.Text.Encoding.Default.GetString(bline);
                if (line.Contains("Content-Transfer-Encoding:"))
                {
                    string value = line.Replace("Content-Transfer-Encoding:", "");
                    if (value.StartsWith(" "))
                        value = value.Remove(0, 1);
                    encoding = value;
                }
            }
        }

        private void SetCharset()
        {
            foreach(byte[] bline in emailheader)
            {
                string line = System.Text.Encoding.Default.GetString(bline);

                if (line.Contains("Content-Type:"))
                {
                    string value = line.Replace("Content-Type:", "");
                    if (value.StartsWith(" "))
                        value.Remove(0, 1);

                    string charsetprefix = "charset=";

                    if (value.Contains(charsetprefix))
                    {
                        int index = value.IndexOf(charsetprefix);
                        value = value.Remove(0, index);
                        value = value.Replace(charsetprefix, "");

                        charset = value.ToLower();
                    }
                }
            }
        }


    }
}
