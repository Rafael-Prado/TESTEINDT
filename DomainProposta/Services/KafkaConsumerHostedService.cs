using DomainProposta.Entities;
using DomainProposta.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DomainProposta.Services
{
    public class KafkaConsumerHostedService : BackgroundService
    {
        private readonly IEventConsumer _consumer;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMongoService _mongoService;

        public KafkaConsumerHostedService(IEventConsumer consumer, IMongoService mongoService, IServiceScopeFactory scopeFactory)
        {
            _consumer = consumer;
            _mongoService = mongoService;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _scopeFactory.CreateScope();
            var propostaRepository = scope.ServiceProvider.GetRequiredService<IPropostaRepository>();

            await _consumer.ConsumirAsync<Proposta>(async (evento) =>
            {
                await propostaRepository.AtualizarAsync(evento);

                await _mongoService.SaveHistoricoPropostaAsync(new PropostaMongo(
                                                 evento.Id,
                                                 evento.IdCliente,
                                                 evento.IdProduto,
                                                 evento.Valor,
                                                 evento.Status
                                                ));

            }, stoppingToken);
        }
    }
}
