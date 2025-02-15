﻿using System.Transactions;
using FluentValidation;
using RestaurantAPI.Domain.DTO.Messaging;
using RestaurantAPI.Domain.DTO.Reservation;
using RestaurantAPI.Domain.DTO.Table;
using RestaurantAPI.Domain.Interface.Application;
using RestaurantAPI.Domain.Interface.Builder;
using RestaurantAPI.Domain.Interface.Messaging;
using RestaurantAPI.Domain.Interface.Notification;
using RestaurantAPI.Domain.Interface.Services;

namespace RestaurantAPI.Application.Application
{
    public class ReservationApplication : IReservationApplication
    {
        private readonly IRabbitMQSender _rabbitSender;
        private readonly IValidator<TableReservationDTO> _validatorReservation;
        private readonly IValidator<TableCancelReservationDTO> _validatorCancelReservation;
        private readonly IReservationService _reservationService;
        private readonly INotification _notification;
        private readonly IEmailBuilder _emailBuilder;
        public ReservationApplication(IRabbitMQSender rabbitSender,
            IReservationService reservationService,
            INotification notification,
            IValidator<TableReservationDTO> validatorReservation,
            IValidator<TableCancelReservationDTO> validatorCancelTableReservation,
            IEmailBuilder emailBuilder
            )
        {
            _rabbitSender = rabbitSender;
            _reservationService = reservationService;
            _validatorReservation = validatorReservation;
            _validatorCancelReservation = validatorCancelTableReservation;
            _notification = notification;
            _emailBuilder = emailBuilder;
        }

        public async Task<TableReservationResponseDTO> Create(TableReservationDTO dto)
        {
            _notification.AddNotifications(await _validatorReservation.ValidateAsync(dto));
            if (_notification.HasNotifications) return null;

            using TransactionScope transactionScope = new(TransactionScopeAsyncFlowOption.Enabled);
            var table = await _reservationService.Create(dto);
            transactionScope.Complete();

            var email = _emailBuilder.WithName(table.RestaurantName).
                WithSubject($"{table.RestaurantName} - Reserva Confirmada").
                WithReceiver(table.Email).
                WithMessage($"Olá, gostaria de informar que sua reserva para a mesa {table.Identification} foi CONFIRMADA no dia {table.Date:dd/MM/yyyy} às {table.Date:HH:mm}").
                Build();

            await _rabbitSender.SendMessage<EmailDTO>(email);

            return table;
        }

        public async Task<TableReservationResponseDTO> Cancel(long tableId)
        {
            _notification.AddNotifications(await _validatorCancelReservation.ValidateAsync(new TableCancelReservationDTO(tableId)));
            if (_notification.HasNotifications) return null;

            using TransactionScope transactionScope = new(TransactionScopeAsyncFlowOption.Enabled);
            var table = await _reservationService.Cancel(tableId);
            transactionScope.Complete();

            var email = _emailBuilder.WithName(table.RestaurantName).
            WithSubject($"{table.RestaurantName} - Reserva Cancelada").
            WithReceiver(table.Email).
            WithMessage($"Olá, gostaria de informar que sua reserva para a mesa {table.Identification} foi CANCELADA no dia {table.Date:dd/MM/yyyy} às {table.Date:HH:mm}").
            Build();

            await _rabbitSender.SendMessage<EmailDTO>(email);

            return table;
        }
    }
}
