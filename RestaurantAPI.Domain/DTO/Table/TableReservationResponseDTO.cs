namespace RestaurantAPI.Domain.DTO.Table
{
    public class TableReservationResponseDTO
    {
        public string RestaurantName { get; set; }
        public string Identification { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public bool Reserved { get; set; }
    }
}
