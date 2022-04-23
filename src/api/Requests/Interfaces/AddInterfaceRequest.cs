using api.Core;
using api.Models;
using api.ViewModels.Interface;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace api.Requests.Interfaces
{
    public class AddInterfaceRequest : IRequest<RequestResult<InterfaceVM>>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Guid> IncludingIds { get; set; } = new List<Guid>();
        public List<CTInterfaceProperty> Properties { get; set; } = new List<CTInterfaceProperty>();
    }

    public class AddInterfaceRequestHandler : IRequestHandler<AddInterfaceRequest, RequestResult<InterfaceVM>>
    {
        private readonly IHistoryLoader _historyLoader;
        private readonly IMapper _mapper;
        private readonly DBContext _context;

        public AddInterfaceRequestHandler(IHistoryLoader historyLoader, IMapper mapper, DBContext context)
        {
            _historyLoader = historyLoader;
            _mapper = mapper;
            _context = context;
        }

        public async Task<RequestResult<InterfaceVM>> Handle(AddInterfaceRequest request, CancellationToken cancellationToken)
        {
            // validate
            var alreadyExists = await _context.Set<CTInterface>()
                .Where(x => x.Name == request.Name)
                .AnyAsync(cancellationToken);

            if (alreadyExists)
                return RequestResult.Error<InterfaceVM>("Interface with same name already exists!");

            // create new interface
            var @interface = new CTInterface()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description
            };

            // attach includings
            await AddIncludings(@interface, request.IncludingIds, cancellationToken);

            // attach properties
            AddProperties(@interface, request.Properties);

            // save changes
            await _context.AddAsync(@interface, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            // attach history
            @interface.History = await _historyLoader.Load(@interface, true, cancellationToken);

            var vm = _mapper.Map<InterfaceVM>(@interface);
            return RequestResult.Success(vm);
        }

        private async Task AddIncludings(CTInterface @interface, List<Guid> includingIds, CancellationToken cancellationToken)
        {
            if (!includingIds.Any())
                return;

            var children = await _context.Set<CTInterface>()
                .Where(x => includingIds.Contains(x.Id))
                .ToListAsync(cancellationToken);

            foreach (var child in children)
                @interface.Includings.Add(new CTInterfaceAssignment()
                {
                    SourceId = @interface.Id,
                    DestinationId = child.Id
                });
        }

        private static void AddProperties(CTInterface @interface, List<CTInterfaceProperty> properties)
        {
            foreach (var property in properties)
            {
                property.Id = Guid.NewGuid();
                property.InterfaceId = @interface.Id;
                @interface.Properties.Add(property);
            }
        }
    }
}
