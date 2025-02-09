using FluentValidation;
using RestaurantAPI.Domain.DTO.User;

namespace RestaurantAPI.Domain.Validator.User
{
    public class UserCreateValidator : AbstractValidator<UserCreateDTO>
    {
        public UserCreateValidator()
        {
            RuleFor(a => a.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("Email inválido!");

            RuleFor(a => a.Password)
                .NotEmpty()
                .MinimumLength(6)
                .WithMessage("Senha deve conter ao menos 6 dígitos")
                .Must(ValidateUpper)
                .WithMessage("A senha deve conter ao menos uma letra maiúscula!");

            RuleFor(a => new { a.Password, a.ConfirmPassword })
                .Must(x => x.Password == x.ConfirmPassword)
                .WithMessage("ConfirmPassword")
                .WithMessage("As senhas devem coincidir");
        }

        public bool ValidateUpper(string value)
        {
            return value.Any(char.IsUpper);
        }
    }
}
