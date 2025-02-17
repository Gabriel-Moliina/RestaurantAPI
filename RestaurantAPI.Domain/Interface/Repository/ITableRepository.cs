using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interface.Repository.Base;

namespace RestaurantAPI.Domain.Interface.Repository
{
    public interface ITableRepository : IBaseRepository<Table>
    {
        Task<Table> GetByIdAndUserId(long id, long userId);
        Task<List<Table>> GetByRestaurantIdAndUserId(long restaurantId, long userId);
        Task<bool> ExistsByIdentificationRestaurantAndUserId(string identification, long restaurantId, long userId);
        Task<bool> ExistsByIdentificationRestaurantAndUserIdWithDiffId(string identification, long restaurantId, long tableId, long userId);
        Task<bool> ExistsByIdAndUserId(long id, long userId);
        Task<bool> ExistsByIdAndUserId(long id, long restaurantId, long userId);
    }
}
