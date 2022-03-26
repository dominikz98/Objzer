using api.Core;
using api.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace api.Requests
{
    public class GetContractsRequest : IRequest<IReadOnlyCollection<CTContract>> { }

    public class GetContractsRequestHandler : IRequestHandler<GetContractsRequest, IReadOnlyCollection<CTContract>>
    {
        private readonly DBContext _context;

        public GetContractsRequestHandler(DBContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<CTContract>> Handle(GetContractsRequest request, CancellationToken cancellationToken)
            => await _context.Set<CTContract>()
                .AsNoTracking()
                .ToListAsync(cancellationToken);
    }
}
