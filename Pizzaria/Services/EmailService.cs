using Microsoft.Extensions.Options;
using Pizzaria.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Pizzaria.Services
{
    public class EmailService : IEmailService
    {
        private EmailServiceOptions _emailServiceOptions;
        public EmailService(IOptions<EmailServiceOptions> emailServiceOptions)
        {
            _emailServiceOptions = emailServiceOptions.Value;
        }

        public Task SendEmail(string recipient, string subject, string mailBody)
        {
            using (var client = new SmtpClient(_emailServiceOptions.MailServer, int.Parse(_emailServiceOptions.MailPort)))
            {
                if (bool.Parse(_emailServiceOptions.UseSSL) == true)
                {
                    client.EnableSsl = true;
                }

                if(!string.IsNullOrEmpty(_emailServiceOptions.UserId))
                {
                    client.Credentials = new NetworkCredential(_emailServiceOptions.UserId, _emailServiceOptions.Password);
                }

                client.Send(new MailMessage("noreply@pizza.dk", recipient, subject, mailBody));
            }

            return Task.CompletedTask;
        }
    }
}
