using ApiProposta.DTOs;
using AutoMapper;
using DomainProposta.Interfaces;
using MediatR;

namespace ApiProposta.Queries.Handlers
{
    public class ListarPropostasHandler : IRequestHandler<ListarPropostasQuery, IEnumerable<PropostaDto>>
    {
        private readonly IPropostaRepository _repository;
        private readonly IMapper _mapper;

        public ListarPropostasHandler(IPropostaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PropostaDto>> Handle(ListarPropostasQuery request, CancellationToken cancellationToken)
        {
            var propostas=  await _repository.ListarAsync();
            return _mapper.Map<IEnumerable<PropostaDto>>(propostas);
        }
    }
}
