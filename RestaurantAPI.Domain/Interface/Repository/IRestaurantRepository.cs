using RestaurantAPI.Domain.DTO.Restaurant;
using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interface.Repository.Base;

namespace RestaurantAPI.Domain.Interface.Repository
{
    public interface IRestaurantRepository : IBaseRepository<Restaurant>
    {
        Task<List<Restaurant>> GetByUserId(long userId);
        Task<bool> ExistsByNameAndUserId(string name, long userId);
        Task<bool> ExistsWithDiffId(string name, long userId, long restaurantId);
        Task<Restaurant> GetByIdAndUserId(long id, long userId);
        Task<bool> ExistsByIdAndUserId(long id, long userId);
    }
}
