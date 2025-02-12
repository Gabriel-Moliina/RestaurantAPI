using System.Transactions;
using FluentValidation;
using RestaurantAPI.Application.Application.Base;
using RestaurantAPI.Domain.DTO.Table;
using RestaurantAPI.Domain.Interface.Application;
using RestaurantAPI.Domain.Interface.Notification;
using RestaurantAPI.Domain.Interface.Services;

namespace RestaurantAPI.Application.Application
{
    public class TableApplication : BaseApplication, ITableApplication
    {
        private readonly IValidator<TableSaveDTO> _validatorTableCreate;
        private readonly IValidator<TableChangeStatusDTO> _validatorTableChangeStatus;
        private readonly ITableService _tableService;
        public TableApplication(INotification notification,
            ITableService tableService,
            IValidator<TableSaveDTO> validatorTableCreate,
            IValidator<TableChangeStatusDTO> validatorTableChangeStatus
            ) : base(notification)
        {
            _tableService = tableService;
            _validatorTableCreate = validatorTableCreate;
            _validatorTableChangeStatus = validatorTableChangeStatus;
        }

        public async Task<TableDTO> GetById(long id) => await _tableService.GetById(id);
        public async Task<List<TableDTO>> GetByRestaurantId(long restaurantId) => await _tableService.GetByRestaurantId(restaurantId);
        public async Task<TableResponseDTO> Create(TableSaveDTO dto)
        {
            _notification.AddNotifications(await _validatorTableCreate.ValidateAsync(dto));
            if (_notification.HasNotifications) return null;

            using TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var response = await _tableService.Create(dto);
            transactionScope.Complete();
            return response;
        }
        public async Task<TableDTO> DeleteById(long id) => await _tableService.DeleteById(id);

        public async Task<bool> Release(TableChangeStatusDTO dto)
        {
            _notification.AddNotifications(await _validatorTableChangeStatus.ValidateAsync(dto));
            if (_notification.HasNotifications) return false;

            using TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            bool response = await _tableService.Release(dto);
            transactionScope.Complete();
            return response;
        }
    }
}
