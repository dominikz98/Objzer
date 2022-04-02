using api.Core;
using api.Models;
using api.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace api.Requests
{
    public class GetContractsRequest : IRequest<IReadOnlyCollection<ListVM>> { }

    public class GetContractsRequestHandler : IRequestHandler<GetContractsRequest, IReadOnlyCollection<ListVM>>
    {
        private readonly DBContext _context;

        public GetContractsRequestHandler(DBContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<ListVM>> Handle(GetContractsRequest request, CancellationToken cancellationToken)
        {
            // load all enumerations
            var enumerations = await _context.Set<CTEnumeration>()
                .Select(x => new ListVM
                {
                    Id = x.Id,
                    Type = ListType.Enumeration,
                    References = new List<ListReferenceVM>()
                    {
                        new() { Type = ListReferenceType.History, Count = x.History.Count },
                        new() { Type = ListReferenceType.Property, Count = x.Properties.Count }
                    }
                })
                .ToListAsync(cancellationToken);

            // load all interfacers
            var interfaces = await _context.Set<CTInterface>()
                .Select(x => new ListVM
                {
                    Id = x.Id,
                    Type = ListType.Interface,
                    References = new List<ListReferenceVM>()
                    {
                        new () { Type = ListReferenceType.History, Count = x.History.Count },
                        new () { Type = ListReferenceType.Property, Count = x.Properties.Count },
                        new () { Type = ListReferenceType.Interfaces, Count = x.Implementations.References.Count },
                        new () { Type = ListReferenceType.Objects, Count = x.Objects.Count }
                    }
                })
                .ToListAsync(cancellationToken);

            // load all abstractions
            var abstractions = await _context.Set<CTAbstraction>()
                .Select(x => new ListVM
                {
                    Id = x.Id,
                    Type = ListType.Interface,
                    References = new List<ListReferenceVM>()
                    {
                        new () { Type = ListReferenceType.History, Count = x.History.Count },
                        new () { Type = ListReferenceType.Property, Count = x.Properties.Count },
                        new () { Type = ListReferenceType.Interfaces, Count = x.Inheritances.Count },
                        new () { Type = ListReferenceType.Objects, Count = x.Objects.Count }
                    }
                })
                .ToListAsync(cancellationToken);

            return enumerations.Union(interfaces)
                .Union(abstractions)
                .ToList();
        }
    }
}
