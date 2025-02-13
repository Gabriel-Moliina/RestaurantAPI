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

        public async Task<List<Restaurant>> GetByUserId(long userId) => 
            await _dbSet.Where(e => e.UserId == userId)
                .ToListAsync();

        public async Task<bool> Exists(string name, long userId) => await _dbSet.AnyAsync(e => e.Name == name && e.UserId == userId);
    }
}
