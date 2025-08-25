using DomainProposta.Entities;
using DomainProposta.Interfaces;
using Microsoft.Extensions.Hosting;

namespace DomainProposta.Services
{
    public class KafkaConsumerHostedService : BackgroundService
    {
        private readonly IEventConsumer _consumer;

        public KafkaConsumerHostedService(IEventConsumer consumer)
        {
            _consumer = consumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _consumer.ConsumirAsync<PropostaCriadaEvent>(async (evento) =>
            {
                Console.WriteLine($"📩 Evento recebido: {evento.Id} - Status: {evento.Status}");

                // Aqui você pode disparar um Command ou atualizar MongoDB, etc.
                await Task.CompletedTask;

            }, stoppingToken);
        }
    }
}
