using FluentValidation;
using RestaurantAPI.Domain.DTO.Table;
using RestaurantAPI.Domain.Interface.Repository;
using RestaurantAPI.Domain.Interface.Token;

namespace RestaurantAPI.Domain.Validator.Table
{
    public class TableSaveValidator : AbstractValidator<TableSaveDTO>
    {
        private readonly ITableRepository _tableRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        public TableSaveValidator(ITableRepository tableRepository,
            IRestaurantRepository restaurantRepository,
            ITokenService tokenService)
        {
            _tableRepository = tableRepository;
            _restaurantRepository = restaurantRepository;

            RuleFor(a => a.Identification)
                .NotEmpty()
                .WithName("Identificação")
                .WithMessage("Identificação da mesa não pode ser vazia");

            RuleFor(a => new { a.Identification, a.RestaurantId, a.Id })
                .MustAsync(async (model, cancellationToken) =>
                {
                    if(model.Id == 0)
                        return !(await _tableRepository.Exists(model.Identification, model.RestaurantId) != null);

                    var editedTable = (await _tableRepository.Exists(model.Identification, model.RestaurantId))?.Id == model.Id;

                    return editedTable;
                })
                .WithName("Identificação")
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
