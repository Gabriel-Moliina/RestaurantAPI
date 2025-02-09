using System.Security.Authentication;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using RestaurantAPI.Domain.DTO.Messaging;
using RestaurantAPI.Domain.Interface.Email;

namespace RestaurantAPI.Infra.Email.Sender
{
    public class EmailSender : IEmailSender
    {
        private readonly IEmailSettings _emailSettings;
        public EmailSender(IEmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public async Task SendEmail(EmailDTO email)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(email.Name, _emailSettings.Email));

            message.To.Add(MailboxAddress.Parse(email.Receiver));
            message.Subject = email.Subject;
            message.Body = new TextPart("html")
            {
                Text = email.Message
            };

            using var client = new SmtpClient();
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;

            await client.ConnectAsync(_emailSettings.SMTP,
                                      _emailSettings.Port,
                                      _emailSettings.SSL);

            await client.AuthenticateAsync(_emailSettings.Email, _emailSettings.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
