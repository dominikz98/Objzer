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
        {
            // load from db
            var interfaces = await _context.Set<CTInterface>()
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.Description,
                    History = x.History.Select(x => x.Timestamp),
                    PropertiesHistories = x.Properties.Select(x => x.History.Select(x => x.Timestamp)),
                    Objects = x.Objects.Count,
                    Properties = x.Properties.Count
                })
                .ToListAsync(cancellationToken);

            var result = new List<ListInterfaceVM>();
            foreach (var @interface in interfaces)
            {
                // calculate history count and last modification timestamp
                var counter = @interface.History.Count();
                var modifications = @interface.History.ToList();
                foreach (var propHistory in @interface.PropertiesHistories)
                {
                    modifications.AddRange(propHistory);
                    counter += propHistory.Count();
                }

                result.Add(new ListInterfaceVM()
                {
                    Id = @interface.Id,
                    Name = @interface.Name,
                    Description = @interface.Description[..25],
                    HistoryCount = counter,
                    LastModified = modifications.Max(),
                    ObjectsCount = @interface.Objects,
                    PropertiesCount = @interface.Properties
                });
            }
            return result;
        }
    }
}
