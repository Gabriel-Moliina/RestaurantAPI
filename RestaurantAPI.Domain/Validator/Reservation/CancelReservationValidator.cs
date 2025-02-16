using FluentValidation;
using RestaurantAPI.Domain.DTO.Reservation;
using RestaurantAPI.Domain.Interface.Repository;
using RestaurantAPI.Domain.Interface.Token;

namespace RestaurantAPI.Domain.Validator.Table
{
    public class CancelReservationValidator : AbstractValidator<TableCancelReservationDTO>
    {
        private readonly ITableRepository _tableRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly ITokenService _tokenService;
        public CancelReservationValidator(ITableRepository tableRepository,
            IReservationRepository reservationRepository,
            ITokenService tokenService
            )
        {
            _tableRepository = tableRepository;
            _reservationRepository = reservationRepository;
            _tokenService = tokenService;

            RuleFor(a => a.TableId)
                .MustAsync(async (tableId, cancellationToken) =>
                {
                    return await _tableRepository.ExistsByIdAndUserId(tableId, _tokenService.GetUserId);
                })
                .WithMessage("Mesa não encontrada")
                .MustAsync(async (tableId, cancellationToken) =>
                {
                    var reservation = await _reservationRepository.GetByTableIdAndUserId(tableId, _tokenService.GetUserId);
                    return reservation != null;
                })
                .WithMessage("Nenhuma reserva encontrada para esta mesa");
        }
    }
}
