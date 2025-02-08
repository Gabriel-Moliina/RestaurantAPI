﻿using AutoMapper;
using RestaurantAPI.Domain.DTO.User;
using RestaurantAPI.Domain.Entities;

namespace RestaurantAPI.Domain.Mapper
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UserCreateDTO, User>();
            CreateMap<User, UserCreateResponseDTO>();
        }
    }
}
