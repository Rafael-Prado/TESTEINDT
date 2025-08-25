using ApiProposta.Commands;
using ApiProposta.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiProposta.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
        public class PropostaController : ControllerBase
        {
            private readonly IMediator _mediator;

            public PropostaController(IMediator mediator)
            {
                _mediator = mediator;
            }

            [HttpPost]
            public async Task<IActionResult> Criar([FromBody] CriarPropostaCommand command)
            {
                var result = await _mediator.Send(command);
            if (!result.Sucesso)
                return BadRequest(new { message = result.Erro, type = result.TipoErro });

            return Ok(result);
        }

            [HttpGet]
            public async Task<IActionResult> Listar()
            {
                var propostas = await _mediator.Send(new ListarPropostasQuery());
                return Ok(propostas);
            }

            [HttpPut("{id:guid}/status")]
            public async Task<IActionResult> AlterarStatus(Guid id, [FromBody] AlterarStatusPropostaCommand command)
            {
                if (id != command.PropostaId)
                    return BadRequest("ID da URL difere do corpo da requisição.");

                var result = await _mediator.Send(command);

                if (!result.Sucesso)
                    return BadRequest(new { message = result.Erro, type = result.TipoErro });

                return Ok(result);
            }
        }
}
