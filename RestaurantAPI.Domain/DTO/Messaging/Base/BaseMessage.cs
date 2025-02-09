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
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
