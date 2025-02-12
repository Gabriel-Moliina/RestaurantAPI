using RestaurantAPI.Domain.DTO.Table;

namespace RestaurantAPI.Domain.Interface.Services
{
    public interface ITableService
    {
        Task<TableDTO> GetById(long id);
        Task<List<TableDTO>> GetByRestaurantId(long restaurantId);
        Task<TableResponseDTO> Create(TableSaveDTO dto);
        Task<TableDTO> DeleteById(long id);
        Task<bool> Release(TableChangeStatusDTO dto);
    }
}
