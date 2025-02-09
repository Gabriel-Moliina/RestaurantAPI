using FluentValidation;
using RestaurantAPI.Domain.DTO.Restaurant;
using RestaurantAPI.Domain.Interface.Repository;
using RestaurantAPI.Domain.Interface.Token;

namespace RestaurantAPI.Domain.Validator.Restaurant
{
    public class RestaurantCreateValidator : AbstractValidator<RestaurantCreateDTO>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly ITokenService _tokenService;
        public RestaurantCreateValidator(IRestaurantRepository restaurantRepository,
            ITokenService tokenService)
        {
            _restaurantRepository = restaurantRepository;
            _tokenService = tokenService;

            RuleFor(a => a.Name)
                .NotEmpty()
                .WithMessage("O nome deve ser preenchido!");

            RuleFor(a => a.Name)
                .MustAsync(async (name, cancellationToken) =>
                {
                    if (string.IsNullOrEmpty(name)) return true;
                    return !await _restaurantRepository.Exists(name, _tokenService.GetUser().Id);
                })
                .WithMessage("Restaurante já existente!");
        }
    }
}
