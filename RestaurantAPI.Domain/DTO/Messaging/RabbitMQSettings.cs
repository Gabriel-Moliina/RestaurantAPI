using Microsoft.Extensions.Configuration;

namespace RestaurantAPI.Domain.DTO.Messaging
{
    public class RabbitMQSettings
    {
        public RabbitMQSettings(IConfiguration configuration)
        {
            HostName = configuration["RabbitMQSettings:HostName"];
            UserName = configuration["RabbitMQSettings:UserName"];
            Password = configuration["RabbitMQSettings:Password"];

        }
        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
