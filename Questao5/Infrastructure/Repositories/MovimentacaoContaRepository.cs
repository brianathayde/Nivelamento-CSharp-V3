using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Repositories
{
    public class MovimentacaoContaRepository : IMovimentacaoContaRepository
    {
        private readonly DatabaseConfig databaseConfig;

        public MovimentacaoContaRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }


        public async Task<ContaCorrente?> GetContaCorrenteAsync(Guid idContaCorrente)
        {
            var sql = "SELECT * FROM contacorrente WHERE idcontacorrente = @IdContaCorrente";
            using var connection = new SqliteConnection(databaseConfig.Name);
            var conta = await connection.QueryFirstOrDefaultAsync<ContaCorrente?>(sql, new { IdContaCorrente = idContaCorrente });
            return conta;
        }

        public async Task<string?> InserirMovimentoAsync(Movimento movimento)
        {
            var sql = @"
            INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor)
            VALUES (@IdMovimento, @IdContaCorrente, @DataMovimento, @TipoMovimento, @Valor);";

            using var connection = new SqliteConnection(databaseConfig.Name);
            await connection.OpenAsync();
            await connection.ExecuteAsync(sql, movimento);
            await connection.CloseAsync();

            return movimento.IdMovimento;
        }
    }
}
