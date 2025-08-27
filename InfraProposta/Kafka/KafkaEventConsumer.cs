using Confluent.Kafka;
using DomainProposta.Config;
using DomainProposta.Entities;
using DomainProposta.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace InfraProposta.Kafka
{
    public class KafkaEventConsumer : IEventConsumer
    {
        private readonly ConsumerConfig _config;
        private readonly string _topic;
        

        public KafkaEventConsumer(IOptions<KafkaSettings> kafkaSettings)
        {
            var settings = kafkaSettings.Value;

            _config = new ConsumerConfig
            {
                BootstrapServers = settings.BootstrapServers,
                GroupId = settings.GroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _topic = settings.Topic;
        }
        public async Task ConsumirAsync<Proposta>(Func<Proposta, Task> onMessage, CancellationToken cancellationToken)
        {
            using var consumer = new ConsumerBuilder<Ignore, string>(_config).Build();
            consumer.Subscribe(_topic);

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var cr = consumer.Consume(cancellationToken);
                    var evento = JsonSerializer.Deserialize<Proposta>(cr.Message.Value);
                    if (evento != null)
                    {
                        await onMessage(evento);
                    }
                }
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine($"Erro ao desserializar: {ex.Message}");
            }
            finally
            {
                consumer.Close();
            }
        }
    }
}
