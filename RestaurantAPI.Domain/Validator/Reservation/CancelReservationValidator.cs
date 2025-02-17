using FluentValidation;
using RestaurantAPI.Domain.DTO.Reservation;
using RestaurantAPI.Domain.Interface.Repository;
using RestaurantAPI.Domain.Interface.Token;

namespace RestaurantAPI.Domain.Validator.Table
{
    public class CancelReservationValidator : AbstractValidator<TableCancelReservationDTO>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ITokenService _tokenService;
        public CancelReservationValidator(IReservationRepository reservationRepository,
            ITokenService tokenService
            )
        {
            _reservationRepository = reservationRepository;
            _tokenService = tokenService;

            RuleFor(a => a.Id)
                .MustAsync(async (id, cancellationToken) =>
                {
                    var reservation = await _reservationRepository.GetByIdAndUserId(id, _tokenService.GetUserId);
                    return reservation != null;
                })
                .WithMessage("Nenhuma reserva encontrada para esta mesa");
        }
    }
}
