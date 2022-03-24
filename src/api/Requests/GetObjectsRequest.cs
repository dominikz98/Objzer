using api.Core;
using api.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace api.Requests
{
    public class GetObjectsRequest : IRequest<IReadOnlyCollection<CTObject>> { }

    public class GetObjectsRequestHandler : IRequestHandler<GetObjectsRequest, IReadOnlyCollection<CTObject>>
    {
        private readonly ObjzerContext _context;

        public GetObjectsRequestHandler(ObjzerContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<CTObject>> Handle(GetObjectsRequest request, CancellationToken cancellationToken)
            => await _context.Objects.Include(x => x.Interfaces)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
    }
}
