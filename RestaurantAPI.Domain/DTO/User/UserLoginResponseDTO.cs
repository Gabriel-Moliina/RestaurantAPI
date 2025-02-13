using RestaurantAPI.Domain.DTO.Restaurant;

namespace RestaurantAPI.Domain.DTO.User
{
    public class UserLoginResponseDTO
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
