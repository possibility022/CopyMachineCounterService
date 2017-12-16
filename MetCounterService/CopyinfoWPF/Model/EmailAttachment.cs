using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyinfoWPF.Model
{
    public class EmailAttachment
    {
        MimeEntity attachment;
        string fileName = "FileNameNotFound";

        public EmailAttachment(MimeKit.MimeEntity attachment)
        {
            this.attachment = attachment;

            if (attachment is MimePart)
            {
                MimePart tmp = (MimePart)attachment;
                fileName = tmp.FileName;
            }
        }

        public string GetFile()
        {
            using (var stream = File.Create(GetAttachmentName()))
            {
                if (attachment is MessagePart)
                {
                    var part = (MessagePart)attachment;
                    part.Message.WriteTo(stream);
                }
                else
                {
                    var part = (MimePart)attachment;
                    part.ContentObject.DecodeTo(stream);
                }
            }

            return GetAttachmentName();
        }

        public string GetAttachmentName()
        {
            return fileName;
        }
    }
}
