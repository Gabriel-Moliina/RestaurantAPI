using RestaurantAPI.Domain.DTO.Restaurant;

namespace RestaurantAPI.Domain.Interface.Application
{
    public interface IRestaurantApplication
    {
        Task<List<RestaurantDTO>> Get();
        Task<RestaurantDTO> GetById(long restaurantId);
        Task<RestaurantDTO> SaveOrUpdate(RestaurantCreateDTO dto);
        Task<RestaurantDTO> DeleteById(long id);
    }
}
