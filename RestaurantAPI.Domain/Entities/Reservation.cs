using RestaurantAPI.Domain.Entities.Base;

namespace RestaurantAPI.Domain.Entities
{
    public class Reservation : BaseEntity<long>
    {
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public long TableId { get; set; }
        public virtual Table Table { get; set; }
    }
}
