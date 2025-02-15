namespace RestaurantAPI.Domain.Entities.Base
{
    public abstract class BaseEntity
    {
        public long Id {get;set;}
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
