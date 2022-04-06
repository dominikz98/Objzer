using api.Core;
using api.Requests;
using api.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InterfacesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public InterfacesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
            => Ok(await _mediator.Send(new GetAllInterfacesRequest(), cancellationToken));


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
            => Ok(await _mediator.Send(new GetInterfaceRequest(id), cancellationToken));

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddInterfaceVM vm, CancellationToken cancellationToken)
        {
            var request = _mapper.Map<AddInterfaceRequest>(vm);
            var result = await _mediator.Send(request, cancellationToken);

            return result.Status switch
            {
                RequestResultStatus.SUCCESS => Ok(result.Value),
                RequestResultStatus.VALIDATION_ERROR => BadRequest(result.Message),
                _ => throw new NotImplementedException()
            };
        }

        //[HttpPut]
        //public async Task<IActionResult> Update(CancellationToken cancellationToken)
        //    => Ok(await _mediator.Send(null, cancellationToken));

        //[HttpDelete]
        //public async Task<IActionResult> Delete(CancellationToken cancellationToken)
        //    => Ok(await _mediator.Send(null, cancellationToken));
    }
}
