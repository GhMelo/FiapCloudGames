namespace Application.Interfaces.IService
{
    public interface IKafkaProducerService
    {
        Task SendEmailMessageAsync(EmailMessageDto emailMessage);
    }
}