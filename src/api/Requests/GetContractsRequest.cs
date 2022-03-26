using api.Core;
using api.Models;
using api.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace api.Requests
{
    public class GetContractsRequest : IRequest<IReadOnlyCollection<ContractVM>> { }

    public class GetContractsRequestHandler : IRequestHandler<GetContractsRequest, IReadOnlyCollection<ContractVM>>
    {
        private readonly DBContext _context;

        public GetContractsRequestHandler(DBContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<ContractVM>> Handle(GetContractsRequest request, CancellationToken cancellationToken)
        {
            // load all enumerations
            var enumerations = await _context.Set<CTEnumeration>()
                .Select(x => new ContractVM()
                {
                    Id = x.Id,
                    Name = x.Name,
                    PropertyCount = x.Properties.Count,
                    Type = "Enumeration"
                })
                .ToListAsync(cancellationToken);

            // load all interfacers
            var interfaces = await _context.Set<CTInterface>()
                .Select(x => new ContractVM()
                {
                    Id = x.Id,
                    Name = x.Name,
                    PropertyCount = x.Properties.Count,
                    Type = "Interface"
                })
                .ToListAsync(cancellationToken);

            // load all abstractions
            var abstractions = await _context.Set<CTAbstraction>()
                .Select(x => new ContractVM()
                {
                    Id = x.Id,
                    Name = x.Name,
                    PropertyCount = x.Properties.Count,
                    Type = "Abstraction"
                })
                .ToListAsync(cancellationToken);

            return enumerations.Union(interfaces)
                .Union(abstractions)
                .ToList();
        }
    }
}
