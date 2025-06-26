using MediatR;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Requests
{
    public class MovimentarContarReq : IRequest<bool>
    {
        public int ReqId { get; set; }
        public Guid ContaId { get; set; }
        public TipoMovimento Tipo { get; set; }
        public decimal Valor { get; set; }
    }
}
