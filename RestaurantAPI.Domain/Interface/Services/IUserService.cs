using RestaurantAPI.Domain.DTO.User;

namespace RestaurantAPI.Domain.Interface.Services
{
    public interface IUserService
    {
        Task<List<UserDTO>> Get();
        Task<UserDTO> GetById(long id);
        Task<UserCreateResponseDTO> Create(UserCreateDTO user);
    }
}
