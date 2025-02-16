namespace RestaurantAPI.Domain.DTO.Reservation
{
    public class CreateReservationResponseDTO
    {
        public string RestaurantName { get; set; }
        public string Identification { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public bool Reserved { get; set; }
    }
}
