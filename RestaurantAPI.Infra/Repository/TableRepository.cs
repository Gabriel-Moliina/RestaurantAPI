using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interface.Repository;
using RestaurantAPI.Domain.Interface.Token;
using RestaurantAPI.Infra.Context;
using RestaurantAPI.Infra.Repository.Base;

namespace RestaurantAPI.Infra.Repository
{
    public class TableRepository : BaseRepository<Table>, ITableRepository
    {
        public TableRepository(EntityContext context, ITokenService tokenService) : base(context)
        {
        }

        public override async Task<Table> GetById(long id) => await _dbSet.Where(e => e.Id == id).Include(e => e.Restaurant).FirstOrDefaultAsync();

        public async Task<Table> GetByIdAndUserId(long id, long userId) =>
            await _dbSet.Where(e => e.Id == id && e.Restaurant.UserId == userId).Include(e => e.Restaurant).Include(e => e.Reservation).FirstOrDefaultAsync();

        public async Task<List<Table>> GetByRestaurantIdAndUserId(long restaurantId, long userId) =>
            await _dbSet.Where(e => e.RestaurantId == restaurantId && e.Restaurant.UserId == userId).Include(e => e.Reservation).ToListAsync();

        public async Task<Table> GetByIdentificationRestaurantAndUserId(string identification, long restaurantId, long userId) =>
            await _dbSet.FirstOrDefaultAsync(e => e.Identification == identification && e.RestaurantId == restaurantId && e.Restaurant.UserId == userId);
        
        public async Task<Table> GetByIdentificationRestaurantWithDiffIdAndUserId(string identification, long restaurantId, long tableId, long userId) =>
            await _dbSet.FirstOrDefaultAsync(e => e.Identification == identification && e.RestaurantId == restaurantId && e.Id != tableId && e.Restaurant.UserId == userId);

        public async Task<bool> ExistsByIdAndUserId(long id, long userId) => await _dbSet.AnyAsync(e => e.Id == id && e.Restaurant.UserId == userId);
        public async Task<bool> ExistsByIdAndUserId(long id, long restaurantId, long userId) => await _dbSet.AnyAsync(e => e.Id == id && e.RestaurantId == restaurantId && e.Restaurant.UserId == userId);
    }
}
