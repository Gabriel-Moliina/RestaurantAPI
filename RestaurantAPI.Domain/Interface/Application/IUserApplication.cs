using RestaurantAPI.Domain.DTO.User;

namespace RestaurantAPI.Domain.Interface.Application
{
    public interface IUserApplication
    {
        Task<List<UserDTO>> Get();
        Task<UserDTO> GetById(long id);
        Task<UserCreateResponseDTO> Create(UserCreateDTO dto);
    }
}
