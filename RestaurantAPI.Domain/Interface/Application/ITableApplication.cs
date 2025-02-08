using RestaurantAPI.Domain.DTO.Table;

namespace RestaurantAPI.Domain.Interface.Application
{
    public interface ITableApplication
    {
        Task<TableDTO> GetById(long id);
        Task<TableResponseDTO> Create(TableDTO dto);
        Task<TableDTO> DeleteById(long id);
        Task<bool> ChangeStatus(TableChangeStatusDTO dto);

    }
}
