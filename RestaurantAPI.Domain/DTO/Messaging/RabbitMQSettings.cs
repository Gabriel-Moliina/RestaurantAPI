using Microsoft.Extensions.Configuration;
using RestaurantAPI.Domain.Interface.Messaging;

namespace RestaurantAPI.Domain.DTO.Messaging
{
    public class RabbitMQSettings : IRabbitMQSettings
    {
        public RabbitMQSettings(IConfiguration configuration)
        {
            HostName = configuration["RabbitMQSettings:HostName"];
            UserName = configuration["RabbitMQSettings:UserName"];
            Password = configuration["RabbitMQSettings:Password"];

        }
        public string HostName { get; }
        public string UserName { get; }
        public string Password { get; }
    }
}
