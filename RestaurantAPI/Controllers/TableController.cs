using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Controllers.Base;
using RestaurantAPI.Domain.DTO.Table;
using RestaurantAPI.Domain.Interface.Application;
using RestaurantAPI.Domain.Interface.Notification;
using RestaurantAPI.ViewModels;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TableController : BaseController
    {
        private readonly ITableApplication _tableApplication;

        public TableController(INotification notification,
            ITableApplication tableApplication) : base(notification)
        {
            _tableApplication = tableApplication;
        }

        [HttpGet("{id}")]
        public Task<ActionResult<ResponseApiViewModel<TableDTO>>> GetById(long id)
        {
            return Execute(async () => await _tableApplication.GetById(id));
        }

        [HttpGet("GetByRestaurantId/{restaurantId}")]
        public Task<ActionResult<ResponseApiViewModel<List<TableDTO>>>> GetByRestaurantId(long restaurantId)
        {
            return Execute(async () => await _tableApplication.GetByRestaurantId(restaurantId));
        }

        [HttpPost]
        public Task<ActionResult<ResponseApiViewModel<TableResponseDTO>>> SaveOrUpdate([FromBody] TableSaveDTO table)
        {
            return Execute(async () => await _tableApplication.SaveOrUpdate(table));
        }

        [HttpPost("Release")]
        public Task<ActionResult<ResponseApiViewModel<bool>>> Release(TableChangeStatusDTO dto)
        {
            return Execute(async () => await _tableApplication.Release(dto));
        }

        [HttpDelete("{id}")]
        public Task<ActionResult<ResponseApiViewModel<TableDTO>>> DeleteById(long id)
        {
            return Execute(async () => await _tableApplication.DeleteById(id));
        }
    }
}
