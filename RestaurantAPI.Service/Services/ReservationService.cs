using AutoMapper;
using RestaurantAPI.Domain.DTO.Messaging;
using RestaurantAPI.Domain.Interface.Notification;
using RestaurantAPI.Domain.Interface.Repository;
using RestaurantAPI.Domain.Interface.Services;
using RestaurantAPI.Email.Sender;
using RestaurantAPI.Service.Services.Base;

namespace RestaurantAPI.Service.Services
{
    public class ReservationService : BaseService, IReservationService
    {
        private readonly EmailSender _emailSender;
        public ReservationService(IMapper mapper, 
            INotification notification,
            EmailSender emailSender) : base(mapper, notification)
        {
            _emailSender = emailSender;
        }

        public Task CreateReserve(ReserveTableMessage reserveMessage)
        {
            return Task.CompletedTask;
        }
    }
}
