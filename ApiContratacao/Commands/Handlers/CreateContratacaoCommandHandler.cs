using ApiContratacao.Commands;
using DomainContratacao.Entities;
using DomainContratacao.Enums;
using DomainContratacao.Interfaces;
using InfraContratacao.Repositories;
using MediatR;

namespace ApiContratacao.Commands.Handlers
{
    public class CreateContratacaoCommandHandler : IRequestHandler<CreateContratacaoCommand, Result>
    {
        private readonly ICacheService _cacheService;
        private readonly IContratacaoRepository _contratacaoRepository;
        //private readonly IEventBus _eventBus;

        public CreateContratacaoCommandHandler(ICacheService cacheService, IContratacaoRepository propostaRepository)
        {
            _cacheService = cacheService;
            _contratacaoRepository = propostaRepository;
        }

        public async Task<Result> Handle(CreateContratacaoCommand request, CancellationToken cancellationToken)
        {
            var proposta = await _cacheService.GetAsync<Proposta>($"proposta:{request.IdCliente}");
            if (proposta == null)
                return Result.Fail("Proposta não encontrada ou não aprovada.", "ValidationError");

            proposta.Status = StatusProposta.Contratada;

            //Remover e utilizar Kafka para comunicar a alteração de status
           // await _eventBus.PublicarAsync("proposta-contratada-topic", proposta);
            await _contratacaoRepository.UpdateProposta(proposta);

            await _contratacaoRepository.InsertContratacao(new Contratacoes
            {
                Id = Guid.NewGuid(),
                PropostaId = proposta.Id,
                DataContratacao = DateTime.UtcNow
            });

            return Result.Ok(proposta.Id);

        }
    }
}
