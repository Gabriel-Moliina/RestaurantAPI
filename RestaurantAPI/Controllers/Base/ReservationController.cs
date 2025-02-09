using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Domain.DTO.Reservation;
using RestaurantAPI.Domain.DTO.Table;
using RestaurantAPI.Domain.Interface.Application;
using RestaurantAPI.Domain.Interface.Notification;
using RestaurantAPI.ViewModels;

namespace RestaurantAPI.Controllers.Base
{
    [Route("api/[controller]")]
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

        [HttpPost]
        public Task<ActionResult<ResponseApiViewModel<TableReservationResponseDTO>>> Create([FromBody] TableReservationDTO dto)
        {
            return Execute(async () => await _reservationApplication.Create(dto));
        }

        [HttpDelete("CancelReservation/{tableId}")]
        public Task<ActionResult<ResponseApiViewModel<TableReservationResponseDTO>>> Cancel(long tableId)
        {
            return Execute(async () => await _reservationApplication.Cancel(tableId));
        }
    }
}
