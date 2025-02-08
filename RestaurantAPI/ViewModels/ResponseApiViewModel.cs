using RestaurantAPI.Domain.DTO.Notification;
using RestaurantAPI.Domain.Interface.Notification;

namespace RestaurantAPI.ViewModels
{
    public class ResponseApiViewModel<T>
    {
        public ResponseApiViewModel(T data, INotification notification)
        {
            Data = data;
            Success = !notification.HasNotifications;
            Messages = notification.Notifications.ToList();
        }
        public ResponseApiViewModel(Exception e)
        {
            Error = e.Message;
            Success = false;
        }
        public T Data { get; set; }
        public string Error { get; set; }
        public bool Success { get; set; }
        public IList<NotificationDTO> Messages { get; set; }
    }
}
