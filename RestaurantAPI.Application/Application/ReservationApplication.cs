using System.Transactions;
using RestaurantAPI.Domain.DTO.Messaging;
using RestaurantAPI.Domain.DTO.Reservation;
using RestaurantAPI.Domain.DTO.Table;
using RestaurantAPI.Domain.Interface.Application;
using RestaurantAPI.Domain.Interface.Messaging;
using RestaurantAPI.Domain.Interface.Services;

namespace RestaurantAPI.Application.Application
{
    public class ReservationApplication : IReservationApplication
    {
        private readonly IRabbitMQSender _rabbitSender;
        private readonly IReservationService _reservationService;
        public ReservationApplication(IRabbitMQSender rabbitSender,
            IReservationService reservationService)
        {
            _rabbitSender = rabbitSender;
            _reservationService = reservationService;
        }

        public async Task<TableReservationResponseDTO> Create(TableReservationDTO dto)
        {
            using TransactionScope transactionScope = new(TransactionScopeAsyncFlowOption.Enabled);
            var table = await _reservationService.Create(dto);
            transactionScope.Complete();
            if (table != null && table.Reserved)
            {
                var email = new EmailDTO(table.RestaurantName,
                    $"{table.RestaurantName} - Reserva Confirmada",
                    table.Email,
                    $"Olá, gostaria de informar que sua reserva para a mesa {table.Identification} foi CONFIRMADA no dia {table.Date:dd/MM/yyyy} às {table.Date:HH:mm}");
                await _rabbitSender.SendMessage<EmailDTO>(email);
            }
            return table;
        }

        public async Task<TableReservationResponseDTO> Cancel(long tableId)
        {
            using TransactionScope transactionScope = new(TransactionScopeAsyncFlowOption.Enabled);
            var table = await _reservationService.Cancel(tableId);
            transactionScope.Complete();

            if (table != null && table.Reserved)
            {
                var email = new EmailDTO(table.RestaurantName,
                $"{table.RestaurantName} - Reserva Cancelada",
                table.Email,
                $"Olá, gostaria de informar que sua reserva para a mesa {table.Identification} foi CANCELADA no dia {table.Date:dd/MM/yyyy} às {table.Date:HH:mm}");
                await _rabbitSender.SendMessage<EmailDTO>(email);
            }

            return table;
        }
    }
}
