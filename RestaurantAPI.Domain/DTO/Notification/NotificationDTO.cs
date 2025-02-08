namespace RestaurantAPI.Domain.DTO.Notification
{
    public class NotificationDTO
    {
        public NotificationDTO(string key, string message)
        {
            Key = key;
            Message = message;
        }

        public string Key { get; set; }
        public string Message { get; set; }
    }
}
