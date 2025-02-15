using AutoMapper;
using RestaurantAPI.Domain.DTO.Reservation;
using RestaurantAPI.Domain.DTO.Table;
using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interface.Builder;
using RestaurantAPI.Domain.Interface.Repository;
using RestaurantAPI.Domain.Interface.Services;
using RestaurantAPI.Service.Services.Base;

namespace RestaurantAPI.Service.Services
{
    public class ReservationService : BaseService, IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ITableRepository _tableRepository;
        private readonly IReservationBuilder _reservationBuilder;
        private readonly ITableReservationResponseBuilder _tableReservationResponseBuilder;
        public ReservationService(IMapper mapper,
            ITableRepository tableRepository,
            IReservationBuilder reservationBuilder, 
            ITableReservationResponseBuilder tableReservationResponseBuilder,
            IReservationRepository reservationRepository) : base(mapper)
        {
            _reservationBuilder = reservationBuilder;
            _tableReservationResponseBuilder = tableReservationResponseBuilder;
            _tableRepository = tableRepository;
            _reservationRepository = reservationRepository;
        }

        public async Task<TableReservationResponseDTO> Create(TableReservationDTO dto)
        {
            var table = await _tableRepository.GetById(dto.TableId);
            table.Reserved = true;
            await _tableRepository.Update(table);

            dto.Date = TimeZoneInfo.ConvertTime(dto.Date, TimeZoneInfo.Local).ToLocalTime();

            Reservation reservation = _reservationBuilder.
                WithDate(dto.Date).
                WithEmail(dto.Email).
                WithTableId(dto.TableId).
                Build();

            await _reservationRepository.Add(reservation);

            TableReservationResponseDTO response = _tableReservationResponseBuilder.
                WithDate(dto.Date).
                WithEmail(dto.Email).
                WithIdentification(table.Identification).
                WithReserved(table.Reserved).
                WithRestaurantName(table.Restaurant.Name).
                Build();

            return response;
        }

        public async Task<TableReservationResponseDTO> Cancel(long tableId)
        {
            var table = await _tableRepository.GetById(tableId);
            var reservation = await _reservationRepository.GetByTableId(tableId);

            table.Reserved = false;
            TableReservationResponseDTO reseponse = _tableReservationResponseBuilder.
                WithDate(reservation.Date).
                WithEmail(reservation.Email).
                WithIdentification(table.Identification).
                WithReserved(table.Reserved).
                WithRestaurantName(table.Restaurant.Name).
                Build();

            await _reservationRepository.Delete(reservation);
            await _tableRepository.Update(table);

            return reseponse;
        }
    }
}
