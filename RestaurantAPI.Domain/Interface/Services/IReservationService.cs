using RestaurantAPI.Domain.DTO.Messaging;
using RestaurantAPI.Domain.DTO.Reservation;
using RestaurantAPI.Domain.DTO.Table;

namespace RestaurantAPI.Domain.Interface.Services
{
    public interface IReservationService
    {
        Task<TableReservationResponseDTO> Create(TableReservationDTO dto);
        Task<TableReservationResponseDTO> Cancel(long reservationId);
    }
}
