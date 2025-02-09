namespace RestaurantAPI.Domain.Interface.Email
{
    public interface IEmailSettings
    {
        string SMTP { get; }
        int Port { get;  }
        string Email { get; }
        string Password { get; }
        bool SSL { get; }
    }
}
