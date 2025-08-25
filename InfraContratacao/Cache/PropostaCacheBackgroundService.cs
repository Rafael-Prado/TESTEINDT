using DomainContratacao.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraContratacao.Cache
{
    public class PropostaCacheBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ICacheService _cacheService;
        private readonly ILogger<PropostaCacheBackgroundService> _logger;

        public PropostaCacheBackgroundService(
            IServiceProvider serviceProvider,
            ICacheService cacheService,
            ILogger<PropostaCacheBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _cacheService = cacheService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var propostaRepo = scope.ServiceProvider.GetRequiredService<IContratacaoRepository>();
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var propostas = await propostaRepo.GetPropostasAprovadasAsync();

                    foreach (var proposta in propostas)
                    {
                        var key = $"proposta:{proposta.IdCliente}";
                        await _cacheService.SetAsync(key, proposta, TimeSpan.FromMinutes(5));
                    }

                    _logger.LogInformation("✅ Cache de propostas atualizado: {count}", propostas.Count());
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao atualizar cache de propostas.");
                }

                await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken); // ⏱ roda a cada 2 min
            }
        }
    }
}
