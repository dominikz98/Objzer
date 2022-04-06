using api.Core;
using api.Models;
using api.ViewModels;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace api.Requests
{
    public class AddInterfaceRequest : IRequest<RequestResult<InterfaceVM>>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Guid> ChildrenIds { get; set; } = new List<Guid>();
        public List<CTInterfaceProperty> Properties { get; set; } = new List<CTInterfaceProperty>();
    }

    public class AddInterfaceRequestHandler : IRequestHandler<AddInterfaceRequest, RequestResult<InterfaceVM>>
    {
        private readonly IMapper _mapper;
        private readonly DBContext _context;

        public AddInterfaceRequestHandler(IMapper mapper, DBContext context)
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
                return RequestResult.Error<InterfaceVM>("Interface with same name already exists!");

            // create new interface
            var @interface = new CTInterface()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description
            };

            // attach children
            if (request.ChildrenIds.Any())
            {
                var children = await _context.Set<CTInterface>()
                .Where(x => request.ChildrenIds.Contains(x.Id))
                .ToListAsync(cancellationToken);

                @interface.Implementations = new CTInterfaceAssignment()
                {
                    ParentId = @interface.Id,
                    Parent = @interface,
                    References = children
                };
            }

            // attach properties
            foreach (var property in request.Properties)
            {
                property.Id = Guid.NewGuid();
                property.InterfaceId = @interface.Id;
                @interface.Properties.Add(property);
            }

            // save changes
            await _context.AddAsync(@interface, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var vm = _mapper.Map<InterfaceVM>(@interface);
            return RequestResult.Success(vm);
        }
    }
}
