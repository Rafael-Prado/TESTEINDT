using DomainProposta.Entities;
using DomainProposta.Enums;
using DomainProposta.Interfaces;
using DomainProposta.Validator;
using MediatR;

namespace ApiProposta.Commands.Handlers
{
    public class CriarPropostaCommandHandler : IRequestHandler<CriarPropostaCommand, Result>
    {
        private readonly IPropostaRepository _repository;

        public CriarPropostaCommandHandler(IPropostaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(CriarPropostaCommand request, CancellationToken cancellationToken)
        {
            var proposta = new Proposta
            {
                Id = Guid.NewGuid(),
                IdCliente = request.IdCliente,
                IdProduto = request.IdProduto,
                Valor = request.Valor,
                Status = StatusProposta.EmAnalise
            };

            var validator = new PropostaValidator();
            var result = validator.Validate(proposta);
            if (!result.IsValid)
            {
                return Result.Fail(result.Errors.FirstOrDefault()?.ErrorMessage, "ValidationError");
            }

            await _repository.CriarAsync(proposta);

            return Result.Ok(proposta.Id);
        }
    }
}
