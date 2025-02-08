using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using RestaurantAPI.Email.Email;

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
            new SmtpClient(_emailSettings.SMTP, _emailSettings.Port).Send(
                _emailSettings.Email,
                receiver,
                subject,
                body
            );
        }
    }
}
