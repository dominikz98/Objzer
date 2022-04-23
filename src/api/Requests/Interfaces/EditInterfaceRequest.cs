using api.Core;
using api.Models;
using api.ViewModels.Interface;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace api.Requests.Interfaces
{
    public class EditInterfaceRequest : IRequest<RequestResult<InterfaceVM>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Guid> IncludingIds { get; set; } = new List<Guid>();
        public List<CTInterfaceProperty> Properties { get; set; } = new List<CTInterfaceProperty>();
    }

    public class EditInterfaceRequestHandler : IRequestHandler<EditInterfaceRequest, RequestResult<InterfaceVM>>
    {
        private readonly IHistoryLoader _historyLoader;
        private readonly IMapper _mapper;
        private readonly DBContext _context;

        public EditInterfaceRequestHandler(IHistoryLoader historyLoader, IMapper mapper, DBContext context)
        {
            _historyLoader = historyLoader;
            _mapper = mapper;
            _context = context;
        }

        public async Task<RequestResult<InterfaceVM>> Handle(EditInterfaceRequest request, CancellationToken cancellationToken)
        {
            // load interface
            var @interface = await _context.Set<CTInterface>()
                .Include(x => x.Includings)
                .Include(x => x.Properties)
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (@interface is null)
                return RequestResult.Null<InterfaceVM>();

            // mappings
            @interface.Name = request.Name;
            @interface.Description = request.Description;
            UpdateIncludings(@interface, request.IncludingIds);
            UpdateProperties(@interface, request.Properties);

            // save changes
            _context.Update(@interface);
            await _context.SaveChangesAsync(cancellationToken);

            // attach history
            @interface.History = await _historyLoader.Load(@interface, true, cancellationToken);

            var vm = _mapper.Map<InterfaceVM>(@interface);
            return RequestResult.Success(vm);
        }

        private static void UpdateIncludings(CTInterface @interface, List<Guid> includingIds)
        {
            // remove including assignments
            foreach (var including in @interface.Includings)
            {
                var contains = includingIds.Contains(including.DestinationId);
                if (!contains)
                    including.Deleted = true;
            }

            // add including assignments
            foreach (var includingId in includingIds)
            {
                var contains = @interface.Includings.Any(x => x.DestinationId == includingId);
                if (!contains)
                    @interface.Includings.Add(new CTInterfaceAssignment()
                    {
                        SourceId = @interface.Id,
                        DestinationId = includingId
                    });
            }
        }

        private static void UpdateProperties(CTInterface @interface, List<CTInterfaceProperty> properties)
        {
            // remove properties
            foreach (var property in @interface.Properties)
            {
                var contains = properties.Any(x => x.Id == property.Id);
                if (!contains)
                    property.Deleted = true;
            }

            // add properties
            foreach (var property in properties)
            {
                var contains = @interface.Properties.Any(x => x.Id == property.Id);
                if (contains)
                    continue;

                property.Id = Guid.NewGuid();
                property.InterfaceId = @interface.Id;
                @interface.Properties.Add(property);
            }
        }
    }
}
