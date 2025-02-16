using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Controllers.Base;
using RestaurantAPI.Domain.DTO.Restaurant;
using RestaurantAPI.Domain.Interface.Application;
using RestaurantAPI.Domain.Interface.Notification;
using RestaurantAPI.ViewModels;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant")]
    [ApiController]
    [Authorize]
    public class RestaurantController : BaseController
    {
        private readonly IRestaurantApplication _restaurantApplication;
        public RestaurantController(INotification notification,
            IRestaurantApplication restaurantApplication) : base(notification)
        {
            _restaurantApplication = restaurantApplication;
        }

        [HttpGet]
        public Task<ActionResult<ResponseApiViewModel<List<RestaurantDTO>>>> Get()
        {
            return Execute(async () => await _restaurantApplication.Get());
        }

        [HttpGet("{id}")]
        public Task<ActionResult<ResponseApiViewModel<RestaurantDTO>>> GetById(long id)
        {
            return Execute(async () => await _restaurantApplication.GetById(id));
        }

        [HttpPost]
        public Task<ActionResult<ResponseApiViewModel<RestaurantDTO>>> SaveOrUpdate([FromBody] RestaurantSaveDTO restaurant)
        {
            return Execute(async () => await _restaurantApplication.SaveOrUpdate(restaurant));
        }

        [HttpDelete("{id}")]
        public Task<ActionResult<ResponseApiViewModel<RestaurantDTO>>> DeleteById(long id)
        {
            return Execute(async () => await _restaurantApplication.DeleteById(id));
        }
    }
}
