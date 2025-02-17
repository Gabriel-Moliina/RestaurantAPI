namespace RestaurantAPI.Domain.DTO.Reservation
{
    public class TableCancelReservationDTO
    {
        public TableCancelReservationDTO(long id)
        {
            Id = id;
        }
        public long Id { get; }
    }
}
