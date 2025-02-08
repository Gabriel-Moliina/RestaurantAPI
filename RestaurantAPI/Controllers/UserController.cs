using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Controllers.Base;
using RestaurantAPI.Domain.DTO.User;
using RestaurantAPI.Domain.Interface.Application;
using RestaurantAPI.Domain.Interface.Notification;
using RestaurantAPI.ViewModels;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserApplication _userApplication;
        public UserController(IUserApplication userApplication,
            INotification notification) : base(notification)
        {
            _userApplication = userApplication;
        }

        [HttpGet]
        public Task<ActionResult<ResponseApiViewModel<List<UserDTO>>>> Get()
        {
            return Execute(async () => await _userApplication.Get());
        }

        [HttpGet("{id}")]
        public Task<ActionResult<ResponseApiViewModel<UserDTO>>> GetById(long id)
        {
            return Execute(async () => await _userApplication.GetById(id));
        }

        [HttpPost("Login")]
        public Task<ActionResult<ResponseApiViewModel<UserLoginResponseDTO>>> Login([FromBody] UserLoginDTO user)
        {
            return Execute(async () => await _userApplication.Login(user));
        }

        [HttpPost("Create")]
        public Task<ActionResult<ResponseApiViewModel<UserCreateResponseDTO>>> Create([FromBody] UserCreateDTO user)
        {
            return Execute(async () => await _userApplication.Create(user));
        }
    }
}
