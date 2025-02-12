using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interface.Repository.Base;

namespace RestaurantAPI.Domain.Interface.Repository
{
    public interface ITableRepository : IBaseRepository<Table>
    {
        Task<List<Table>> GetByRestaurantId(long restaurantId);
        Task<Table> GetByIdentificationRestaurant(string identification, long restaurantId);
        Task<Table> GetByIdentificationRestaurantWithDiffId(string identification, long restaurantId, long tableId);
    }
}
