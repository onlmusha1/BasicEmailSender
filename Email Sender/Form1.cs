using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Windows.Forms;

namespace Email_Sender
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private EmailHandler emailHandler = new EmailHandler();

        private void btnSend_Click(object sender, EventArgs e)
        {
            this.emailHandler.addEmailToQueue(new Email(this.txtBody.Text, this.txtSubject.Text, this.txtSendTo.Text));
            
        }
        
    }
    internal class EmailHandler
    {
        private EmailSender email = new EmailSender();
        private List<Email> emailQueue = new List<Email>();
        public EmailHandler()
        {
            this.email.emailClient.SendCompleted += MessageSent;
        }

        public void addEmailToQueue(Email email)
        {
            this.emailQueue.Add(email);
            if (this.emailQueue.Count > 0)
            {
                this.trySendEmail();
            }
        }

        private void trySendEmail()
        {
            if (this.emailQueue.Count > 0)
            {
                this.email.sendEmail(this.emailQueue[0].Body, this.emailQueue[0].Subject, this.emailQueue[0].ToAddress);
            }
        }
        private void MessageSent(object sender, EventArgs e)
        {
            this.emailQueue.RemoveAt(0);
            if(this.emailQueue.Count > 0)
            {
                this.trySendEmail();
            }
        }
    }

    internal class Email
    {
        private string body;
        private string subject;
        private string toAddress;

        public string Body { get => body; set => body = value; }
        public string Subject { get => subject; set => subject = value; }
        public string ToAddress { get => toAddress; set => toAddress = value; }

        public Email(string body, string subject, string toAddress)
        {
            this.Body = body;
            this.Subject = subject;
            this.ToAddress = toAddress;
        }
    }
}
