using FluentValidation.Results;
using RestaurantAPI.Domain.DTO.Notification;

namespace RestaurantAPI.Domain.Interface.Notification
{
    public interface INotification
    {
        IReadOnlyCollection<NotificationDTO> Notifications { get; }
        bool HasNotifications { get; }
        void AddNotification(string key, string message);
        void AddNotification(NotificationDTO notification);
        void AddNotifications(IList<NotificationDTO> notifications);
        void AddNotifications(ValidationResult validationResult);
    }
}
