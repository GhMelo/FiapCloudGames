using Application.Interfaces.IService;
using Confluent.Kafka;

namespace Application.Services
{
    public class KafkaProducerService : IKafkaProducerService
    {
        private readonly string _bootstrapServers;

        public KafkaProducerService(string bootstrapServers)
        {
            _bootstrapServers = bootstrapServers;
        }

        public Task SendEmailMessageAsync(EmailMessageDto emailMessage)
        {
            throw new NotImplementedException();
        }

        public async Task SendMessageAsync(string topic, string message)
        {
            var config = new ProducerConfig { BootstrapServers = _bootstrapServers };

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                var result = await producer.ProduceAsync(
                    topic,
                    new Message<Null, string> { Value = message }
                );

                Console.WriteLine($"Mensagem '{message}' enviada para {result.TopicPartitionOffset}");
            }
        }
    }
}