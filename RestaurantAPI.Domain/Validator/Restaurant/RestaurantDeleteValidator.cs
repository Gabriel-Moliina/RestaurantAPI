using FluentValidation;
using RestaurantAPI.Domain.DTO.Restaurant;
using RestaurantAPI.Domain.Interface.Repository;

namespace RestaurantAPI.Domain.Validator.Restaurant
{
    public class RestaurantDeleteValidator : AbstractValidator<RestaurantDeleteDTO>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        public RestaurantDeleteValidator(IRestaurantRepository restaurantRepository)
        {
            RuleFor(a => a.Id)
                .MustAsync(async (id, cancellationToken) =>
                {
                    return await _restaurantRepository.ExistsById(id);
                })
                .WithMessage("Restaurante não encontrado");
        }
    }
}
