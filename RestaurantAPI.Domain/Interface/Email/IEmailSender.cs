using RestaurantAPI.Domain.DTO.Messaging;

namespace RestaurantAPI.Domain.Interface.Email
{
    public interface IEmailSender
    {
        Task SendEmail(EmailDTO email);
    }
}
