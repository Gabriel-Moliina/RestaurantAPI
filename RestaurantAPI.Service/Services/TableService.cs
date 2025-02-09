using AutoMapper;
using FluentValidation;
using RestaurantAPI.Domain.DTO.Table;
using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interface.Repository;
using RestaurantAPI.Domain.Interface.Services;
using RestaurantAPI.Domain.ValueObjects.Table;
using RestaurantAPI.Service.Services.Base;

namespace RestaurantAPI.Service.Services
{
    public class TableService : BaseService, ITableService
    {
        private readonly ITableRepository _tableRepository;
        public TableService(IMapper mapper,
            ITableRepository tableRepository) : base(mapper)
        {
            _tableRepository = tableRepository;
        }

        public async Task<TableDTO> GetById(long id) => _mapper.Map<TableDTO>(await _tableRepository.GetById(id));
        public async Task<TableResponseDTO> Create(TableDTO dto)
        {
            var table = _mapper.Map<Table>(dto);

            return _mapper.Map<TableResponseDTO>(await _tableRepository.Add(table));
        }
        public async Task<TableDTO> DeleteById(long id) => _mapper.Map<TableDTO>(await _tableRepository.DeleteById(id));

        public async Task<bool> ChangeStatus(TableChangeStatusDTO dto)
        {
            var table = await _tableRepository.GetById(dto.TableId);
            table.Status = dto.Status;
            await _tableRepository.Update(table);

            return true;
        }
    }
}
