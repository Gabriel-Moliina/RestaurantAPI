using FluentValidation;
using RestaurantAPI.Domain.DTO.Reservation;
using RestaurantAPI.Domain.Interface.Repository;

namespace RestaurantAPI.Domain.Validator.Reservation
{
    public class CreateReservationValidator : AbstractValidator<CreateReservationDTO>
    {
        private readonly ITableRepository _tableRepository;
        public CreateReservationValidator(ITableRepository tableRepository)
        {
            _tableRepository = tableRepository;

            RuleFor(a => a.Email)
                .EmailAddress()
                .WithMessage("Email inválido");

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
                    return !(table?.Reserved ?? false);
                })
                .WithMessage("A mesa não está disponível para reserva");

            RuleFor(a => a.Date)
                .NotEqual(DateTime.MinValue)
                .WithMessage("Selecione uma data válida");
        }
    }
}
