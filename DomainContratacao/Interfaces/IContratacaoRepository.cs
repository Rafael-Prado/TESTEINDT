using DomainContratacao.Entities;

namespace DomainContratacao.Interfaces
{
    public interface IContratacaoRepository
    {
        Task<IEnumerable<Proposta>> GetPropostasAprovadasAsync();
        Task InsertContratacao(Contratacoes proposta);
        Task UpdateProposta(Proposta proposta);
    }
}
