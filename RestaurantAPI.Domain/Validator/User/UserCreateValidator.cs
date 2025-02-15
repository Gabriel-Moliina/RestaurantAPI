using FluentValidation;
using RestaurantAPI.Domain.DTO.User;
using RestaurantAPI.Domain.Interface.Repository;

namespace RestaurantAPI.Domain.Validator.User
{
    public class UserCreateValidator : AbstractValidator<UserCreateDTO>
    {
        private readonly IUserRepository _userRepository;
        public UserCreateValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            
            RuleFor(a => a.Email)
                .EmailAddress()
                .WithMessage("Email inválido")
                .MustAsync(async (email, cancellationToken) =>
                {
                    return !await _userRepository.Exists(email);
                })
                .WithMessage("Email já cadastrado");

            RuleFor(a => a.Password)
                .NotEmpty()
                .WithMessage("A senha não pode ser vazia")
                .MinimumLength(6)
                .WithMessage("Senha deve conter ao menos 6 dígitos")
                .Must(ValidateUpper)
                .WithMessage("A senha deve conter ao menos uma letra maiúscula")
                .WithName("Senha");

            RuleFor(a => new { a.Password, a.ConfirmPassword })
                .Must(x => x.Password == x.ConfirmPassword)
                .WithMessage("As senhas devem coincidir");
        }

        public bool ValidateUpper(string value) => value?.Any(char.IsUpper) ?? false;
    }
}
