using AutoMapper;
using RestaurantAPI.Domain.DTO.Restaurant;
using RestaurantAPI.Domain.Entities;

namespace RestaurantAPI.Domain.Mapper
{
    public class RestaurantMapper : Profile
    {
        public RestaurantMapper()
        {
            CreateMap<RestaurantSaveDTO, Restaurant>();
            CreateMap<Restaurant, RestaurantDTO>();
            CreateMap<Restaurant, RestaurantDTO>()
                .ForMember(x => x.Tables, y => y.MapFrom(x => x.Tables));
        }
    }
}
