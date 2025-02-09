using RestaurantAPI.Domain.Builder;
using RestaurantAPI.Domain.DTO.Messaging;

namespace RestaurantAPI.Domain.Interface.Builder
{
    public interface IEmailBuilder : IBaseBuilder<EmailDTO>
    {
        EmailBuilder WithName(string Name);
        EmailBuilder WithSubject(string Subject);
        EmailBuilder WithReceiver(string Receiver);
        EmailBuilder WithMessage(string Message);

    }
}
