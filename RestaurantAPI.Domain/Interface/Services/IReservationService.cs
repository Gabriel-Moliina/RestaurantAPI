using RestaurantAPI.Domain.DTO.Messaging;
using RestaurantAPI.Domain.DTO.Reservation;

namespace RestaurantAPI.Domain.Interface.Services
{
    public interface IReservationService
    {
        Task<ReservationDTO> GetById(long id);
        Task<CreateReservationResponseDTO> Create(CreateReservationDTO dto);
        Task<CreateReservationResponseDTO> Cancel(long reservationId);
    }
}
