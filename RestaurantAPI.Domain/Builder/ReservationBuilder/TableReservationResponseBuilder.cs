﻿using RestaurantAPI.Domain.DTO.Table;
using RestaurantAPI.Domain.Interface.Builder;

namespace RestaurantAPI.Domain.Builder.ReservationBuilder
{
    public class TableReservationResponseBuilder : ITableReservationResponseBuilder
    {
        private readonly TableReservationResponseDTO _tableReservation;
        public TableReservationResponseBuilder()
        {
            _tableReservation = new TableReservationResponseDTO();
        }

        public ITableReservationResponseBuilder WithRestaurantName(string restaurantName)
        {
            _tableReservation.RestaurantName = restaurantName;
            return this;
        }
        public ITableReservationResponseBuilder WithIdentification(string identification)
        {
            _tableReservation.Identification = identification;
            return this;
        }
        public ITableReservationResponseBuilder WithEmail(string email)
        {
            _tableReservation.Email = email;
            return this;
        }
        public ITableReservationResponseBuilder WithDate(DateTime date)
        {
            _tableReservation.Date = date;
            return this;
        }
        public ITableReservationResponseBuilder WithReserved(bool reserved)
        {
            _tableReservation.Reserved = reserved;
            return this;
        }
        public TableReservationResponseDTO Build()
        {
            return _tableReservation;
        }
    }
}
