using RestaurantAPI.Domain.ValueObjects.Table;

namespace RestaurantAPI.Domain.DTO.Table
{
    public class TableDTO
    {
        public long Id { get; set; }
        public string Identification { get; set; }
        public int Capacity { get; set; }
        public long RestaurantId { get; set; }
        public EnumTableStatus Status { get; set; }
    }
}
