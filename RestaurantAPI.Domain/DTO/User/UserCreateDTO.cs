namespace RestaurantAPI.Domain.DTO.User
{
    public class UserCreateDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
