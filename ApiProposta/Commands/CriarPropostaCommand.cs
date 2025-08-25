using MediatR;
namespace ApiProposta.Commands
{
    public record CriarPropostaCommand(Guid IdCliente, Guid IdProduto, decimal Valor) : IRequest<Result>;
}
