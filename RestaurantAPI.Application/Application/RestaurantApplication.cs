using System.Transactions;
using RestaurantAPI.Domain.DTO.Restaurant;
using RestaurantAPI.Domain.Interface.Application;
using RestaurantAPI.Domain.Interface.Services;

namespace RestaurantAPI.Application.Application
{
    public class RestaurantApplication : IRestaurantApplication
    {
        private readonly IRestaurantService _restaurantService;
        public RestaurantApplication(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        public async Task<List<RestaurantDTO>> Get() => await _restaurantService.Get();
        public async Task<RestaurantDTO> GetById(long restaurantId) => await _restaurantService.GetById(restaurantId);
        public async Task<List<RestaurantDTO>> GetByUserId(long userId) => await _restaurantService.GetByUserId(userId);
        public async Task<RestaurantDTO> Create(RestaurantCreateDTO dto)
        {
            using TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var response = await _restaurantService.Create(dto);
            transactionScope.Complete();
            return response;
        }
        public async Task<RestaurantDTO> DeleteById(long id)
        {
            using TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var restaurant = await _restaurantService.DeleteById(id);
            transactionScope.Complete();
            return restaurant;
        }
    }
}
