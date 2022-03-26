using api.Core;
using api.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace api.Requests
{
    public class AddInterfaceRequest : IRequest<RequestResult<CTInterface>>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Guid> ChildrenIds { get; set; } = new List<Guid>();
    }

    public class AddInterfaceRequestHandler : IRequestHandler<AddInterfaceRequest, RequestResult<CTInterface>>
    {
        private readonly DBContext _context;

        public AddInterfaceRequestHandler(DBContext context)
        {
            _context = context;
        }

        public async Task<RequestResult<CTInterface>> Handle(AddInterfaceRequest request, CancellationToken cancellationToken)
        {
            // validate
            var alreadyExists = await _context.Set<CTInterface>()
                .Where(x => x.Name == request.Name)
                .AnyAsync(cancellationToken);

            if (alreadyExists)
                return RequestResult.Error<CTInterface>("Interface with same name already exists!");

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

                var assignment = new CTInterfaceAssignment()
                {
                    ParentId = @interface.Id,
                    Parent = @interface,
                    Children = children
                };
                @interface.Implementations.Add(assignment);
            }

            // save changes
            await _context.AddAsync(@interface, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return RequestResult.Success(@interface);
        }
    }
}
