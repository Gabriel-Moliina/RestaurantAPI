using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interface.Repository.Base;

namespace RestaurantAPI.Domain.Interface.Repository
{
    public interface IReservationRepository : IBaseRepository<Reservation>
    {
        Task<Reservation> GetByTableId(long tableId);
        Task<bool> ExistByTableId(long tableId);
    }
}
