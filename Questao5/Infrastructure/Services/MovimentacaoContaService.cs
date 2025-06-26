using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Application.Commands.Requests;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Repositories;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Services
{
    public class MovimentacaoContaService : IMovimentacaoContaService
    {
        private readonly IMovimentacaoContaRepository movimentacaoContaRepository;

        public MovimentacaoContaService(IMovimentacaoContaRepository movimentacaoContaRepository)
        {
            this.movimentacaoContaRepository = movimentacaoContaRepository;
        }

        public async Task MovimentarContar(MovimentarContarReq request)
        {
            var contaCorrente = await movimentacaoContaRepository.GetContaCorrenteAsync(request.ContaId);
            if (contaCorrente == null)
            {
                Console.WriteLine("INVALID_ACCOUNT");
                return;
            }
            if(!contaCorrente.Ativo)
            {
                Console.WriteLine("INACTIVE_ACCOUNT");
                return;
            }
            if (request.Valor < 0)
            {
                Console.WriteLine("INVALID_VALUE");
                return;
            }
            string credito = TipoMovimento.CREDITO.ToString();
            string debito = TipoMovimento.DEBITO.ToString();
            string reqTipo = request.Tipo.ToString();
            if (reqTipo != credito && reqTipo != debito)
            {
                Console.WriteLine("INVALID_TYPE");
                return;
            }

            var novoMovimento = new Movimento
            {
                IdMovimento = Guid.NewGuid().ToString(),
                IdContaCorrente = request.ContaId.ToString(),
                Valor = request.Valor,
                TipoMovimento = request.Tipo.ToString().ToUpper(),
                DataMovimento = DateTime.Now.ToString("dd/MM/yyyy")
            };

            await movimentacaoContaRepository.InserirMovimentoAsync(novoMovimento);
        }
    }
}
