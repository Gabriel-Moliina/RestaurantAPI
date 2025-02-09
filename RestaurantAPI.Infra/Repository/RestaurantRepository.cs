using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interface.Repository;
using RestaurantAPI.Infra.Context;
using RestaurantAPI.Infra.Repository.Base;

namespace RestaurantAPI.Infra.Repository
{
    public class RestaurantRepository : BaseRepository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(EntityContext context) : base(context)
        {
        }

        public override async Task<List<Restaurant>> Get() => await _dbSet.AsNoTracking().Include(e => e.Tables).ToListAsync();

        public override async Task<Restaurant> GetById(long restaurantId) => 
            await _dbSet.Where(e => e.Id == restaurantId)
                .Include(e => e.Tables)
                .FirstOrDefaultAsync();

        public async Task<List<Restaurant>> GetByUserId(long userId) => 
            await _dbSet.Where(e => e.UserId == userId)
                .Include(e => e.Tables)
                .ToListAsync();

        public async Task<bool> Exists(string name, long userId) => await _dbSet.AnyAsync(e => e.Name == name && e.UserId == userId);
    }
}
