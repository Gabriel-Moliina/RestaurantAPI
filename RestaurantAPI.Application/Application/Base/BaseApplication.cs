using System.Transactions;
using RestaurantAPI.Domain.Interface.Application.Base;
using RestaurantAPI.Domain.Interface.Notification;

namespace RestaurantAPI.Application.Application.Base
{
    public class BaseApplication : IBaseApplication
    {
        protected readonly INotification _notification;
        public BaseApplication(INotification notification)
        {
            _notification = notification;
        }
        protected TransactionScope GetTransactionScopeAsyncEnabled() => new(TransactionScopeAsyncFlowOption.Enabled);
    }
}
