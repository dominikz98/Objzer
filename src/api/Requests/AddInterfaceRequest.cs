using api.Core;
using api.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace api.Requests
{
    public class AddInterfaceRequest : IRequest<CTInterface>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Guid> ChildrenIds { get; set; } = new List<Guid>();
    }

    public class AddInterfaceValidator : AbstractValidator<AddInterfaceRequest>
    {
        public AddInterfaceValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }

    public class AddInterfaceRequestHandler : IRequestHandler<AddInterfaceRequest, CTInterface>
    {
        private readonly DBContext _context;

        public AddInterfaceRequestHandler(DBContext context)
        {
            _context = context;
        }

        public async Task<CTInterface> Handle(AddInterfaceRequest request, CancellationToken cancellationToken)
        {
            // create new interface
            var newInterface = new CTInterface()
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
                    ParentId = newInterface.Id,
                    Parent = newInterface,
                    Children = children
                };
                newInterface.Implementations.Add(assignment);
            }

            // save changes
            await _context.AddAsync(newInterface, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return newInterface;
        }
    }
}
