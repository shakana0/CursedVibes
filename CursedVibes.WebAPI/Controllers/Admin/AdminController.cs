using Asp.Versioning;
using CursedVibes.Application.Characters.Commands.CreateCharacter;
using CursedVibes.Application.Characters.Dtos;
using CursedVibes.Application.Characters.Queries.GetCharacter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CursedVibes.WebAPI.Controllers.Admin
{
    [ApiController]
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Character/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CharacterDto>> GetCharacterById(int id)
        {
            var result = await _mediator.Send(new GetCharacterByIdQuery(id));

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("Character")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> CreateCharacter(CreateCharacterCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
