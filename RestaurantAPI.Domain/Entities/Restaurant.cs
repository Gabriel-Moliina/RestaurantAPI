using RestaurantAPI.Domain.Entities.Base;

namespace RestaurantAPI.Domain.Entities
{
    public class Restaurant : BaseEntity<long>
    {
        public Restaurant()
        {
            Tables = new HashSet<Table>();
        }
        public string Name { get; set; }
        public bool Open { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public ICollection<Table> Tables { get; set; }
    }
}
