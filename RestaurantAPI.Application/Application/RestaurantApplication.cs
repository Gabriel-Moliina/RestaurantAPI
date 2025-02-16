using System.Transactions;
using FluentValidation;
using RestaurantAPI.Application.Application.Base;
using RestaurantAPI.Domain.DTO.Restaurant;
using RestaurantAPI.Domain.Interface.Application;
using RestaurantAPI.Domain.Interface.Notification;
using RestaurantAPI.Domain.Interface.Services;
using RestaurantAPI.Domain.Interface.Token;

namespace RestaurantAPI.Application.Application
{
    public class RestaurantApplication : BaseApplication, IRestaurantApplication
    {
        private readonly IRestaurantService _restaurantService;
        private readonly IValidator<RestaurantSaveDTO> _validatorRestaurantCreate;
        private readonly IValidator<RestaurantDeleteDTO> _validatorRestaurantDelete;
        private readonly ITokenService _tokenService;
        public RestaurantApplication(INotification notification,
            IRestaurantService restaurantService,
            IValidator<RestaurantSaveDTO> validatorRestaurantCreate,
            IValidator<RestaurantDeleteDTO> validatorRestaurantDelete,
            ITokenService tokenService
            ) : base(notification)
        {
            _restaurantService = restaurantService;
            _tokenService = tokenService;
            _validatorRestaurantCreate = validatorRestaurantCreate;
            _validatorRestaurantDelete = validatorRestaurantDelete;
        }

        public async Task<List<RestaurantDTO>> Get() => await _restaurantService.GetByUserId(_tokenService.GetUserId);
        public async Task<RestaurantDTO> GetById(long restaurantId) => await _restaurantService.GetByIdAndUserId(restaurantId, _tokenService.GetUserId);
        public async Task<RestaurantDTO> SaveOrUpdate(RestaurantSaveDTO dto)
        {
            _notification.AddNotifications(await _validatorRestaurantCreate.ValidateAsync(dto));
            if (_notification.HasNotifications) return null;

            using TransactionScope transactionScope = GetTransactionScopeAsyncEnabled();
            var response = await _restaurantService.SaveOrUpdate(dto);
            transactionScope.Complete();
            return response;
        }
        public async Task<RestaurantDTO> DeleteById(long id)
        {
            _notification.AddNotifications(await _validatorRestaurantDelete.ValidateAsync(new RestaurantDeleteDTO(id)));
            if (_notification.HasNotifications) return null;

            using TransactionScope transactionScope = GetTransactionScopeAsyncEnabled();
            var restaurant = await _restaurantService.DeleteById(id);
            transactionScope.Complete();
            return restaurant;
        }
    }
}
