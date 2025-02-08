using AutoMapper;
using RestaurantAPI.Domain.DTO.Messaging;
using RestaurantAPI.Domain.Interface.Notification;
using RestaurantAPI.Domain.Interface.Repository;
using RestaurantAPI.Domain.Interface.Services;
using RestaurantAPI.Service.Services.Base;

namespace RestaurantAPI.Service.Services
{
    public class ReservationService : BaseService, IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        public ReservationService(IMapper mapper, 
            INotification notification,
            IReservationRepository reservationRepository,
            EmailSender email) : base(mapper, notification)
        {
            _reservationRepository = reservationRepository;
        }

        public Task CreateReserve(ReserveTableMessage reserveMessage)
        {
            throw new NotImplementedException();
        }
    }
}
