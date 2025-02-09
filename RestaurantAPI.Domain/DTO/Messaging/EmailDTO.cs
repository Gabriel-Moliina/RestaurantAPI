using RestaurantAPI.Domain.DTO.Messaging.Base;

namespace RestaurantAPI.Domain.DTO.Messaging
{
    public class EmailDTO : BaseMessage
    {
        public const string QueueNameEmail = "queue_email_reservation";
        public EmailDTO() : base(QueueNameEmail)
        {
        }

        public string Name { get; set; }
        public string Subject { get; set; }
        public string Receiver { get; set; }
        public string Message { get; set; }
    }
}
