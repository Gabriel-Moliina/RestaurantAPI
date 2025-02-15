using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Controllers.Base;
using RestaurantAPI.Domain.DTO.User;
using RestaurantAPI.Domain.Interface.Application;
using RestaurantAPI.Domain.Interface.Notification;
using RestaurantAPI.ViewModels;

namespace RestaurantAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserApplication _userApplication;
        public UserController(IUserApplication userApplication,
            INotification notification) : base(notification)
        {
            _userApplication = userApplication;
        }

        [HttpPost("login")]
        public Task<ActionResult<ResponseApiViewModel<UserLoginResponseDTO>>> Login([FromBody] UserLoginDTO user)
        {
            return Execute(async () => await _userApplication.Login(user));
        }

        [HttpPost("create")]
        public Task<ActionResult<ResponseApiViewModel<UserCreateResponseDTO>>> Create([FromBody] UserCreateDTO user)
        {
            return Execute(async () => await _userApplication.Create(user));
        }
    }
}
