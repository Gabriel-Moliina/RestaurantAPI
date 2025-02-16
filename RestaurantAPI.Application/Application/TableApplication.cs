using System.Transactions;
using FluentValidation;
using RestaurantAPI.Application.Application.Base;
using RestaurantAPI.Domain.DTO.Restaurant;
using RestaurantAPI.Domain.DTO.Table;
using RestaurantAPI.Domain.Interface.Application;
using RestaurantAPI.Domain.Interface.Notification;
using RestaurantAPI.Domain.Interface.Services;

namespace RestaurantAPI.Application.Application
{
    public class TableApplication : BaseApplication, ITableApplication
    {
        private readonly IValidator<TableSaveDTO> _validatorTableCreate;
        private readonly IValidator<TableReleaseDTO> _validatorTableChangeStatus;
        private readonly IValidator<TableDeleteDTO> _validatorTableDelete;
        private readonly ITableService _tableService;
        public TableApplication(INotification notification,
            ITableService tableService,
            IValidator<TableSaveDTO> validatorTableCreate,
            IValidator<TableReleaseDTO> validatorTableChangeStatus,
            IValidator<TableDeleteDTO> validatorTableDelete
            ) : base(notification)
        {
            _tableService = tableService;
            _validatorTableCreate = validatorTableCreate;
            _validatorTableChangeStatus = validatorTableChangeStatus;
            _validatorTableDelete = validatorTableDelete;
        }

        public async Task<TableDTO> GetById(long id) => await _tableService.GetById(id);
        public async Task<List<TableDTO>> GetByRestaurantId(long restaurantId) => await _tableService.GetByRestaurantId(restaurantId);
        public async Task<TableResponseDTO> SaveOrUpdate(TableSaveDTO dto)
        {
            _notification.AddNotifications(await _validatorTableCreate.ValidateAsync(dto));
            if (_notification.HasNotifications) return null;

            using TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var response = await _tableService.SaveOrUpdate(dto);
            transactionScope.Complete();
            return response;
        }
        public async Task<TableDTO> DeleteById(long id) {

            _notification.AddNotifications(await _validatorTableDelete.ValidateAsync(new TableDeleteDTO(id)));
            if (_notification.HasNotifications) return null;

            using TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var table = await _tableService.DeleteById(id);
            transactionScope.Complete();
            return table;
        } 

        public async Task<bool> Release(TableReleaseDTO dto)
        {
            _notification.AddNotifications(await _validatorTableChangeStatus.ValidateAsync(dto));
            if (_notification.HasNotifications) return false;

            using TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            bool response = await _tableService.Release(dto);

            if(response) transactionScope.Complete();
            return response;
        }
    }
}
