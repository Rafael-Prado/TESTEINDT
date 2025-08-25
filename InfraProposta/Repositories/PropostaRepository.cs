using Dapper;
using DomainProposta.Entities;
using DomainProposta.Interfaces;
using System.Data;
using System.Diagnostics.Contracts;

namespace InfraProposta.Repositories
{
    public class PropostaRepository : IPropostaRepository
    {
        private readonly IDbConnection _connection;

        public PropostaRepository(IDbConnection connection)
        {
            _connection = connection;
        }


        public async Task AtualizarAsync(Proposta proposta)
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

        public async Task<Proposta> CriarAsync(Proposta proposta)
        {
            var sql = @"
                INSERT INTO [Proposta] (Id, IdCliente, IdProduto, Valor,Status) 
                VALUES (@Id, @IdCliente, @IdProduto,@Valor, @Status);";

            await _connection.ExecuteScalarAsync(sql, proposta);

            return proposta;
        }

        public async Task<IEnumerable<Proposta>> ListarAsync()
        {
            var sql = @"
                    SELECT 
                        Id,
                        IdCliente,
                        IdProduto,
                        Valor,
                        Status
                    FROM [Proposta]";

            var orders = await _connection.QueryAsync<Proposta>(
                sql
            );
            return orders;
        }

        public async Task<Proposta?> ObterPorIdAsync(Guid id)
        {
            var sql = @"
                    SELECT 
                        Id,
                        IdCliente,
                        IdProduto,
                        Valor,
                        Status
                    FROM [Proposta]
                    WHERE Id = @Id";

            var order = await _connection.QuerySingleOrDefaultAsync<Proposta>(
                sql,
                new {Id=id} // parâmetro nomeado
            );

            return order;
        }
    }
}
