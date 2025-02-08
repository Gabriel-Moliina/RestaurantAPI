using RestaurantAPI.Domain.DTO.Messaging.Base;

namespace RestaurantAPI.Domain.DTO.Messaging
{
    public class ReserveTableMessage : BaseMessage
    {
        public string Email { get; set; }
        public DateTime Date { get; set; }
    }
}
