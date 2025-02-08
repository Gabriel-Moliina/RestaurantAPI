using RestaurantAPI.Domain.DTO.Messaging.Base;

namespace RestaurantAPI.Domain.Interface.Messaging
{
    public interface IRabbitMQSender
    {
        void SendMessage(BaseMessage message, string queueName);
    }
}
