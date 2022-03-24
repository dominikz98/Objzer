using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("contracts/[controller]")]
    public class InterfacesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InterfacesController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
            => Ok(await _mediator.Send(null, cancellationToken));

        [HttpPost()]
        public async Task<IActionResult> Add(CancellationToken cancellationToken)
            => Ok(await _mediator.Send(null, cancellationToken));

        [HttpPut()]
        public async Task<IActionResult> Update(CancellationToken cancellationToken)
            => Ok(await _mediator.Send(null, cancellationToken));

        [HttpPut()]
        public async Task<IActionResult> Delete(CancellationToken cancellationToken)
            => Ok(await _mediator.Send(null, cancellationToken));
    }
}
