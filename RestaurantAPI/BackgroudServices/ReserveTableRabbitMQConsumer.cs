using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RestaurantAPI.Domain.DTO.Messaging;
using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interface.Messaging;
using RestaurantAPI.Domain.Interface.Repository;
using RestaurantAPI.Domain.Interface.Services;

namespace RestaurantAPI.BackgroudServices
{
    public class ReserveTableRabbitMQConsumer : BackgroundService
    {
        private readonly RabbitMQSettings _rabbitMQSettings;
        private readonly IReservationService _reservationService;
        public ReserveTableRabbitMQConsumer(RabbitMQSettings rabbitMQSettings,
            IReservationService reservationService)
        {
            _rabbitMQSettings = rabbitMQSettings;
            _reservationService = reservationService;
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
                await _reservationService.CreateReserve(reserveTableMessage);
            };
            await channel.BasicConsumeAsync("queue_table_reservation", true, consumer);
        }
    }
}
