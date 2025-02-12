using RestaurantAPI.Domain.DTO.Table;

namespace RestaurantAPI.Domain.Interface.Application
{
    public interface ITableApplication
    {
        Task<TableDTO> GetById(long id);
        Task<List<TableDTO>> GetByRestaurantId(long restaurantId);
        Task<TableResponseDTO> SaveOrUpdate(TableSaveDTO dto);
        Task<TableDTO> DeleteById(long id);
        Task<bool> Release(TableChangeStatusDTO dto);

    }
}
