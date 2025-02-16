using RestaurantAPI.Domain.Builder.ReservationBuilder;
using RestaurantAPI.Domain.Entities;

namespace RestaurantAPI.Domain.Interface.Builder
{
    public interface IReservationBuilder : IBaseBuilder<Reservation>
    {
        IReservationBuilder WithEmail(string email);
        IReservationBuilder WithDate(DateTime date);
        IReservationBuilder WithTableId(long tableId);
        IReservationBuilder WithTable(Table table);
    }
}
