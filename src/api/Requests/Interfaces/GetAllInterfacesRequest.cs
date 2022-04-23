using api.Core;
using api.Models;
using api.ViewModels.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace api.Requests.Interfaces
{
    public class GetAllInterfacesRequest : IRequest<IReadOnlyCollection<ListInterfaceVM>> { }

    public class GetContractsRequestHandler : IRequestHandler<GetAllInterfacesRequest, IReadOnlyCollection<ListInterfaceVM>>
    {
        private readonly IHistoryLoader _historyLoader;
        private readonly DBContext _context;

        public GetContractsRequestHandler(IHistoryLoader historyLoader, DBContext context)
        {
            _historyLoader = historyLoader;
            _context = context;
        }

        public async Task<IReadOnlyCollection<ListInterfaceVM>> Handle(GetAllInterfacesRequest request, CancellationToken cancellationToken)
        {
            // load from db
            var interfaces = await _context.Set<CTInterface>()
                .AsQueryable()
                .Select(x => new InterfaceDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Properties = x.Properties.Select(y => new MinimalDTO { Id = y.Id, Name = y.Name })
                })
                .ToListAsync(cancellationToken);

            // load history
            var history = await _historyLoader.QueryAll(interfaces)
                .Select(x => new { x.EntityId, x.Timestamp })
                .ToListAsync(cancellationToken);

            // create viewmodels
            return interfaces
                .Select(x => new ListInterfaceVM()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    LastModified = history.Where(y => y.EntityId == x.Id).Max(y => y.Timestamp),
                    HistoryCount = history.Where(y => y.EntityId == x.Id).Count(),
                    PropertiesCount = x.Properties.Count()
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
        public bool Deleted { get; set; }
        public IEnumerable<MinimalDTO> Properties { get; set; } = new List<MinimalDTO>();
    }

    internal class MinimalDTO : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Deleted { get; set; }
    }
}
