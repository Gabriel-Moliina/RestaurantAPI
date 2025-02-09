using RestaurantAPI.Domain.DTO.Reservation;
using RestaurantAPI.Domain.DTO.Table;

namespace RestaurantAPI.Domain.Interface.Application
{
    public interface IReservationApplication
    {
        Task<TableReservationResponseDTO> Create(TableReservationDTO dto);
        Task<TableReservationResponseDTO> Cancel(long tableId);
    }
}
