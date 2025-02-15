﻿using AutoMapper;
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
            var table = await _tableRepository.GetById(dto.Id) ?? new Table();
            table.Capacity = dto.Capacity;
            table.Identification = dto.Identification;

            if (dto.Id == 0)
            {
                table.RestaurantId = dto.RestaurantId;
                await _tableRepository.Add(table);
            }
            else
                await _tableRepository.Update(table);

            return _mapper.Map<TableResponseDTO>(table);
        }
        public async Task<TableDTO> DeleteById(long id) => _mapper.Map<TableDTO>(await _tableRepository.DeleteById(id));
        public async Task<bool> Release(TableReleaseDTO dto)
        {
            var table = await _tableRepository.GetById(dto.TableId);
            table.Reserved = false;
            await _tableRepository.Update(table);

            var reservation = await _reservationRepository.GetByTableId(table.Id);
            await _reservationRepository.Delete(reservation);

            return true;
        }
    }
}
