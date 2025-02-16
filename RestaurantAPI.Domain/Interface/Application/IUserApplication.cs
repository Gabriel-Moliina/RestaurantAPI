using RestaurantAPI.Domain.DTO.User;

namespace RestaurantAPI.Domain.Interface.Application
{
    public interface IUserApplication
    {
        Task<UserCreateResponseDTO> Create(UserCreateDTO dto);
        Task<UserLoginResponseDTO> Login(UserLoginDTO dto);
    }
}
