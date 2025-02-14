using FluentValidation;
using RestaurantAPI.Domain.DTO.User;
using RestaurantAPI.Domain.Interface.Repository;

namespace RestaurantAPI.Domain.Validator.User
{
    public class UserLoginValidator : AbstractValidator<UserLoginDTO>
    {
        private readonly IUserRepository _userRepository;
        public UserLoginValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(a => new { a.Email, a.Password })
                .MustAsync(async (dto, cancellation) =>
                {
                    var user = await _userRepository.ValidateUser(dto.Email, dto.Password);
                    return user != null;
                })
                .WithName("Login")
                .WithMessage("Usuário/senha não encontrados");
        }
    }
}
