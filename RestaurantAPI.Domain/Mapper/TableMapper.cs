using AutoMapper;
using RestaurantAPI.Domain.DTO.Table;
using RestaurantAPI.Domain.Entities;

namespace RestaurantAPI.Domain.Mapper
{
    public class TableMapper : Profile
    {
        public TableMapper()
        {
            CreateMap<Table, TableDTO>();
            CreateMap<Table, TableResponseDTO>();
            CreateMap<TableDTO, Table>();
            CreateMap<TableCreateDTO, Table>();
        }
    }
}
