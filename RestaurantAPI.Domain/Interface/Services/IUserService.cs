using RestaurantAPI.Domain.DTO.User;

namespace RestaurantAPI.Domain.Interface.Services
{
    public interface IUserService
    {
        Task<UserCreateResponseDTO> Create(UserCreateDTO user);
        Task<UserLoginResponseDTO> Login(UserLoginDTO dto);
    }
}
