using FluentValidation;
using RestaurantAPI.Domain.DTO.Table;
using RestaurantAPI.Domain.Interface.Repository;
using RestaurantAPI.Domain.ValueObjects.Table;

namespace RestaurantAPI.Domain.Validator.Table
{
    public class TableChangeStatusValidator : AbstractValidator<TableChangeStatusDTO>
    {
        private readonly ITableRepository _tableRepository;
        public TableChangeStatusValidator(ITableRepository tableRepository)
        {
            _tableRepository = tableRepository;

            RuleFor(a => a.Status)
                .NotEmpty()
                .Must((status) =>
                {
                    return Enum.IsDefined(typeof(EnumTableStatus), status);
                })
                .WithMessage("Status não encontrado!");

            RuleFor(a => a.TableId)
                .NotEqual(0)
                .MustAsync(async (tableId, cancellationToken) =>
                {
                    return await _tableRepository.ExistsById(tableId);
                })
                .WithName("Mesa")
                .WithMessage("Mesa não encontrada!");
        }
    }
}
