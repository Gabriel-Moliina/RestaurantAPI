using AutoMapper;
using FluentValidation;
using RestaurantAPI.Domain.DTO.Reservation;
using RestaurantAPI.Domain.DTO.Table;
using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interface.Notification;
using RestaurantAPI.Domain.Interface.Repository;
using RestaurantAPI.Domain.Interface.Services;
using RestaurantAPI.Domain.ValueObjects.Table;
using RestaurantAPI.Service.Services.Base;

namespace RestaurantAPI.Service.Services
{
    public class ReservationService : BaseService, IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ITableRepository _tableRepository;
        private readonly IValidator<TableReservationDTO> _validatorReservation;
        public ReservationService(IMapper mapper,
            INotification notification,
            ITableRepository tableRepository,
            IValidator<TableReservationDTO> validatorReservation,
            IReservationRepository reservationRepository) : base(mapper, notification)
        {
            _tableRepository = tableRepository;
            _reservationRepository = reservationRepository;
            _validatorReservation = validatorReservation;
        }

        public async Task<TableReservationResponseDTO> Create(TableReservationDTO dto)
        {
            _notification.AddNotifications(await _validatorReservation.ValidateAsync(dto));
            if (_notification.HasNotifications) return null;

            var table = await _tableRepository.GetById(dto.TableId);
            table.Status = EnumTableStatus.Reserved;
            await _tableRepository.Update(table);

            Reservation reservation = new()
            {
                Date = dto.Date,
                Email = dto.Email,
                TableId = dto.TableId
            };

            await _reservationRepository.Add(reservation);

            TableReservationResponseDTO response = new()
            {
                Date = dto.Date,
                Email = dto.Email,
                Identification = table.Identification,
                Reserved = true,
                RestaurantName = table.Restaurant.Name
            };

            return response;
        }

        public async Task<TableReservationResponseDTO> Cancel(long tableId)
        {
            var table = await _tableRepository.GetById(tableId);
            if (table == null)
                _notification.AddNotification("Mesa", "Mesa não encontrada!");

            var reservation = await _reservationRepository.GetByTableId(tableId);
            if (reservation == null)
                _notification.AddNotification("Mesa", "Nenhuma reserva encontrada para esta mesa!");

            if(_notification.HasNotifications) return null;

            table.Status = EnumTableStatus.Free;
            TableReservationResponseDTO reseponse = new()
            {
                Date = reservation.Date,
                Email = reservation.Email,
                Identification = table.Identification,
                Reserved = true,
                RestaurantName = table.Restaurant.Name
            };

            await _reservationRepository.Delete(reservation);
            await _tableRepository.Update(table);

            return reseponse;
        }
    }
}
