using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interface.Repository;
using RestaurantAPI.Infra.Context;
using RestaurantAPI.Infra.Repository.Base;

namespace RestaurantAPI.Infra.Repository
{
    public class TableRepository : BaseRepository<Table>, ITableRepository
    {
        public TableRepository(EntityContext context) : base(context)
        {
        }

        public async Task<bool> Exists(string identification, long restaurantId) => await _dbSet.AnyAsync(e => e.Identification == identification && e.RestaurantId == restaurantId);
    }
}
