using RestaurantAPI.Domain.Entities.Base;

namespace RestaurantAPI.Domain.Entities
{
    public class Restaurant : BaseEntity
    {
        public Restaurant()
        {
            Tables = new HashSet<Table>();
        }
        public string Name { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public ICollection<Table> Tables { get; set; }
    }
}
