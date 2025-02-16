using RestaurantAPI.Domain.Builder.ReservationBuilder;
using RestaurantAPI.Domain.DTO.Table;

namespace RestaurantAPI.Domain.Interface.Builder
{
    public interface ITableReservationResponseBuilder : IBaseBuilder<TableReservationResponseDTO>
    {
        ITableReservationResponseBuilder WithRestaurantName(string restaurantName);
        ITableReservationResponseBuilder WithIdentification(string identification);
        ITableReservationResponseBuilder WithEmail(string email);
        ITableReservationResponseBuilder WithDate(DateTime date);
        ITableReservationResponseBuilder WithReserved(bool reserved);
    }
}
