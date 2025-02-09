using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RestaurantAPI.Domain.DTO.Messaging;
using RestaurantAPI.Domain.Interface.Email;
using RestaurantAPI.Domain.Interface.Messaging;

namespace RestaurantAPI.BackgroudServices
{
    public class TableReserveEmailConsumer : BackgroundService
    {
        private readonly IRabbitMQSettings _rabbitMQSettings;
        private readonly IEmailSender _emailSender;
        public TableReserveEmailConsumer(IRabbitMQSettings rabbitMQSettings,
            IEmailSender emailSender)
        {
            _rabbitMQSettings = rabbitMQSettings;
            _emailSender = emailSender;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory
            {
                HostName = _rabbitMQSettings.HostName,
                UserName = _rabbitMQSettings.UserName,
                Password = _rabbitMQSettings.Password
            };

            var connection = await factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();
            await channel.QueueDeclareAsync(queue: EmailDTO.QueueNameEmail, false, false, false, arguments: null);

            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (channel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                EmailDTO tableReserveEmail = JsonSerializer.Deserialize<EmailDTO>(content);
                await _emailSender.SendEmail(tableReserveEmail);
            };
            await channel.BasicConsumeAsync(EmailDTO.QueueNameEmail, true, consumer);
        }
    }
}
