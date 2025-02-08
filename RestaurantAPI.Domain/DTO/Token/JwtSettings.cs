namespace RestaurantAPI.Domain.DTO.Token
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public int ExpirationInMinutes { get; set; }
    }
}
