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
            _restaurantRepository = restaurantRepository;
            _tokenService = tokenService;
        }

        public async Task<List<RestaurantDTO>> GetByUserId(long id) => _mapper.Map<List<RestaurantDTO>>(await _restaurantRepository.GetByUserId(id));
        public async Task<RestaurantDTO> GetById(long restaurantId) => _mapper.Map<RestaurantDTO>(await _restaurantRepository.GetById(restaurantId));
        public async Task<RestaurantDTO> SaveOrUpdate(RestaurantCreateDTO dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            restaurant.UserId = _tokenService.GetUser().Id;

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
