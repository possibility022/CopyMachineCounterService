using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Copyinfo.Other
{
    class Email
    {
        private static string login_encrypted = "***REMOVED***";
        private static string password_encrypted = "***REMOVED***";
        private static string smtp_encrypted = "***REMOVED***";

        private static string smtp = "";
        private static string login = "";
        private static string password = "";
        private static string from = "";
        private static string subject = "Mail wysłany z programu CopyInfo!";

        public static void Initialize()
        {
            login = Security.Encrypting.AES_Decrypt(login_encrypted);
            password = Security.Encrypting.AES_Decrypt(password_encrypted);
            smtp = Security.Encrypting.AES_Decrypt(smtp_encrypted);
            from = login;
        }

        public static void SendEmail(string to, string message, string subject = "")
        {
            Task.Factory.StartNew(() => {

                try
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
                catch (Exception ex)
                { System.Windows.Forms.MessageBox.Show("Powstał jakiś problem z wysyłaniem maila. " + ex.Message); }

            });
        }
    }
}
