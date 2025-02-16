namespace RestaurantAPI.Domain.DTO.Table
{
    public class TableDeleteDTO
    {
        public TableDeleteDTO(long id)
        {
            Id = id;
        }
        public long Id { get; set; }
    }
}
