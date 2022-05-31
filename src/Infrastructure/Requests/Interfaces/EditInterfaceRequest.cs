using AutoMapper;
using Core.Models;
using Core.ViewModels.Interfaces;
using Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Requests.Interfaces;

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
    private readonly IMapper _mapper;
    private readonly ObjzerContext _context;

    public EditInterfaceRequestHandler(IMapper mapper, ObjzerContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<RequestResult<InterfaceVM>> Handle(EditInterfaceRequest request, CancellationToken cancellationToken)
    {
        // load interface
        var @interface = await _context.Set<CTInterface>()
            .IgnoreQueryFilters()
            .Include(x => x.Properties)
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (@interface is null)
            return RequestResult<InterfaceVM>.Null();

        // mappings
        @interface.Name = request.Name;
        @interface.Description = request.Description;
        await UpdateProperties(@interface, request.Properties, cancellationToken);

        // save changes
        _context.Update(@interface);
        await _context.SaveChangesAsync(cancellationToken);

        // attach history
        @interface.History = await _context.Set<CTHistory>().GetByEntity(@interface.Id);

        var vm = _mapper.Map<InterfaceVM>(@interface);
        return RequestResult<InterfaceVM>.Success(vm);
    }

    private async Task UpdateProperties(CTInterface @interface, List<CTInterfaceProperty> currentProperties, CancellationToken cancellationToken)
    {
        // remove missing properties
        foreach (var original in @interface.Properties)
        {
            var contains = currentProperties.Any(x => x.Name == original.Name);
            if (contains)
                continue;

            original.Deleted = true;
        }

        foreach (var current in currentProperties)
        {
            var original = @interface.Properties
                .Where(x => x.Name == current.Name)
                .FirstOrDefault();

            // update original
            if (original is not null)
            {
                original.Name = original.Name;
                original.Description = original.Description;
                original.Type = original.Type;
                original.Required = original.Required;
                original.Deleted = false;
                continue;
            }

            // add new
            current.InterfaceId = @interface.Id;
            @interface.Properties.Add(current);
            await _context.AddAsync(current, cancellationToken);
        }
    }
}
