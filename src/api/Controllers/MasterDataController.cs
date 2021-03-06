using Core.Models.Enumerations;
using Infrastructure.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MasterDataController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MasterDataController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("property/types")]
        public async Task<IActionResult> GetPropertyTypes(CancellationToken cancellationToken)
           => Ok(await _mediator.Send(new GetDataFromEnumRequest(typeof(PropertyType)), cancellationToken));
    }
}
