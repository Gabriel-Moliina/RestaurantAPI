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
        
        public override async Task<Table> GetById(long id) =>
            await _dbSet.Where(e => e.Id == id).Include(e => e.Restaurant).FirstOrDefaultAsync();

        public async Task<List<Table>> GetByRestaurantId(long restaurantId) =>
            await _dbSet.Where(e => e.RestaurantId == restaurantId).ToListAsync();

        public async Task<Table> Exists(string identification, long restaurantId) =>
            await _dbSet.FirstOrDefaultAsync(e => e.Identification == identification && e.RestaurantId == restaurantId);
    }
}
