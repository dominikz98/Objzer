using api.Core;
using api.Models;
using api.ViewModels.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace api.Requests
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
                .Select(x => x.Timestamp)
                .ToListAsync(cancellationToken);

            // create viewmodels
            var result = new List<ListInterfaceVM>();
            foreach (var @interface in interfaces)
            {

                result.Add(new ListInterfaceVM()
                {
                    Id = @interface.Id,
                    Name = @interface.Name,
                    Description = @interface.Description,
                    LastModified = history.Max(),
                    HistoryCount = history.Count,
                    PropertiesCount = @interface.Properties.Count()
                });
            }
            return result;
        }

        class InterfaceDTO : IEntity
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public bool Deleted { get; set; }
            public IEnumerable<MinimalDTO> Properties { get; set; } = new List<MinimalDTO>();
        }

        class MinimalDTO : IEntity
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public bool Deleted { get; set; }
        }
    }
}
