using FluentValidation;
using RestaurantAPI.Domain.DTO.Table;
using RestaurantAPI.Domain.Interface.Repository;

namespace RestaurantAPI.Domain.Validator.Table
{
    public class TableDeleteValidator : AbstractValidator<TableDeleteDTO>
    {
        private readonly ITableRepository _tableRepository;
        public TableDeleteValidator(ITableRepository tableRepository)
        {
            _tableRepository = tableRepository;

            RuleFor(a => a.Id)
                .MustAsync(async (id, cancellationToken) =>
                {
                    return await _tableRepository.ExistsById(id);
                })
                .WithMessage("Mesa não encontrado");
        }
    }
}
