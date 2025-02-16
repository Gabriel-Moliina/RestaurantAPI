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
            CreateMap<Table, TableDTO>()
                .ForPath(x => x.ReservationId, y => y.MapFrom(x => x.Reservation.Id));
            CreateMap<Table, TableResponseDTO>();
            CreateMap<TableDTO, Table>();
            CreateMap<TableSaveDTO, Table>();
        }
    }
}
