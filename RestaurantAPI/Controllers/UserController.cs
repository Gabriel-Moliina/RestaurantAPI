using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Controllers.Base;
using RestaurantAPI.Domain.DTO.User;
using RestaurantAPI.Domain.Interface.Application;
using RestaurantAPI.Domain.Interface.Notification;
using RestaurantAPI.Domain.Interface.Services;
using RestaurantAPI.ViewModels;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UserController : BaseController
    {
        private readonly IUserApplication _userApplication;
        public UserController(IUserApplication userApplication,
            INotification notification) : base(notification)
        {
            _userApplication = userApplication;
        }

        public Task<ActionResult<ResponseApiViewModel<UserCreateResponseDTO>>> CreateUser([FromBody] UserCreateDTO user)
        {
            return Execute(async () => await _userApplication.Create(user));
        }
    }
}
