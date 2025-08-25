using Confluent.Kafka;
using DomainProposta.Interfaces;
using System.Text.Json;

namespace InfraProposta.Kafka
{
    public class KafkaEventConsumer : IEventConsumer
    {
        private readonly ConsumerConfig _config;
        private readonly string _topic;

        public KafkaEventConsumer(ConsumerConfig config, string topic)
        {
            _config = config;
            _topic = topic;
        }
        public async Task ConsumirAsync<T>(Func<T, Task> onMessage, CancellationToken cancellationToken)
        {
            using var consumer = new ConsumerBuilder<Ignore, string>(_config).Build();
            consumer.Subscribe(_topic);

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var cr = consumer.Consume(cancellationToken);
                    var mensagem = JsonSerializer.Deserialize<T>(cr.Message.Value);

                    if (mensagem != null)
                        await onMessage(mensagem);
                }
            }
            catch (OperationCanceledException)
            {
                // encerrando graceful
            }
            finally
            {
                consumer.Close();
            }
        }
    }
}
