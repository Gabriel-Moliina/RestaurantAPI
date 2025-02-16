using System.Transactions;
using FluentValidation;
using RestaurantAPI.Domain.DTO.Messaging;
using RestaurantAPI.Domain.DTO.Reservation;
using RestaurantAPI.Domain.Interface.Application;
using RestaurantAPI.Domain.Interface.Builder;
using RestaurantAPI.Domain.Interface.Messaging;
using RestaurantAPI.Domain.Interface.Notification;
using RestaurantAPI.Domain.Interface.Services;
using RestaurantAPI.Domain.Interface.Token;

namespace RestaurantAPI.Application.Application
{
    public class ReservationApplication : IReservationApplication
    {
        private readonly IRabbitMQSender _rabbitSender;
        private readonly IValidator<CreateReservationDTO> _validatorReservation;
        private readonly IValidator<TableCancelReservationDTO> _validatorCancelReservation;
        private readonly IReservationService _reservationService;
        private readonly INotification _notification;
        private readonly IEmailBuilder _emailBuilder;
        private readonly ITokenService _tokenService;
        public ReservationApplication(IRabbitMQSender rabbitSender,
            IReservationService reservationService,
            INotification notification,
            IValidator<CreateReservationDTO> validatorReservation,
            IValidator<TableCancelReservationDTO> validatorCancelTableReservation,
            ITokenService tokenService,
            IEmailBuilder emailBuilder
            )
        {
            _rabbitSender = rabbitSender;
            _reservationService = reservationService;
            _validatorReservation = validatorReservation;
            _validatorCancelReservation = validatorCancelTableReservation;
            _notification = notification;
            _emailBuilder = emailBuilder;
            _tokenService = tokenService;
        }

        public async Task<ReservationDTO> GetById(long id) => await _reservationService.GetByIdAndUserId(id, _tokenService.GetUserId);

        public async Task<CreateReservationResponseDTO> Create(CreateReservationDTO dto)
        {
            _notification.AddNotifications(await _validatorReservation.ValidateAsync(dto));
            if (_notification.HasNotifications) return null;

            using TransactionScope transactionScope = new(TransactionScopeAsyncFlowOption.Enabled);
            var table = await _reservationService.Create(dto);
            transactionScope.Complete();

            var dateLocal = TimeZoneInfo.ConvertTime(table.Date, TimeZoneInfo.Local).ToLocalTime();

            var email = _emailBuilder.WithName(table.RestaurantName).
                WithSubject($"{table.RestaurantName} - Reserva Confirmada").
                WithReceiver(table.Email).
                WithMessage($"Olá, gostaria de informar que sua reserva para a mesa {table.Identification} foi CONFIRMADA no dia {dateLocal:dd/MM/yyyy} às {dateLocal:HH:mm}").
                Build();

            await _rabbitSender.SendMessage<EmailDTO>(email);

            return table;
        }

        public async Task<CreateReservationResponseDTO> Cancel(long tableId)
        {
            _notification.AddNotifications(await _validatorCancelReservation.ValidateAsync(new TableCancelReservationDTO(tableId)));
            if (_notification.HasNotifications) return null;

            using TransactionScope transactionScope = new(TransactionScopeAsyncFlowOption.Enabled);
            var table = await _reservationService.Cancel(tableId);
            transactionScope.Complete();

            var dateLocal = TimeZoneInfo.ConvertTimeFromUtc(table.Date, TimeZoneInfo.Local).ToLocalTime();

            var email = _emailBuilder.WithName(table.RestaurantName).
                WithSubject($"{table.RestaurantName} - Reserva Cancelada").
                WithReceiver(table.Email).
                WithMessage($"Olá, gostaria de informar que sua reserva para a mesa {table.Identification} foi CANCELADA no dia {dateLocal:dd/MM/yyyy} às {dateLocal:HH:mm}").
                Build();

            await _rabbitSender.SendMessage<EmailDTO>(email);

            return table;
        }
    }
}
