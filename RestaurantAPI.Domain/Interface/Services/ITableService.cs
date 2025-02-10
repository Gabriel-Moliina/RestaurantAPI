using RestaurantAPI.Domain.DTO.Table;

namespace RestaurantAPI.Domain.Interface.Services
{
    public interface ITableService
    {
        Task<TableDTO> GetById(long id);
        Task<List<TableDTO>> GetByRestaurantId(long restaurantId);
        Task<TableResponseDTO> Create(TableDTO dto);
        Task<TableDTO> DeleteById(long id);
        Task<bool> ChangeStatus(TableChangeStatusDTO dto);
    }
}
