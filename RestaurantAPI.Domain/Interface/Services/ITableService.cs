using RestaurantAPI.Domain.DTO.Table;
using RestaurantAPI.Domain.ValueObjects.Table;

namespace RestaurantAPI.Domain.Interface.Services
{
    public interface ITableService
    {
        Task<TableDTO> GetById(long id);
        Task<TableResponseDTO> Create(TableDTO dto);
        Task<TableDTO> DeleteById(long id);
        Task<bool> ChangeStatus(TableChangeStatusDTO dto);
    }
}
