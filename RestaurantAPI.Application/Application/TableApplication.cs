using System.Transactions;
using RestaurantAPI.Domain.DTO.Table;
using RestaurantAPI.Domain.Interface.Application;
using RestaurantAPI.Domain.Interface.Messaging;
using RestaurantAPI.Domain.Interface.Services;

namespace RestaurantAPI.Application.Application
{
    public class TableApplication : ITableApplication
    {
        private readonly ITableService _tableService;
        private readonly IRabbitMQSender _rabbitMQSender;
        public TableApplication(ITableService tableService,
            IRabbitMQSender rabbitMQSender)
        {
            _tableService = tableService;
            _rabbitMQSender = rabbitMQSender;
        }

        public async Task<TableDTO> GetById(long id) => await _tableService.GetById(id);
        public async Task<TableResponseDTO> Create(TableDTO dto)
        {
            using TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var response = await _tableService.Create(dto);
            transactionScope.Complete();
            return response;
        }
        public async Task<TableDTO> DeleteById(long id) => await _tableService.DeleteById(id);

        public async Task<bool> ChangeStatus(TableChangeStatusDTO dto)
        {
            using TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            bool response = await _tableService.ChangeStatus(dto);
            transactionScope.Complete();
            return response;
        }
    }
}
