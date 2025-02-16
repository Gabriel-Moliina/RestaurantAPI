using FluentValidation;
using RestaurantAPI.Domain.DTO.Table;
using RestaurantAPI.Domain.Interface.Repository;

namespace RestaurantAPI.Domain.Validator.Table
{
    public class TableReleaseValidator : AbstractValidator<TableReleaseDTO>
    {
        private readonly ITableRepository _tableRepository;
        private readonly IReservationRepository _reservationRepository;
        public TableReleaseValidator(ITableRepository tableRepository, IReservationRepository reservationRepository)
        {
            _tableRepository = tableRepository;
            _reservationRepository = reservationRepository;

            RuleFor(a => a.TableId)
                .NotEqual(0)
                .MustAsync(async (tableId, cancellationToken) =>
                {
                    return await _tableRepository.ExistsById(tableId);
                })
                .WithMessage("Mesa não encontrada")
                .MustAsync(async (tableId, cancellationToken) =>
                {
                    return await _reservationRepository.ExistByTableId(tableId);
                })
                .WithMessage("Reserva não encontrada");
        }
    }
}
