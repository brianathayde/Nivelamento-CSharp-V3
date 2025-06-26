using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;

namespace Questao5.Infrastructure.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimentacaoContaController : ControllerBase
    {
        private readonly IMovimentacaoContaService movimentacaoContaService;

        public MovimentacaoContaController(IMovimentacaoContaService movimentacaoContaService)
        {
            this.movimentacaoContaService = movimentacaoContaService;
        }

        [HttpPost]
        public void Post([FromBody] MovimentarContarReq moveConta)
        {
            movimentacaoContaService.MovimentarContar(moveConta);
        }

    }
}
