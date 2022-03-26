using api.Core;
using api.Models;
using api.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace api.Requests
{
    public class GetObjectsRequest : IRequest<IReadOnlyCollection<ObjectVM>> { }

    public class GetObjectsRequestHandler : IRequestHandler<GetObjectsRequest, IReadOnlyCollection<ObjectVM>>
    {
        private readonly DBContext _context;

        public GetObjectsRequestHandler(DBContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<ObjectVM>> Handle(GetObjectsRequest request, CancellationToken cancellationToken)
            => await _context.Set<CTObject>()
                .Select(x => new ObjectVM()
                {
                    Id = x.Id,
                    Name = x.Name,
                    PropertyCount = x.Properties.Count,
                    AbstractionCount = x.Abstractions.Count,
                    InterfaceCount = x.Interfaces.Count
                })
                .ToListAsync(cancellationToken);
    }
}
