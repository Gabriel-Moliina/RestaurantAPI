using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RestaurantAPI.Domain.DTO.Messaging;
using RestaurantAPI.Domain.DTO.Messaging.Base;
using RestaurantAPI.Domain.Interface.Messaging;

namespace RestaurantAPI.Messaging.Sender
{
    public class RabbitMQSender : IRabbitMQSender
    {
        private RabbitMQSettings _rabbitMQSettings;
        public RabbitMQSender(IConfiguration configuration, RabbitMQSettings rabbitMQSettings)
        {
            _rabbitMQSettings = rabbitMQSettings;
        }

        public async void SendMessage(BaseMessage message, string queueName)
        {
            var factory = new ConnectionFactory
            {
                HostName = _rabbitMQSettings.HostName,
                UserName = _rabbitMQSettings.UserName,
                Password = _rabbitMQSettings.Password
            };

            var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();
            await channel.QueueDeclareAsync(queue: queueName, false, false, false, arguments: null);
            byte[] body = GetMessageAsByteArray(message);
            await channel.BasicPublishAsync(exchange: "", routingKey: queueName, body);
        }


        private byte[] GetMessageAsByteArray(BaseMessage message)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            var json = JsonSerializer.Serialize((ReserveTableMessage)message, options);
            var body = Encoding.UTF8.GetBytes(json);
            return body;
        }
    }
}
