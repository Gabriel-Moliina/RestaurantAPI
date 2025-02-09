using RestaurantAPI.Domain.DTO.Messaging;
using RestaurantAPI.Domain.Interface.Builder;

namespace RestaurantAPI.Domain.Builder
{
    public class EmailBuilder : IEmailBuilder
    {
        private readonly EmailDTO _email;
        public EmailBuilder()
        {
            _email = new EmailDTO();
        }
        public EmailBuilder WithName(string name)
        {
            _email.Name = name;
            return this;
        }
        public EmailBuilder WithSubject(string subject)
        {
            _email.Subject = subject;
            return this;
        }
        public EmailBuilder WithReceiver(string receiver)
        {
            _email.Receiver = receiver;
            return this;
        }
        public EmailBuilder WithMessage(string message)
        {
            _email.Message = message;
            return this;
        }
        public EmailDTO Build()
        {
            return _email;
        }
    }
}
