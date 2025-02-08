using AutoMapper;
using RestaurantAPI.Domain.Interface.Notification;

namespace RestaurantAPI.Service.Services.Base
{
    public class BaseService
    {
        protected readonly IMapper _mapper;
        protected readonly INotification _notification;

        public BaseService(IMapper mapper, INotification notification)
        {
            _mapper = mapper;
            _notification = notification;
        }
    }
}
