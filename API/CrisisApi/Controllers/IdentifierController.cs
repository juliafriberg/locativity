using System.Threading.Tasks;
using CrisisApi.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CrisisApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentifierController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IdentifierController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetIdentifier()
        {
            var result = await _mediator.Send(new GetIdentifierCommand());

            return new JsonResult(result);
        }
    }
}