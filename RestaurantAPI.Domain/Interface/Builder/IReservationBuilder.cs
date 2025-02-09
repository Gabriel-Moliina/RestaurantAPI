using RestaurantAPI.Domain.Builder.ReservationBuilder;
using RestaurantAPI.Domain.Entities;

namespace RestaurantAPI.Domain.Interface.Builder
{
    public interface IReservationBuilder : IBaseBuilder<Reservation>
    {
        ReservationBuilder WithEmail(string email);
        ReservationBuilder WithDate(DateTime date);
        ReservationBuilder WithTableId(long tableId);
        ReservationBuilder WithTable(Table table);
    }
}
