using RestaurantAPI.Domain.ValueObjects.Table;

namespace RestaurantAPI.Domain.DTO.Table
{
    public class TableChangeStatusDTO
    {
        public long TableId { get; set; }
        public EnumTableStatus Status { get; set; }
    }
}
