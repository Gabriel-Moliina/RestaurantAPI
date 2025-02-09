using Microsoft.Extensions.Configuration;
using RestaurantAPI.Domain.Interface.Email;

namespace RestaurantAPI.Infra.Email.Email
{
    public class EmailSettings : IEmailSettings
    {
        public EmailSettings(IConfiguration configuration)
        {
            var emailSettings = configuration.GetSection("EmailSettings");
            Email = emailSettings["Email"];
            SMTP = emailSettings["Server"];
            Password = emailSettings["Password"];
            Port = int.Parse(emailSettings["Port"]);
            SSL = bool.Parse(emailSettings["SSL"]);
        }
        public string SMTP { get; }
        public int Port { get; }
        public string Email { get; }
        public string Password { get; }
        public bool SSL { get; }
    }
}
