using RestaurantAPI.Domain.DTO.Messaging.Base;

namespace RestaurantAPI.Domain.DTO.Messaging
{
    public class EmailDTO : BaseMessage
    {
        public const string QueueName = "queue_email_reservation";
        public EmailDTO(string subject, string receiver, string message) : base(QueueName)
        {
            Subject = subject;
            Receiver = receiver;
            Message = message;
        }

        public string Subject { get; set; }
        public string Receiver { get; set; }
        public string Message { get; set; }
    }
}
