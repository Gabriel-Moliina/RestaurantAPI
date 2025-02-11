namespace RestaurantAPI.Domain.DTO.Table
{
    public class TableResponseDTO
    {
        public long Id { get; set; }
        public string Identification { get; set; }
        public int Capacity { get; set; }
        public long RestaurantId { get; set; }
        public long Reserved { get; set; }
    }
}
