using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using RestaurantAPI.Domain.DTO.User;

namespace RestaurantAPI.Domain.Validator.User
{
    public class UserCreateValidator : AbstractValidator<UserCreateDTO>
    {
        public UserCreateValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty();

            RuleFor(a => a.Email)
                .EmailAddress()
                .NotEmpty()
                .WithMessage("Invalid Email!");

            RuleFor(a => a.Password)
                .NotEmpty()
                .MinimumLength(6)
                .WithMessage("Password must contain more than 6 digits!")
                .Must(ValidateUpper)
                .WithMessage("The password must contain at least one uppercase character!");
        }

        public bool ValidateUpper(string value)
        {
            return value.Any(char.IsUpper);
        }
    }
}
