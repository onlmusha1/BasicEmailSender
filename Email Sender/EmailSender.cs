using System;
using System.Net;
using System.Net.Mail;
using System.Security;
using System.Configuration;
using System.Configuration.Assemblies;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Email_Sender
{
    class EmailSender
    {

        public SmtpClient emailClient = new SmtpClient("smtp.gmail.com", 587);
        private MailMessage mailMessage;
        public EmailSender()
        {
            this.emailClient.UseDefaultCredentials = false;
            this.emailClient.Credentials = new NetworkCredential(
                ConfigurationManager.ConnectionStrings["gmail"].ProviderName,
                new NetworkCredential("", ConfigurationManager.ConnectionStrings["gmail"].ConnectionString).SecurePassword
                );
            this.emailClient.Timeout = 3000;
            this.emailClient.EnableSsl = true;
        }

        public void sendEmail(string body, string subject, string toEmail)
        {
            this.mailMessage = new MailMessage("lovetrumpshate6969@gmail.com", toEmail, subject, body);
            try
            {
                this.emailClient.SendMailAsync(this.mailMessage);
            }
            catch(Exception ex)
            {
                this.emailClient.Dispose();
                Console.WriteLine(ex.Message);
            }
        }

    }
}
