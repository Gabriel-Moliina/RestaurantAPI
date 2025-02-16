using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Controllers.Base;
using RestaurantAPI.Domain.DTO.Reservation;
using RestaurantAPI.Domain.Interface.Application;
using RestaurantAPI.Domain.Interface.Notification;
using RestaurantAPI.ViewModels;

namespace RestaurantAPI.Controllers
{
    [Route("api/reservation")]
    [ApiController]
    [Authorize]
    public class ReservationController : BaseController
    {
        private readonly IReservationApplication _reservationApplication;
        public ReservationController(INotification notification,
            IReservationApplication reservationApplication) : base(notification)
        {
            _reservationApplication = reservationApplication;
        }

        [HttpGet("{id}")]
        public Task<ActionResult<ResponseApiViewModel<ReservationDTO>>> GetById(long id)
        {
            return Execute(async () => await _reservationApplication.GetById(id));
        }

        [HttpPost]
        public Task<ActionResult<ResponseApiViewModel<CreateReservationResponseDTO>>> Create([FromBody] CreateReservationDTO dto)
        {
            return Execute(async () => await _reservationApplication.Create(dto));
        }

        [HttpDelete("table/{tableId}")]
        public Task<ActionResult<ResponseApiViewModel<CreateReservationResponseDTO>>> Cancel(long tableId)
        {
            return Execute(async () => await _reservationApplication.Cancel(tableId));
        }
    }
}
