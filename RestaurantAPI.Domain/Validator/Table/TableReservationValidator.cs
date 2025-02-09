using FluentValidation;
using RestaurantAPI.Domain.DTO.Reservation;
using RestaurantAPI.Domain.Interface.Repository;
using RestaurantAPI.Domain.ValueObjects.Table;

namespace RestaurantAPI.Domain.Validator.Table
{
    public class TableReservationValidator : AbstractValidator<TableReservationDTO>
    {
        private readonly ITableRepository _tableRepository;
        public TableReservationValidator(ITableRepository tableRepository)
        {
            _tableRepository = tableRepository;

            RuleFor(a => a.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("Email inválido!");

            RuleFor(a => a.TableId)
                .MustAsync(async (tableId, cancellationToken) =>
                {
                    var table = await _tableRepository.GetById(tableId);
                    return table != null;
                })
                .WithMessage("A mesa não foi encontrada")
                .MustAsync(async (tableId, cancellationToken) =>
                {
                    var table = await _tableRepository.GetById(tableId);
                    return table?.Status == EnumTableStatus.Free;
                })
                .WithMessage("A mesa não está disponível para reserva");
        }
    }
}
