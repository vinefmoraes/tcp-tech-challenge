namespace Tcp.TechChallenge.Api.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Tcp.TechChallenge.Domain.Models;
    using Tcp.TechChallenge.Domain.Services;

    [ApiController]
    [Route("v1/[controller]/")]
    public class ConteinerController : ControllerBase
    {
        [HttpGet]
        [Route("{conteinerIdentifier}")]
        [ProducesResponseType(typeof(Error[]), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ConteinerRequest), StatusCodes.Status200OK)]
        public IActionResult Get(
            [FromServices] IConteinerHandleService conteinerHandleService,
            [FromRoute] string conteinerIdentifier)
        {
            var (success, result, errors) = conteinerHandleService.FindByIdentifier(conteinerIdentifier);
            if (!success)
                return BadRequest(errors);

            return Ok(result);
        }

        [HttpGet]
        [Route("all")]
        [ProducesResponseType(typeof(Error[]), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IList<ConteinerRequest>), StatusCodes.Status200OK)]
        public IActionResult GetAllConteiners(
           [FromServices] IConteinerHandleService conteinerHandleService)
        {
            var result = conteinerHandleService.FindAll();
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Error[]), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(
           [FromServices] IConteinerHandleService conteinerHandleService,
           [FromBody] ConteinerRequest conteiner)
        {
            var (success, result, errors) = await conteinerHandleService.Insert(conteiner);
            if (!success)
                return BadRequest(errors);

            return Created("", conteiner);
        }

        [HttpPut("{identifier}")]
        [ProducesResponseType(typeof(Error[]), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Edit(
           [FromServices] IConteinerHandleService conteinerHandleService,
           [FromRoute] string identifier,
           [FromBody] ConteinerRequest conteiner)
        {
            var (success, _, errors) = await conteinerHandleService.Edit(identifier, conteiner);
            if (!success)
                return BadRequest(errors);

            return NoContent();
        }

        [HttpDelete("{identifier}")]
        [ProducesResponseType(typeof(Error[]), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status202Accepted)]
        public async Task<IActionResult> Delete(
           [FromServices] IConteinerHandleService conteinerHandleService,
           [FromRoute] string identifier)
        {
            var (success, result, errors) = await conteinerHandleService.Delete(identifier);
            if (!success)
                return BadRequest(errors);

            return Accepted(result);
        }
    }
}
