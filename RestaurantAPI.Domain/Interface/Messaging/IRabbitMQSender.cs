using RestaurantAPI.Domain.DTO.Messaging.Base;

namespace RestaurantAPI.Domain.Interface.Messaging
{
    public interface IRabbitMQSender
    {
        Task SendMessage<T>(BaseMessage message) where T: BaseMessage;
    }
}
