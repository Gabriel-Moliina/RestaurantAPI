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
            IRestaurantRepository restaurantRepository)
        {
            _tableRepository = tableRepository;
            _restaurantRepository = restaurantRepository;

            RuleFor(a => a.Identification)
                .NotEmpty()
                .WithName("Identificação")
                .WithMessage("Identificação da mesa não pode ser vazia");

            When(a => a.Id == 0, () =>
            {
                RuleFor(a => a.Identification)
                .MustAsync(async (model, identification, cancellationToken) =>
                {
                    return !(await _tableRepository.GetByIdentificationRestaurant(model.Identification, model.RestaurantId) != null);
                })
                .WithName("Identificação")
                .WithMessage("Mesa já existente");
            });
            
            When(a => a.Id != 0, () =>
            {
                RuleFor(a => a.Identification)
                .MustAsync(async (model, identification, cancellationToken) =>
                {
                    var tableExists = await _tableRepository.GetByIdentificationRestaurantWithDiffId(model.Identification, model.RestaurantId, model.Id) == null;

                    return tableExists;
                })
                .WithName("Identificação")
                .WithMessage("Mesa já existente");
            });



            RuleFor(a => a.RestaurantId)
                .MustAsync(async (restaurantId, cancellationToken) =>
                {
                    return await _restaurantRepository.GetById(restaurantId) != null;
                })
                .WithMessage("Restaurante não encontrado");

            RuleFor(a => a.Capacity)
                .NotEqual(0)
                .WithMessage("A capacidade da mesa não pode ser 0");
        }
    }
}
