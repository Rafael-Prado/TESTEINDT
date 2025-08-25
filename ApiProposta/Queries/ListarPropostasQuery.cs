using ApiProposta.DTOs;
using DomainProposta.Entities;
using MediatR;

namespace ApiProposta.Queries
{
    public class ListarPropostasQuery : IRequest<IEnumerable<PropostaDto>>
    {
    }
}
