﻿namespace RestaurantAPI.Domain.DTO.Reservation
{
    public class CreateReservationDTO
    {
        public long TableId { get; set; }
        public DateTime Date { get; set; }
        public string Email { get; set; }
    }
}
