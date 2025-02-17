using RestaurantAPI.Domain.DTO.Reservation;

namespace RestaurantAPI.Domain.Interface.Application
{
    public interface IReservationApplication
    {
        Task<ReservationDTO> GetById(long id);
        Task<CreateReservationResponseDTO> Create(CreateReservationDTO dto);
        Task<CreateReservationResponseDTO> Cancel(long reservationId);
    }
}
