using RestaurantAPI.Domain.DTO.Table;

namespace RestaurantAPI.Domain.Interface.Services
{
    public interface ITableService
    {
        Task<TableDTO> GetByIdAndUserId(long id, long userId);
        Task<List<TableDTO>> GetByRestaurantIdAndUserId(long restaurantId, long userId);
        Task<TableResponseDTO> SaveOrUpdate(TableSaveDTO dto);
        Task<TableDTO> DeleteById(long id);
        Task<bool> Release(TableReleaseDTO dto);
    }
}
