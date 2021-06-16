using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tcp.TechChallenge.Domain.Models;
using Tcp.TechChallenge.Domain.Services;

namespace Tcp.TechChallenge.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]/")]
    public class ConteinerController : ControllerBase
    {

        [HttpGet]
        [Route("{conteinerIdentifier}")]
        public IActionResult Get(
            [FromServices] IConteinerHandleService conteinerHandleService,
            [FromRoute] string conteinerIdentifier)
        {
            return Ok(conteinerHandleService.FindByIdentifier(conteinerIdentifier));
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAllConteiners(
           [FromServices] IConteinerHandleService conteinerHandleService)
        {
            return Ok(conteinerHandleService.List());
        }

        [HttpPost]
        public IActionResult Create(
           [FromServices] IConteinerHandleService conteinerHandleService,
           [FromBody] Conteiner conteiner)
        {
            return Ok(conteinerHandleService.Insert(conteiner));
        }

        [HttpPut]
        public IActionResult Edit(
           [FromServices] IConteinerHandleService conteinerHandleService,
           [FromRoute] string identifier,
           [FromBody] Conteiner conteiner)
        {
            return Ok(conteinerHandleService.Edit(identifier, conteiner));
        }

        [HttpDelete]
        public IActionResult Delete(
           [FromServices] IConteinerHandleService conteinerHandleService,
           [FromRoute] string identifier,
           [FromBody] Conteiner conteiner)
        {
            return Ok(conteinerHandleService.Edit(identifier, conteiner));
        }
    }
}
