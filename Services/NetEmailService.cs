using Alkemy.Models.Common;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Alkemy.Services
{
    public class NetEmailService : IEmailService
    {
        private readonly EmailClient _client;

        public NetEmailService(IOptions<AppSettings> settings)
        {
            _client = settings.Value.EmailClient;
        }
        public void Send(EmailContent emailContent)
        {
            var client = new SmtpClient(_client.Host, _client.Port)
            {
                Credentials = new NetworkCredential(_client.Email, _client.Password),
                EnableSsl = true,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
            MailMessage mail = new();
            mail.From = new MailAddress(_client.Email, emailContent.DisplayName);
            mail.To.Add(new MailAddress(emailContent.EmailReceptor));
            mail.Subject = emailContent.Subject;
            mail.Body = emailContent.Body;
            mail.IsBodyHtml = emailContent.IsBodyHtml;
            client.Send(mail);
            client.Dispose();
        }

        public Task SendAsync(EmailContent emailContent)
        {
            return Task.Run(() =>
            {
                Send(emailContent);

            });
        }
    }
}
