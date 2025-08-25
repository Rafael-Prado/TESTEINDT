using DomainContratacao.Entities;
using DomainContratacao.Interfaces;
using Dapper;
using System.Data;

namespace InfraContratacao.Repositories
{
    public class ContratacaoRepository : IContratacaoRepository
    {
        private readonly IDbConnection _connection;

        public ContratacaoRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Proposta>> GetPropostasAprovadasAsync()
        {
            
            var sql = @"SELECT Id, IdCliente, IdProduto, Valor, Status
                        FROM Proposta
                        WHERE Status = @Status";

            return await _connection.QueryAsync<Proposta>(sql, new { Status = 2 });
           
        }

        public async Task InsertContratacao(Contratacoes contratacoes)
        {
            var sql = @"
                INSERT INTO [Contratacoes] (Id, PropostaId, DataContratacao) 
                VALUES (@Id, @PropostaId, @DataContratacao);";

            await _connection.ExecuteScalarAsync(sql, contratacoes);
        }

        public async Task UpdateProposta(Proposta proposta)
        {
            var sql = @"
                UPDATE Proposta
                SET 
                    IdCliente = @IdCliente,
                    IdProduto = @IdProduto,
                    Valor = @Valor,
                    Status = @Status
                WHERE Id = @Id;
            ";

            await _connection.ExecuteAsync(sql, proposta);
        }
    }
}
