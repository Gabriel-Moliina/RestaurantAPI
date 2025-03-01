﻿using AutoMapper;
using RestaurantAPI.Domain.DTO.Reservation;
using RestaurantAPI.Domain.Entities;

namespace RestaurantAPI.Domain.Mapper
{
    public class ReservationMapper : Profile
    {
        public ReservationMapper()
        {
            CreateMap<Reservation, CreateReservationDTO>()
                .ForPath(x => x.TableId, y => y.MapFrom(x => x.Table.Id));
            CreateMap<Reservation, ReservationDTO>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => TimeZoneInfo.ConvertTimeFromUtc(src.Date, TimeZoneInfo.Local)))
                .ForPath(x => x.TableId, y => y.MapFrom(x => x.Table.Id))
                .ForPath(x => x.Identification, y => y.MapFrom(x => x.Table.Identification));
        }
    }
}
