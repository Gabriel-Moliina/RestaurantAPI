using RestaurantAPI.Domain.Entities.Base;

namespace RestaurantAPI.Domain.Entities
{
    public class Table : BaseEntity
    {
        public string Identification { get; set; }
        public int Capacity { get; set; }
        public bool Free { get; set; }
        public long RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual Reservation Reservation { get; set; }
    }
}
