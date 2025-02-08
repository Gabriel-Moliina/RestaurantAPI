using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            RuleFor(a => a.Email)
                .EmailAddress()
                .WithMessage("Invalid Email!")
                .NotEmpty()
                .WithMessage("Invalid Email!");

            RuleFor(a => a.Email)
                .MustAsync(async (email, cancellation) =>
                {
                    bool exists = await _userRepository.Exists(email);
                    return exists;
                })
                .WithMessage("User not found!");

            RuleFor(a => a.Password)
                .NotEmpty()
                .MinimumLength(6);
        }
    }
}
