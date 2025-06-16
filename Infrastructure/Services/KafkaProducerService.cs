using Application.Interfaces.IService;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Threading.Tasks;

public class KafkaProducerService : IKafkaProducerService
{
    private readonly string _bootstrapServers;
    private readonly string _topic;

    public KafkaProducerService(IConfiguration configuration)
    {
        _bootstrapServers = configuration["Kafka:BootstrapServers"];
        _topic = configuration["Kafka:EmailTopic"];
    }

    public async Task SendEmailMessageAsync(EmailMessageDto emailMessage)
    {
        var config = new ProducerConfig { BootstrapServers = _bootstrapServers };
        using var producer = new ProducerBuilder<Null, string>(config).Build();

        var messageJson = JsonSerializer.Serialize(emailMessage);

        var result = await producer.ProduceAsync(_topic, new Message<Null, string> { Value = messageJson });

        // Log ou qualquer ação extra
        Console.WriteLine($"Mensagem enviada: {messageJson}");
    }
}