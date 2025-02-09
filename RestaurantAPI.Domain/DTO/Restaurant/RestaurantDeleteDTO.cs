namespace RestaurantAPI.Domain.DTO.Restaurant
{
    public class RestaurantDeleteDTO
    {
        public RestaurantDeleteDTO(long id)
        {
            Id = id;
        }
        public long Id { get; }
    }
}
