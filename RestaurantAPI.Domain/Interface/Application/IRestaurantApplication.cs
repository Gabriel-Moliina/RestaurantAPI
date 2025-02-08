using RestaurantAPI.Domain.DTO.Restaurant;

namespace RestaurantAPI.Domain.Interface.Application
{
    public interface IRestaurantApplication
    {
        Task<List<RestaurantDTO>> Get();
        Task<RestaurantDTO> GetById(long restaurantId);
        Task<List<RestaurantDTO>> GetByUserId(long userId);
        Task<RestaurantDTO> Create(RestaurantCreateDTO dto);
        Task<RestaurantDTO> DeleteById(long id);
    }
}
