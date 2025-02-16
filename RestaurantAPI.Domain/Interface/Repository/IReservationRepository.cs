using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interface.Repository.Base;

namespace RestaurantAPI.Domain.Interface.Repository
{
    public interface IReservationRepository : IBaseRepository<Reservation>
    {
        Task<Reservation> GetByIdAndUserId(long id, long userId);
        Task<Reservation> GetByTableId(long tableId);
        Task<Reservation> GetByTableIdAndUserId(long tableId, long userId);
        Task<bool> ExistByTableIdAndUserId(long tableId, long userId);
    }
}
