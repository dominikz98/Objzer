using AutoMapper;
using Core.Models;
using Core.ViewModels.Interface;
using Infrastructure.Core;
using Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Requests.Interfaces
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
        private readonly IMapper _mapper;
        private readonly ObjzerContext _context;

        public AddInterfaceRequestHandler(IMapper mapper, ObjzerContext context)
        {
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
                return RequestResult<InterfaceVM>.Error("Interface with same name already exists!");

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
            await AddProperties(@interface, request.Properties, cancellationToken);

            // save changes
            await _context.AddAsync(@interface, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            // attach history
            @interface.History = await _context.Set<CTHistory>().GetByEntity(@interface.Id);

            var vm = _mapper.Map<InterfaceVM>(@interface);
            return RequestResult<InterfaceVM>.Success(vm);
        }

        private async Task AddIncludings(CTInterface @interface, List<Guid> includingIds, CancellationToken cancellationToken)
        {
            foreach (var includingId in includingIds)
            {
                var assignment = new CTInterfaceAssignment()
                {
                    ReferenceId = @interface.Id,
                    DestinationId = includingId
                };
                await _context.AddAsync(assignment, cancellationToken);
                @interface.Includings.Add(assignment);
            }
        }

        private async Task AddProperties(CTInterface @interface, List<CTInterfaceProperty> properties, CancellationToken cancellationToken)
        {
            foreach (var property in properties)
            {
                property.ReferenceId = @interface.Id;

                await _context.AddAsync(property, cancellationToken);
                @interface.Properties.Add(property);
            }
        }
    }
}
