using AutoMapper;
using FluentValidation;
using RestaurantAPI.Domain.DTO.Restaurant;
using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interface.Notification;
using RestaurantAPI.Domain.Interface.Repository;
using RestaurantAPI.Domain.Interface.Services;
using RestaurantAPI.Domain.Interface.Token;
using RestaurantAPI.Service.Services.Base;

namespace RestaurantAPI.Service.Services
{
    public class RestaurantService : BaseService, IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly ITokenService _tokenService;
        private readonly IValidator<RestaurantCreateDTO> _validatorRestaurantCreate;
        public RestaurantService(IRestaurantRepository restaurantRepository,
            ITokenService tokenService,
            IMapper mapper,
            INotification notification,
            IValidator<RestaurantCreateDTO> validatorRestaurantCreate) : base(mapper, notification)
        {
            _restaurantRepository = restaurantRepository;
            _tokenService = tokenService;
            _validatorRestaurantCreate = validatorRestaurantCreate;
        }

        public async Task<List<RestaurantDTO>> Get() => _mapper.Map<List<RestaurantDTO>>(await _restaurantRepository.Get());
        public async Task<RestaurantDTO> GetById(long restaurantId) => _mapper.Map<RestaurantDTO>(await _restaurantRepository.GetById(restaurantId));
        public async Task<List<RestaurantDTO>> GetByUserId(long userId) => _mapper.Map<List<RestaurantDTO>>(await _restaurantRepository.GetByUserId(userId));
        public async Task<RestaurantDTO> Create(RestaurantCreateDTO dto)
        {
            _notification.AddNotifications(await _validatorRestaurantCreate.ValidateAsync(dto));
            if (_notification.HasNotifications) return null;

            var restaurant = _mapper.Map<Restaurant>(dto);
            restaurant.UserId = _tokenService.GetUser().Id;

            return _mapper.Map<RestaurantDTO>(await _restaurantRepository.Add(restaurant));
        }
        public async Task<RestaurantDTO> DeleteById(long id)
        {
            var response = _mapper.Map<RestaurantDTO>(await _restaurantRepository.DeleteById(id));
            if (response == null)
                _notification.AddNotification("Restaurant", "Restaurant not found!");
            return response;
        }
    }
}
