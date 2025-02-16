using FluentValidation;
using RestaurantAPI.Domain.DTO.Restaurant;
using RestaurantAPI.Domain.Interface.Repository;
using RestaurantAPI.Domain.Interface.Token;

namespace RestaurantAPI.Domain.Validator.Restaurant
{
    public class RestaurantDeleteValidator : AbstractValidator<RestaurantDeleteDTO>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly ITokenService _tokenService;
        public RestaurantDeleteValidator(IRestaurantRepository restaurantRepository,
            ITokenService tokenService
            )
        {
            _restaurantRepository = restaurantRepository;
            _tokenService = tokenService;

            RuleFor(a => a.Id)
                .MustAsync(async (id, cancellationToken) =>
                {
                    return await _restaurantRepository.ExistsByIdAndUserId(id, _tokenService.GetUserId);
                })
                .WithMessage("Restaurante não encontrado");
        }
    }
}
