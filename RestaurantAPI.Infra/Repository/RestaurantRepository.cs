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

        public async Task<Restaurant> GetByIdAndUserId(long id, long userId) =>
            await _dbSet.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

        public async Task<List<Restaurant>> GetByUserId(long userId) => 
            await _dbSet.Where(e => e.UserId == userId)
                .ToListAsync();

        public async Task<bool> ExistsByNameAndUserId(string name, long userId) => await _dbSet.AnyAsync(e => e.Name == name && e.UserId == userId);
        public async Task<bool> ExistsWithDiffId(string name, long userId, long restaurantId) => await _dbSet.AnyAsync(e => e.Name == name && e.UserId == userId && e.Id != restaurantId);
        public async Task<bool> ExistsByIdAndUserId(long id, long userId) => await _dbSet.AnyAsync(e => e.Id == id && e.UserId == userId);
    }
}
