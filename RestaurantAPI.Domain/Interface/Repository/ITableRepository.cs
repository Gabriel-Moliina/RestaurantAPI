using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interface.Repository.Base;

namespace RestaurantAPI.Domain.Interface.Repository
{
    public interface ITableRepository : IBaseRepository<Table>
    {
        Task<Table> Exists(string identification, long restaurantId);
        Task<List<Table>> GetByRestaurantId(long restaurantId);
    }
}
