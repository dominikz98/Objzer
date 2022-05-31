using AutoMapper;
using Core.Models;
using Core.ViewModels.Objects;
using Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Requests.Objects;

public class GetObjectRequest : IRequest<RequestResult<ObjectVM>>
{
    public Guid Id { get; set; }

    public GetObjectRequest(Guid id)
    {
        Id = id;
    }
}

public class GetObjectRequestHandler : IRequestHandler<GetObjectRequest, RequestResult<ObjectVM>>
{
    private readonly IMapper _mapper;
    private readonly ObjzerContext _context;

    public GetObjectRequestHandler(IMapper mapper, ObjzerContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<RequestResult<ObjectVM>> Handle(GetObjectRequest request, CancellationToken cancellationToken)
    {
        // load object with references
        var @object = await _context.Set<CTObject>()
            .Include(x => x.Properties)
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (@object is null)
            return RequestResult<ObjectVM>.Null();

        // attach history
        @object.History = await _context.Set<CTHistory>().GetByEntity(@object.Id);

        var vm = _mapper.Map<ObjectVM>(@object);
        return RequestResult<ObjectVM>.Success(vm);
    }
}
