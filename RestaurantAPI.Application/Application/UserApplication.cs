using System.Transactions;
using RestaurantAPI.Domain.DTO.User;
using RestaurantAPI.Domain.Interface.Application;
using RestaurantAPI.Domain.Interface.Services;

namespace RestaurantAPI.Application.Application
{
    public class UserApplication : IUserApplication
    {
        private readonly IUserService _userService;
        public UserApplication(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserCreateResponseDTO> Create(UserCreateDTO dto)
        {
            using TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var response = await _userService.Create(dto);
            transactionScope.Complete();
            return response;
        }
    }
}
