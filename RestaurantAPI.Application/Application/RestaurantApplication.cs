using System.Transactions;
using FluentValidation;
using RestaurantAPI.Application.Application.Base;
using RestaurantAPI.Domain.DTO.Restaurant;
using RestaurantAPI.Domain.Interface.Application;
using RestaurantAPI.Domain.Interface.Notification;
using RestaurantAPI.Domain.Interface.Services;

namespace RestaurantAPI.Application.Application
{
    public class RestaurantApplication : BaseApplication, IRestaurantApplication
    {
        private readonly IRestaurantService _restaurantService;
        private readonly IValidator<RestaurantCreateDTO> _validatorRestaurantCreate;
        private readonly IValidator<RestaurantDeleteDTO> _validatorRestaurantDelete;
        public RestaurantApplication(INotification notification,
            IRestaurantService restaurantService,
            IValidator<RestaurantCreateDTO> validatorRestaurantCreate,
            IValidator<RestaurantDeleteDTO> validatorRestaurantDelete
            ) : base(notification)
        {
            _restaurantService = restaurantService;
            _validatorRestaurantCreate = validatorRestaurantCreate;
            _validatorRestaurantDelete = validatorRestaurantDelete;
        }

        public async Task<List<RestaurantDTO>> Get() => await _restaurantService.Get();
        public async Task<RestaurantDTO> GetById(long restaurantId) => await _restaurantService.GetById(restaurantId);
        public async Task<RestaurantDTO> Create(RestaurantCreateDTO dto)
        {
            _notification.AddNotifications(await _validatorRestaurantCreate.ValidateAsync(dto));
            if (_notification.HasNotifications) return null;

            using TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var response = await _restaurantService.Create(dto);
            transactionScope.Complete();
            return response;
        }
        public async Task<RestaurantDTO> DeleteById(long id)
        {
            _notification.AddNotifications(await _validatorRestaurantDelete.ValidateAsync(new RestaurantDeleteDTO(id)));
            if (_notification.HasNotifications) return null;

            using TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var restaurant = await _restaurantService.DeleteById(id);
            transactionScope.Complete();
            return restaurant;
        }
    }
}
