using FluentValidation;
using RestaurantAPI.Domain.DTO.Table;
using RestaurantAPI.Domain.Interface.Repository;
using RestaurantAPI.Domain.Interface.Token;

namespace RestaurantAPI.Domain.Validator.Table
{
    public class TableCreateValidator : AbstractValidator<TableDTO>
    {
        private readonly ITableRepository _tableRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        public TableCreateValidator(ITableRepository tableRepository,
            IRestaurantRepository restaurantRepository,
            ITokenService tokenService)
        {
            _tableRepository = tableRepository;
            _restaurantRepository = restaurantRepository;

            RuleFor(a => new { a.Identification, a.RestaurantId })
                .MustAsync(async (obj, cancellationToken) =>
                {
                    if (string.IsNullOrEmpty(obj.Identification)) return false;
                    return !await _tableRepository.Exists(obj.Identification, obj.RestaurantId);
                })
                .WithName("Identification")
                .WithMessage("Mesa já existente!");

            RuleFor(a => a.RestaurantId)
                .MustAsync(async (restaurantId, cancellationToken) =>
                {
                    return await _restaurantRepository.GetById(restaurantId) != null;
                })
                .WithMessage("Restaurante não encontrado!");

            RuleFor(a => a.Capacity)
                .NotEqual(0)
                .WithMessage("A capcaidade da mesa não pode ser 0");
        }
    }
}
