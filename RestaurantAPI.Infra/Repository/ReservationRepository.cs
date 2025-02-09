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

        public async Task<Reservation> GetByTableId(long tableId) => await _dbSet.FirstOrDefaultAsync(e => e.TableId == tableId);
    }
}
