using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interface.Builder;

namespace RestaurantAPI.Domain.Builder.ReservationBuilder
{
    public class ReservationBuilder : IReservationBuilder
    {
        private readonly Reservation _reservation;
        public ReservationBuilder()
        {
            _reservation = new Reservation();
        }

        public ReservationBuilder WithEmail(string email)
        {
            _reservation.Email = email;
            return this;
        }
        public ReservationBuilder WithDate(DateTime date)
        {
            _reservation.Date = date;
            return this;
        }
        public ReservationBuilder WithTableId(long tableId)
        {
            _reservation.TableId = tableId;
            return this;
        }
        public ReservationBuilder WithTable(Table table)
        {
            _reservation.Table = table;
            return this;
        }

        public Reservation Build()
        {
            return _reservation;
        }
    }
}
