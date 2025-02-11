using FluentValidation;
using RestaurantAPI.Domain.DTO.Table;
using RestaurantAPI.Domain.Interface.Repository;

namespace RestaurantAPI.Domain.Validator.Table
{
    public class TableChangeStatusValidator : AbstractValidator<TableChangeStatusDTO>
    {
        private readonly ITableRepository _tableRepository;
        public TableChangeStatusValidator(ITableRepository tableRepository)
        {
            _tableRepository = tableRepository;

            RuleFor(a => a.TableId)
                .NotEqual(0)
                .MustAsync(async (tableId, cancellationToken) =>
                {
                    return await _tableRepository.ExistsById(tableId);
                })
                .WithMessage("Mesa não encontrada!");
        }
    }
}
