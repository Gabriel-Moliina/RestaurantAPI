using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using MimeKit;
using RestaurantAPI.Email.Email;
using MailKit.Net.Smtp;

namespace RestaurantAPI.Email.Sender
{
    public class EmailSender
    {
        private readonly EmailSettings _emailSettings;
        public EmailSender(IConfiguration configuration)
        {
            var emailSettings = configuration.GetSection("EmailSettings");

            _emailSettings = new()
            {
                Email = emailSettings["Email"],
                SMTP = emailSettings["Server"],
                Password = emailSettings["Password"],
                Port = int.Parse(emailSettings["Port"]),
                SSL = bool.Parse(emailSettings["SSL"])
            };
        }


        public void SendEmail(string receiver, string subject, string body)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(_smtpSettings.SenderName,
                                                _smtpSettings.SenderEmail));
            message.To.Add(new MailboxAddress("destino", email));
            message.Subject = subject;
            message.Body = new TextPart("html")
            {
                Text = body
            };

            using (var client = new System.Net.Mail.SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                if (_env.IsDevelopment())
                {
                    await client.ConnectAsync(_smtpSettings.Server,
                                              _smtpSettings.Port, true);
                }
                else
                {
                    await client.ConnectAsync(_smtpSettings.Server);
                }

                await client.AuthenticateAsync(_smtpSettings.Username,
                                               _smtpSettings.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
