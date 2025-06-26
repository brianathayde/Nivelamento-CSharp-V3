using Questao5.Application.Commands.Requests;

namespace Questao5.Infrastructure.Services
{
    public interface IMovimentacaoContaService
    {
        Task MovimentarContar(MovimentarContarReq request);
    }
}
