namespace RestaurantAPI.Domain.Interface.Messaging
{
    public interface IRabbitMQSettings
    {
        string HostName { get; }
        string UserName { get; }
        string Password { get; }
    }
}
