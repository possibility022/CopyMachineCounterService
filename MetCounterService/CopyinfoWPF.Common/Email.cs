using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CopyinfoWPF.Common
{
    public class Email
    {
        private static string smtp = "";
        private static string login = "";
        private static string password = "";
        private static string from = "";
        private static string subject = "Mail wysłany z programu CopyInfo!";

        public static void Initialize(string login, string password, string smtp)
        {
            Email.login = login;
            Email.password = password;
            Email.smtp = smtp;
            from = login;
        }

        public static void SendEmail(string to, string message, string subject = "")
        {
            MailMessage mail = new MailMessage(from, to);
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(login, password);
            client.Host = smtp;
            mail.Subject = subject == "" ? Email.subject : subject;
            mail.Body = message;
            client.Send(mail);
        }
    }
}
