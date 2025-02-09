using System.Text.Json.Serialization;

namespace RestaurantAPI.Domain.DTO.Messaging.Base
{
    public class BaseMessage
    {
        public BaseMessage(string queueName)
        {
            QueueName = queueName;
        }
        [JsonIgnore]
        public string QueueName { get; }
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
