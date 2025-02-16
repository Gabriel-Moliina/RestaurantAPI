using FluentValidation;
using RestaurantAPI.Domain.DTO.Reservation;
using RestaurantAPI.Domain.Interface.Repository;

namespace RestaurantAPI.Domain.Validator.Table
{
    public class CancelReservationValidator : AbstractValidator<TableCancelReservationDTO>
    {
        private readonly ITableRepository _tableRepository;
        private readonly IReservationRepository _reservationRepository;
        public CancelReservationValidator(ITableRepository tableRepository,
            IReservationRepository reservationRepository)
        {
            _tableRepository = tableRepository;
            _reservationRepository = reservationRepository;

            RuleFor(a => a.TableId)
                .MustAsync(async (tableId, cancellationToken) =>
                {
                    return await _tableRepository.ExistsById(tableId);
                })
                .WithMessage("Mesa não encontrada")
                .MustAsync(async (tableId, cancellationToken) =>
                {
                    var reservation = await _reservationRepository.GetByTableId(tableId);
                    return reservation != null;
                })
                .WithMessage("Nenhuma reserva encontrada para esta mesa");
        }
    }
}
