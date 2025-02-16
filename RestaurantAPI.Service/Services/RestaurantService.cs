using AutoMapper;
using FluentValidation;
using RestaurantAPI.Domain.DTO.Restaurant;
using RestaurantAPI.Domain.Entities;
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

        public RestaurantService(IRestaurantRepository restaurantRepository,
            ITokenService tokenService,
            IMapper mapper) : base(mapper)
        {
            _tokenService = tokenService;
            _restaurantRepository = restaurantRepository;
        }

        public async Task<List<RestaurantDTO>> GetByUserId(long id) => _mapper.Map<List<RestaurantDTO>>(await _restaurantRepository.GetByUserId(id));
        public async Task<RestaurantDTO> GetByIdAndUserId(long restaurantId, long userId) => _mapper.Map<RestaurantDTO>(await _restaurantRepository.GetByIdAndUserId(restaurantId, userId));
        public async Task<RestaurantDTO> SaveOrUpdate(RestaurantSaveDTO dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            if (restaurant == null) 
                return null;

            restaurant.UserId = _tokenService.GetUserId;

            if (restaurant.Id == 0)
                await _restaurantRepository.Add(restaurant);
            else
                await _restaurantRepository.Update(restaurant);

            return _mapper.Map<RestaurantDTO>(restaurant);
        }
        public async Task<RestaurantDTO> DeleteById(long id)
        {
            var response = _mapper.Map<RestaurantDTO>(await _restaurantRepository.DeleteById(id));
            return response;
        }
    }
}
