using FluentValidation;
using RestaurantAPI.Domain.DTO.Reservation;
using RestaurantAPI.Domain.Interface.Repository;
using RestaurantAPI.Domain.Interface.Token;

namespace RestaurantAPI.Domain.Validator.Reservation
{
    public class CreateReservationValidator : AbstractValidator<CreateReservationDTO>
    {
        private readonly ITableRepository _tableRepository;
        private readonly ITokenService _tokenService;
        public CreateReservationValidator(ITableRepository tableRepository, ITokenService tokenService)
        {
            _tableRepository = tableRepository;
            _tokenService = tokenService;

            RuleFor(a => a.Email)
                .EmailAddress()
                .WithMessage("Email inválido");

            RuleFor(a => a.TableId)
                .MustAsync(async (tableId, cancellationToken) =>
                {
                    var table = await _tableRepository.GetByIdAndUserId(tableId, _tokenService.GetUserId);
                    return table != null;
                })
                .WithMessage("A mesa não foi encontrada")
                .MustAsync(async (tableId, cancellationToken) =>
                {
                    var table = await _tableRepository.GetByIdAndUserId(tableId, _tokenService.GetUserId);
                    return !(table?.Reserved ?? false);
                })
                .WithMessage("A mesa não está disponível para reserva");

            RuleFor(a => a.Date)
                .NotEqual(DateTime.MinValue)
                .WithMessage("Selecione uma data válida");
        }
    }
}
