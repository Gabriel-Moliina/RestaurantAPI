using RestaurantAPI.Domain.DTO.Restaurant;

namespace RestaurantAPI.Domain.DTO.User
{
    public class UserLoginResponseDTO
    {
        public long Id { get; set; }
        public List<RestaurantDTO> Restaurants { get; set; }
    }
}
