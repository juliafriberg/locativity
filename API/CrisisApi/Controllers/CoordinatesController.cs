using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrisisApi.Commands;
using CrisisApi.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrisisApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoordinatesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CoordinatesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> PostCoordinates(Coordinates coordinates)
        {
            var result = await _mediator.Send(new PostCoordinatesCommand(coordinates));

            return new JsonResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCoordinatesInfo([FromQuery(Name = "start")]
        DateTime start, [FromQuery(Name = "end")]DateTime end)
        {
            var result = new List<Coordinatesinfo>();

            if(start == null && end == null)
            {
                result = await _mediator.Send(new GetAllCoordinatesByDateCommand(start, end));
            }
            else
            {
                result = await _mediator.Send(new GetAllCoordinatesCommand());
            }

            return new JsonResult(result);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetCoordinatesInfo(Guid id, [FromQuery(Name = "start")]
        DateTime start, [FromQuery(Name = "end")]DateTime end)
        {
            var result = new List<Coordinatesinfo>();

            if(start == null && end == null)
            {
                result = await _mediator.Send(new GetCoordinatesByIdCommand(id));
            }
            else
            {
                result = await _mediator.Send(new GetCoordinatesByDateCommand(id, start, end));
            }

            return new JsonResult(result);
        }
    }
}