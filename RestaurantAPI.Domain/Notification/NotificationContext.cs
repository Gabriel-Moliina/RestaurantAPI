using FluentValidation.Results;
using RestaurantAPI.Domain.DTO.Notification;
using RestaurantAPI.Domain.Interface.Notification;

namespace RestaurantAPI.Domain.Notification
{
    public class NotificationContext : INotification
    {
        private readonly List<NotificationDTO> _notifications;
        public IReadOnlyCollection<NotificationDTO> Notifications => _notifications;
        public bool HasNotifications => _notifications.Any();

        public NotificationContext()
        {
            _notifications = new List<NotificationDTO>();
        }

        public void AddNotification(string key, string message)
        {
            _notifications.Add(new NotificationDTO(key, message));
        }

        public void AddNotification(NotificationDTO notification)
        {
            _notifications.Add(notification);
        }

        public void AddNotifications(IList<NotificationDTO> notifications)
        {
            _notifications.AddRange(notifications);
        }

        public void AddNotifications(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                AddNotification(error.PropertyName, error.ErrorMessage);
            }
        }
    }
}
