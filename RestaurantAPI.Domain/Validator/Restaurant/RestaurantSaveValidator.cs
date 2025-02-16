using FluentValidation;
using RestaurantAPI.Domain.DTO.Restaurant;
using RestaurantAPI.Domain.Interface.Repository;
using RestaurantAPI.Domain.Interface.Token;

namespace RestaurantAPI.Domain.Validator.Restaurant
{
    public class RestaurantSaveValidator : AbstractValidator<RestaurantSaveDTO>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly ITokenService _tokenService;
        public RestaurantSaveValidator(IRestaurantRepository restaurantRepository,
            ITokenService tokenService)
        {
            _restaurantRepository = restaurantRepository;
            _tokenService = tokenService;

            RuleFor(a => a.Name)
                .NotEmpty()
                .WithMessage("O nome deve ser preenchido!");

            When(a => a.Id == 0, () =>
            {
                RuleFor(a => a.Name)
                .MustAsync(async (model, name, cancellationToken) =>
                {
                    return !await _restaurantRepository.ExistsByNameAndUserId(name, _tokenService.GetUserId);
                })
                .WithName("Name")
                .WithMessage("Restaurante já existente");
            });

            When(a => a.Id != 0, () =>
            {
                RuleFor(a => a.Name)
                .MustAsync(async (model, name, cancellationToken) =>
                {
                    return !await _restaurantRepository.ExistsWithDiffId(name, _tokenService.GetUserId, model.Id);
                })
                .WithName("Name")
                .WithMessage("Restaurante já existente")
                .MustAsync(async (model, name, cancellationToken) =>
                {
                    return await _restaurantRepository.ExistsByIdAndUserId(model.Id, _tokenService.GetUserId);
                })
                .WithName("Name")
                .WithMessage("Restaurante não encontrado");
            });
        }
    }
}
