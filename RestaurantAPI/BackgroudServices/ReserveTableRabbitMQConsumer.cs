using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RestaurantAPI.Domain.DTO.Messaging;
using RestaurantAPI.Domain.Interface.Services;
using RestaurantAPI.Email.Sender;

namespace RestaurantAPI.BackgroudServices
{
    public class ReserveTableRabbitMQConsumer : BackgroundService
    {
        private readonly RabbitMQSettings _rabbitMQSettings;
        private readonly EmailSender _emailSender;
        public ReserveTableRabbitMQConsumer(RabbitMQSettings rabbitMQSettings,
            EmailSender emailSender)
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
            await channel.QueueDeclareAsync(queue: "queue_table_reservation", false, false, false, arguments: null);

            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (channel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                ReserveTableMessage reserveTableMessage = JsonSerializer.Deserialize<ReserveTableMessage>(content);
                await CreateReserve(reserveTableMessage);
            };
            await channel.BasicConsumeAsync("queue_table_reservation", true, consumer);
        }

        public Task CreateReserve(ReserveTableMessage reserveMessage)
        {
            _emailSender.SendEmail(reserveMessage.Email, "teste", "teste");
            return Task.CompletedTask;
        }
    }
}
