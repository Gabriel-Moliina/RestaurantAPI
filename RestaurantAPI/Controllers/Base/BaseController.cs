using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Domain.Interface.Notification;
using RestaurantAPI.ViewModels;

namespace RestaurantAPI.Controllers.Base
{
    public class BaseController : ControllerBase
    {
        protected INotification _notification;
        public BaseController(INotification notification)
        {
            _notification = notification;
        }
        protected async Task<ActionResult<ResponseApiViewModel<T>>> Execute<T>(Func<Task<T>> func)
        {
            try
            {
                var data = await func();
                var response = new ResponseApiViewModel<T>(data, _notification);
                var code = response.Success ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest;
                return StatusCode(code, response);
            }
            catch (Exception ex)
            {
                var response = new ResponseApiViewModel<T>(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
