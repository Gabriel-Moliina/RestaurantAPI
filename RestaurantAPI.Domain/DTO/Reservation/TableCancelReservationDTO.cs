namespace RestaurantAPI.Domain.DTO.Reservation
{
    public class TableCancelReservationDTO
    {
        public TableCancelReservationDTO(long tableId)
        {
            TableId = tableId;
        }
        public long TableId { get; }
    }
}
