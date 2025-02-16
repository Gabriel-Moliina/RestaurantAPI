using RestaurantAPI.Domain.Builder.ReservationBuilder;
using RestaurantAPI.Domain.DTO.Reservation;

namespace RestaurantAPI.Domain.Interface.Builder
{
    public interface ITableReservationResponseBuilder : IBaseBuilder<CreateReservationResponseDTO>
    {
        ITableReservationResponseBuilder WithRestaurantName(string restaurantName);
        ITableReservationResponseBuilder WithIdentification(string identification);
        ITableReservationResponseBuilder WithEmail(string email);
        ITableReservationResponseBuilder WithDate(DateTime date);
        ITableReservationResponseBuilder WithReserved(bool reserved);
    }
}
