using RestaurantAPI.Domain.Builder.ReservationBuilder;
using RestaurantAPI.Domain.DTO.Table;

namespace RestaurantAPI.Domain.Interface.Builder
{
    public interface ITableReservationResponseBuilder : IBaseBuilder<TableReservationResponseDTO>
    {
        TableReservationResponseBuilder WithRestaurantName(string restaurantName);
        TableReservationResponseBuilder WithIdentification(string identification);
        TableReservationResponseBuilder WithEmail(string email);
        TableReservationResponseBuilder WithDate(DateTime date);
        TableReservationResponseBuilder WithReserved(bool reserved);
    }
}
