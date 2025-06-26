using Questao5.Domain.Entities;
using System.Threading.Tasks;

namespace Questao5.Infrastructure.Repositories
{
    public interface IMovimentacaoContaRepository
    {
        Task<ContaCorrente?> GetContaCorrenteAsync(Guid idContaCorrente);
        Task<string?> InserirMovimentoAsync(Movimento movimento);
    }
}
