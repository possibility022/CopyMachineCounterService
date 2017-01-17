using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MimeKit;
using System.IO;

namespace Copyinfo.Other
{
    class EmailAttachment
    {
        MimeEntity attachment;
        public EmailAttachment(MimeKit.MimeEntity attachment)
        {
            
        }

        public void toFile()
        {
            using (var stream = File.Create("fileName"))
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
        }

        public void getAttachmentName()
        {
            attachment.ToString();
        }
    }
}
