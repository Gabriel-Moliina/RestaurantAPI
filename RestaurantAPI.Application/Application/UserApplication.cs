using System.Transactions;
using FluentValidation;
using RestaurantAPI.Application.Application.Base;
using RestaurantAPI.Domain.DTO.User;
using RestaurantAPI.Domain.Interface.Application;
using RestaurantAPI.Domain.Interface.Notification;
using RestaurantAPI.Domain.Interface.Services;

namespace RestaurantAPI.Application.Application
{
    public class UserApplication : BaseApplication, IUserApplication
    {
        private readonly IUserService _userService;
        private readonly IValidator<UserCreateDTO> _validatorUserCreate;
        private readonly IValidator<UserLoginDTO> _validatorUserLogin;
        public UserApplication(INotification notification,
            IUserService userService,
            IValidator<UserCreateDTO> validatorUserCreate,
            IValidator<UserLoginDTO> validatorUserLogin
            ) : base(notification)
        {
            _userService = userService;
            _validatorUserCreate = validatorUserCreate;
            _validatorUserLogin = validatorUserLogin;
        }

        public async Task<UserCreateResponseDTO> Create(UserCreateDTO dto)
        {
            _notification.AddNotifications(await _validatorUserCreate.ValidateAsync(dto));
            if (_notification.HasNotifications) return null;

            using TransactionScope transactionScope = GetTransactionScopeAsyncEnabled();
            var response = await _userService.Create(dto);
            transactionScope.Complete();
            return response;
        }

        public async Task<UserLoginResponseDTO> Login(UserLoginDTO dto)
        {
            _notification.AddNotifications(await _validatorUserLogin.ValidateAsync(dto));
            if (_notification.HasNotifications) return null;

            return await _userService.Login(dto);
        }
    }
}
