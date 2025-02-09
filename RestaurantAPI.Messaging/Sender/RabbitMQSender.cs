using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RestaurantAPI.Domain.DTO.Messaging.Base;
using RestaurantAPI.Domain.Interface.Messaging;

namespace RestaurantAPI.Messaging.Sender
{
    public class RabbitMQSender : IRabbitMQSender 
    {
        private IRabbitMQSettings _rabbitMQSettings;
        public RabbitMQSender(IConfiguration configuration, IRabbitMQSettings rabbitMQSettings)
        {
            _rabbitMQSettings = rabbitMQSettings;
        }

        public async Task SendMessage<T>(BaseMessage message) where T : BaseMessage
        {
            var factory = new ConnectionFactory
            {
                HostName = _rabbitMQSettings.HostName,
                UserName = _rabbitMQSettings.UserName,
                Password = _rabbitMQSettings.Password
            };

            var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();
            await channel.QueueDeclareAsync(queue: message.QueueName, false, false, false, arguments: null);
            byte[] body = GetMessageAsByteArray<T>(message);
            await channel.BasicPublishAsync(exchange: "", routingKey: message.QueueName, body);
        }


        private byte[] GetMessageAsByteArray<T>(BaseMessage message) where T : BaseMessage
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            var json = JsonSerializer.Serialize((T)message, options);
            var body = Encoding.UTF8.GetBytes(json);
            return body;
        }
    }
}
