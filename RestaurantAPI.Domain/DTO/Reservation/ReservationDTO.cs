namespace RestaurantAPI.Domain.DTO.Reservation
{
    public class ReservationDTO
    {
        public long TableId { get; set; }
        public string Identification { get; set; }
        public DateTime Date { get; set; }
        public string Email { get; set; }
    }
}
