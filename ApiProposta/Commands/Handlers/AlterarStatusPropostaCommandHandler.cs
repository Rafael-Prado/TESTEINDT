using DomainProposta.Entities;
using DomainProposta.Interfaces;
using DomainProposta.Validator;
using MediatR;

namespace ApiProposta.Commands.Handlers
{
    public class AlterarStatusPropostaCommandHandler : IRequestHandler<AlterarStatusPropostaCommand, Result>
    {
        private readonly IPropostaRepository _repository;
        private readonly IMongoService _mongoService;

        public AlterarStatusPropostaCommandHandler(IPropostaRepository repository, IMongoService mongoService = null)
        {
            _repository = repository;
            _mongoService = mongoService;
        }

        public async Task<Result> Handle(AlterarStatusPropostaCommand request, CancellationToken cancellationToken)
        {
            var proposta = await _repository.ObterPorIdAsync(request.PropostaId);

            if (proposta == null)
                return Result.Fail("Proposta não encontrada.", "PropostaNãoEncontrada");

            proposta.Status = request.Status;

            var validator = new PropostaValidator();
            var result = validator.Validate(proposta);
            if (!result.IsValid)
            {
                return Result.Fail(result.Errors.FirstOrDefault()?.ErrorMessage, "ValidationError");
            }

            await _repository.AtualizarAsync(proposta);

            if (_mongoService != null)
                await _mongoService.SaveHistoricoPropostaAsync(new PropostaMongo(
                     proposta.Id, 
                     proposta.IdCliente, 
                     proposta.IdProduto,                     
                     proposta.Valor,
                     proposta.Status
                    ));

            return Result.Ok(proposta.Id);
        }
    }
}
