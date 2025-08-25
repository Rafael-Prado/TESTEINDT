using DomainProposta.Entities;

namespace DomainProposta.Interfaces
{
    public interface IMongoService
    {
        Task SaveHistoricoPropostaAsync(PropostaMongo proposta);
    }
}
