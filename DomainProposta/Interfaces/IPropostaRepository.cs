using DomainProposta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainProposta.Interfaces
{
    public interface IPropostaRepository
    {
        Task<Proposta> CriarAsync(Proposta proposta);
        Task<IEnumerable<Proposta>> ListarAsync();
        Task<Proposta?> ObterPorIdAsync(Guid id);
        Task AtualizarAsync(Proposta proposta);
    }
}
