using AutoMapper;
using Core.Models;
using Core.ViewModels.Interface;
using Infrastructure.Requests;
using Infrastructure.Requests.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
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

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] InterfaceVM vm, CancellationToken cancellationToken)
        {
            var request = _mapper.Map<EditInterfaceRequest>(vm);
            var result = await _mediator.Send(request, cancellationToken);

            return result.Status switch
            {
                RequestResultStatus.SUCCESS => Ok(result.Value),
                RequestResultStatus.NOT_FOUND => NotFound(result.Message),
                _ => throw new NotImplementedException()
            };
        }

        [HttpPut("lock/{id:guid}")]
        public async Task<IActionResult> Lock(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new UnLockEntityRequest<CTInterface>(id, true), cancellationToken);
            return result.Status switch
            {
                RequestResultStatus.SUCCESS => Ok(),
                RequestResultStatus.NOT_FOUND => NotFound(result.Message),
                _ => throw new NotImplementedException()
            };
        }

        [HttpPut("unlock/{id:guid}")]
        public async Task<IActionResult> Unlock(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new UnLockEntityRequest<CTInterface>(id, false), cancellationToken);
            return result.Status switch
            {
                RequestResultStatus.SUCCESS => Ok(),
                RequestResultStatus.NOT_FOUND => NotFound(result.Message),
                _ => throw new NotImplementedException()
            };
        }

        [HttpPut("archive/{id:guid}")]
        public async Task<IActionResult> Archive(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new RestoreOrArchiveEntityRequest<CTInterface>(id, DateOnly.FromDateTime(DateTime.Now)), cancellationToken);
            return result.Status switch
            {
                RequestResultStatus.SUCCESS => Ok(),
                RequestResultStatus.NOT_FOUND => NotFound(result.Message),
                _ => throw new NotImplementedException()
            };
        }

        [HttpPut("restore/{id:guid}")]
        public async Task<IActionResult> Restore(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new RestoreOrArchiveEntityRequest<CTInterface>(id, null), cancellationToken);
            return result.Status switch
            {
                RequestResultStatus.SUCCESS => Ok(),
                RequestResultStatus.NOT_FOUND => NotFound(result.Message),
                _ => throw new NotImplementedException()
            };
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteInterfaceRequest(id), cancellationToken);
            return result.Status switch
            {
                RequestResultStatus.SUCCESS => Ok(result.Value),
                RequestResultStatus.NOT_FOUND => NotFound(result.Message),
                _ => throw new NotImplementedException()
            };
        }
    }
}
