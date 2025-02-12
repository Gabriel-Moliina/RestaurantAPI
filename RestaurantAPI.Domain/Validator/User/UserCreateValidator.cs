using FluentValidation;
using RestaurantAPI.Domain.DTO.User;

namespace RestaurantAPI.Domain.Validator.User
{
    public class UserCreateValidator : AbstractValidator<UserCreateDTO>
    {
        public UserCreateValidator()
        {
            RuleFor(a => a.Email)
                .EmailAddress()
                .WithMessage("Email inválido!");

            RuleFor(a => a.Password)
                .NotEmpty()
                .WithMessage("A senha não pode ser vazia!")
                .MinimumLength(6)
                .WithMessage("Senha deve conter ao menos 6 dígitos!")
                .Must(ValidateUpper)
                .WithMessage("A senha deve conter ao menos uma letra maiúscula!")
                .WithName("Senha");

            RuleFor(a => new { a.Password, a.ConfirmPassword })
                .Must(x => x.Password == x.ConfirmPassword)
                .WithMessage("As senhas devem coincidir");
        }

        public bool ValidateUpper(string value) => value?.Any(char.IsUpper) ?? false;
    }
}
