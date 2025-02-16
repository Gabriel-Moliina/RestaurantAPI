using FluentValidation;
using RestaurantAPI.Domain.DTO.Table;
using RestaurantAPI.Domain.Interface.Repository;
using RestaurantAPI.Domain.Interface.Token;

namespace RestaurantAPI.Domain.Validator.Table
{
    public class TableReleaseValidator : AbstractValidator<TableReleaseDTO>
    {
        private readonly ITableRepository _tableRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly ITokenService _tokenService;
        public TableReleaseValidator(ITableRepository tableRepository, 
            IReservationRepository reservationRepository,
            ITokenService tokenService
            )
        {
            _tableRepository = tableRepository;
            _reservationRepository = reservationRepository;
            _tokenService = tokenService;

            RuleFor(a => a.TableId)
                .NotEqual(0)
                .MustAsync(async (tableId, cancellationToken) =>
                {
                    return await _tableRepository.ExistsByIdAndUserId(tableId, _tokenService.GetUserId);
                })
                .WithMessage("Mesa não encontrada")
                .MustAsync(async (tableId, cancellationToken) =>
                {
                    return await _reservationRepository.ExistByTableIdAndUserId(tableId, _tokenService.GetUserId);
                })
                .WithMessage("Reserva não encontrada");
        }
    }
}
