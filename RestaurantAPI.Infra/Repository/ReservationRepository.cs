using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interface.Repository;
using RestaurantAPI.Infra.Context;
using RestaurantAPI.Infra.Repository.Base;

namespace RestaurantAPI.Infra.Repository
{
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(EntityContext context) : base(context)
        {
        }
        public override async Task<Reservation> GetById(long id) => await _dbSet.Where(e => e.Id == id).Include(e => e.Table).FirstOrDefaultAsync();
        public async Task<Reservation> GetByTableId(long tableId) => await _dbSet.FirstOrDefaultAsync(e => e.TableId == tableId);
        public async Task<bool> ExistByTableId(long tableId) => await _dbSet.AnyAsync(e => e.TableId == tableId);
    }
}
