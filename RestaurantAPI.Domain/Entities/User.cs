using RestaurantAPI.Domain.Entities.Base;

namespace RestaurantAPI.Domain.Entities
{
    public class User : BaseEntity
    {
        public User()
        {
            Restaurants = new HashSet<Restaurant>();
        }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Restaurant> Restaurants { get; set; }
    }
}
