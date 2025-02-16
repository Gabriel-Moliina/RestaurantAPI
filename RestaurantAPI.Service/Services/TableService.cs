using AutoMapper;
using RestaurantAPI.Domain.DTO.Table;
using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interface.Repository;
using RestaurantAPI.Domain.Interface.Services;
using RestaurantAPI.Service.Services.Base;

namespace RestaurantAPI.Service.Services
{
    public class TableService : BaseService, ITableService
    {
        private readonly ITableRepository _tableRepository;
        private readonly IReservationRepository _reservationRepository;
        public TableService(IMapper mapper,
            ITableRepository tableRepository,
            IReservationRepository reservationRepository) : base(mapper)
        {
            _tableRepository = tableRepository;
            _reservationRepository = reservationRepository;
        }

        public async Task<TableDTO> GetById(long id) => _mapper.Map<TableDTO>(await _tableRepository.GetById(id));
        public async Task<List<TableDTO>> GetByRestaurantId(long restaurantId) => _mapper.Map<List<TableDTO>>(await _tableRepository.GetByRestaurantId(restaurantId));
        public async Task<TableResponseDTO> SaveOrUpdate(TableSaveDTO dto)
        {
            if (dto == null)
                return null;

            Table response;
            if (dto.Id == 0)
            {
                var table = _mapper.Map<Table>(dto);
                response = await _tableRepository.Add(table);
            }
            else
            {
                var entity = await _tableRepository.GetById(dto.Id);
                entity.Capacity = dto.Capacity;
                entity.Identification = dto.Identification;

                response = await _tableRepository.Update(entity);
            }

            return _mapper.Map<TableResponseDTO>(response);
        }
        public async Task<TableDTO> DeleteById(long id) => _mapper.Map<TableDTO>(await _tableRepository.DeleteById(id));
        public async Task<bool> Release(TableReleaseDTO dto)
        {
            var table = await _tableRepository.GetById(dto.TableId);
            if (table == null) return false;
            table.Reserved = false;
            await _tableRepository.Update(table);

            var reservation = await _reservationRepository.GetByTableId(table.Id);
            if (reservation == null) return false;
            await _reservationRepository.Delete(reservation);

            return true;
        }
    }
}
