using RestaurantAPI.Domain.DTO.Reservation;
using RestaurantAPI.Domain.DTO.Table;
using RestaurantAPI.Domain.Interface.Builder;

namespace RestaurantAPI.Domain.Builder.ReservationBuilder
{
    public class TableReservationResponseBuilder : ITableReservationResponseBuilder
    {
        private readonly TableReservationResponseDTO _tableReservation;
        public TableReservationResponseBuilder()
        {
            _tableReservation = new TableReservationResponseDTO();
        }

        public TableReservationResponseBuilder WithRestaurantName(string restaurantName)
        {
            _tableReservation.RestaurantName = restaurantName;
            return this;
        }
        public TableReservationResponseBuilder WithIdentification(string identification)
        {
            _tableReservation.Identification = identification;
            return this;
        }
        public TableReservationResponseBuilder WithEmail(string email)
        {
            _tableReservation.Email = email;
            return this;
        }
        public TableReservationResponseBuilder WithDate(DateTime date)
        {
            _tableReservation.Date = date;
            return this;
        }
        public TableReservationResponseBuilder WithReserved(bool reserved)
        {
            _tableReservation.Reserved = reserved;
            return this;
        }
        public TableReservationResponseDTO Build() =>_tableReservation;
    }
}
