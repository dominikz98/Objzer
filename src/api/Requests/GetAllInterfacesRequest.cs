using api.Core;
using api.Models;
using api.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace api.Requests
{
    public class GetAllInterfacesRequest : IRequest<IReadOnlyCollection<ListInterfaceVM>> { }

    public class GetContractsRequestHandler : IRequestHandler<GetAllInterfacesRequest, IReadOnlyCollection<ListInterfaceVM>>
    {
        private readonly DBContext _context;

        public GetContractsRequestHandler(DBContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<ListInterfaceVM>> Handle(GetAllInterfacesRequest request, CancellationToken cancellationToken)
            => await _context.Set<CTInterface>()
                .Select(x => new ListInterfaceVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    HistoryCount = x.History.Count + x.Properties.Select(x => x.History.Count).Sum(),
                    ObjectsCount = x.Objects.Count,
                    PropertiesCount = x.Properties.Count
                })
                .ToListAsync(cancellationToken);
    }
}
