using AutoMapper;
using RestaurantAPI.Domain.Interface.Notification;

namespace RestaurantAPI.Service.Services.Base
{
    public class BaseService
    {
        protected readonly IMapper _mapper;

        public BaseService(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
