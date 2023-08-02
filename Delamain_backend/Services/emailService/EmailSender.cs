using System;
using System.Net;
using System.Net.Mail;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Delamain_backend.Services.emailService
{
	public class EmailSender : IEmailSender
	{
        private readonly IConfiguration _config;
        public EmailSender(IConfiguration config)
		{
            _config = config;
        }

        public void SendEmailAsync(string email, string subject, string message)
        {

            var Email = new MimeMessage();
            Email.From.Add(MailboxAddress.Parse("developthomas644@gmail.com"));
            Email.To.Add(MailboxAddress.Parse(email));
            Email.Subject = subject;
            Email.Body = new TextPart(TextFormat.Html) { Text = "A Request has been made to change your password if this wasn't you then ignore. " + Environment.NewLine + message };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
            smtp.Authenticate("developthomas644@gmail.com", "wabntrsdqfejmlis");
            smtp.Send(Email);
            smtp.Disconnect(true);

        }
    }
}

