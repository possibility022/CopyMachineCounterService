using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MimeKit;
using System.IO;

namespace Copyinfo.Other
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

        public string getFile()
        {
            using (var stream = File.Create(getAttachmentName()))
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

            return getAttachmentName();
        }

        public string getAttachmentName()
        {
            return fileName;
        }
    }
}
