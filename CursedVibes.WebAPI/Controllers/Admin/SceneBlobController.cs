using Asp.Versioning;
using CursedVibes.Application.Characters.Dtos;
using CursedVibes.Application.Scenes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CursedVibes.WebAPI.Controllers.Admin
{
    [ApiController]
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    public class SceneBlobController: ControllerBase
    {
        private readonly IMediator _mediator;

        public SceneBlobController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("scene/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SceneDto>> GetSceneById(string id)
        {
            var result = await _mediator.Send(new GetSceneByIdQuery(id));
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
