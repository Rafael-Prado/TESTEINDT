using ApiContratacao.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiContratacao.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContratacaoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContratacaoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Contratar([FromBody] CreateContratacaoCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Sucesso)
                return BadRequest(new { result.Erro, result.TipoErro });

            return Ok(result);
        }
    }
}
