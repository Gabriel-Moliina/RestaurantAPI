using RestaurantAPI.Domain.DTO.Messaging;
using RestaurantAPI.Domain.DTO.Reservation;

namespace RestaurantAPI.Domain.Interface.Services
{
    public interface IReservationService
    {
        Task<ReservationDTO> GetByIdAndUserId(long id, long userId);
        Task<CreateReservationResponseDTO> Create(CreateReservationDTO dto);
        Task<CreateReservationResponseDTO> Cancel(long reservationId);
    }
}
