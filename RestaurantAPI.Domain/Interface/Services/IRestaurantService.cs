using RestaurantAPI.Domain.DTO.Restaurant;

namespace RestaurantAPI.Domain.Interface.Services
{
    public interface IRestaurantService
    {
        Task<List<RestaurantDTO>> GetByUserId(long id);
        Task<RestaurantDTO> GetByIdAndUserId(long restaurantId, long userId);
        Task<RestaurantDTO> SaveOrUpdate(RestaurantSaveDTO dto);
        Task<RestaurantDTO> DeleteById(long id);
    }
}
