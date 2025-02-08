using RestaurantAPI.Domain.DTO.User;

namespace RestaurantAPI.Domain.Interface.Token
{
    public interface ITokenService
    {
        string Generate(UserLoginResponseDTO user);
        UserDTO GetUser();
    }
}
