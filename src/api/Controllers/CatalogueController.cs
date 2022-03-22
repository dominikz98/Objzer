using api.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("objects/[controller]")]
    public class CatalogueController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CatalogueController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
            => Ok(await _mediator.Send(new GetObjectsRequest(), cancellationToken));
    }
}