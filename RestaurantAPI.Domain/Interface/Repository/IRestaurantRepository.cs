using RestaurantAPI.Domain.DTO.Restaurant;
using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interface.Repository.Base;

namespace RestaurantAPI.Domain.Interface.Repository
{
    public interface IRestaurantRepository : IBaseRepository<Restaurant>
    {
        Task<List<Restaurant>> GetByUserId(long userId);
        Task<bool> Exists(string name, long userId);
        Task<bool> ExistsWithDiffId(string name, long userId, long restaurantId);
    }
}
