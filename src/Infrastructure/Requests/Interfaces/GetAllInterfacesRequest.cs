using Core.Models;
using Core.Models.Contracts;
using Core.ViewModels.Interface;
using Infrastructure.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Requests.Interfaces
{
    public class GetAllInterfacesRequest : IRequest<IReadOnlyCollection<ListInterfaceVM>> { }

    public class GetContractsRequestHandler : IRequestHandler<GetAllInterfacesRequest, IReadOnlyCollection<ListInterfaceVM>>
    {
        private readonly ObjzerContext _context;

        public GetContractsRequestHandler(ObjzerContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<ListInterfaceVM>> Handle(GetAllInterfacesRequest request, CancellationToken cancellationToken)
        {
            // load from db
            var interfaces = await _context.Set<CTInterface>()
                .Select(x => new InterfaceDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Properties = x.Properties.Count,
                    Locked = x.Locked,
                    Archived = x.Archived
                })
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);

            // load history
            var interfaceIds = interfaces.Select(x => x.Id);
            var history = await _context.Set<CTHistory>()
                .Where(x => interfaceIds.Contains(x.EntityId))
                .Select(x => new { x.EntityId, x.Timestamp })
                .ToListAsync(cancellationToken);

            // create viewmodels
            return interfaces
                .Select(x => new ListInterfaceVM()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Archived = x.Archived,
                    Locked = x.Locked,
                    LastModified = history.Where(y => y.EntityId == x.Id).Max(y => y.Timestamp),
                    HistoryCount = history.Where(y => y.EntityId == x.Id).Count(),
                    PropertiesCount = x.Properties
                })
                .OrderBy(x => x.Name)
                .ToList();
        }
    }

    internal class InterfaceDTO : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Properties { get; set; }
        public bool Locked { get; set; }
        public DateOnly? Archived { get; set; }
        public bool Deleted { get; set; }
    }
}
