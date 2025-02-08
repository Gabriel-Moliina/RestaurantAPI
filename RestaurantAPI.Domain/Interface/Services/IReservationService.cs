using RestaurantAPI.Domain.DTO.Messaging;

namespace RestaurantAPI.Domain.Interface.Services
{
    public interface IReservationService
    {
        Task CreateReserve(ReserveTableMessage reserveMessage);
    }
}
