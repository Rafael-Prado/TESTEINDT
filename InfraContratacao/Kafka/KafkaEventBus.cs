//using Confluent.Kafka;
//using DomainContratacao.Interfaces;
//using System.Text.Json;

//namespace InfraContratacao.Kafka
//{
//    public class KafkaEventBus : IEventBus
//    {
//        private readonly IProducer<Null, string> _producer;
//        private readonly string _defaultTopic;

//        public KafkaEventBus(string bootstrapServers, string defaultTopic)
//        {
//            var config = new ProducerConfig
//            {
//                BootstrapServers = bootstrapServers
//            };

//            _producer = new ProducerBuilder<Null, string>(config).Build();
//            _defaultTopic = defaultTopic;
//        }

//        public async Task PublicarAsync<T>(string topic, T @event)
//        {
//            var json = JsonSerializer.Serialize(@event);
//            var kafkaTopic = topic ?? _defaultTopic;

//            var message = new Message<Null, string> { Value = json };
//            await _producer.ProduceAsync(kafkaTopic, message);
//        }

//    }
//}
