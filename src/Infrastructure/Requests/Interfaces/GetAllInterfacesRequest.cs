﻿using Core.DTOs;
using Core.Models;
using Core.ViewModels.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Requests.Interfaces;

public class GetAllInterfacesRequest : IRequest<IReadOnlyCollection<ListInterfaceVM>> { }

public class GetAllInterfacesRequestHandler : IRequestHandler<GetAllInterfacesRequest, IReadOnlyCollection<ListInterfaceVM>>
{
    private readonly ObjzerContext _context;

    public GetAllInterfacesRequestHandler(ObjzerContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<ListInterfaceVM>> Handle(GetAllInterfacesRequest request, CancellationToken cancellationToken)
    {
        // load from db
        var interfaces = await _context.Set<CTInterface>()
            .Select(x => new EntityDTO
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Properties = x.Properties.Count,
                Locked = x.Locked,
                Archived = x.Archived
            })
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);

        // load history
        var interfaceIds = interfaces.Select(x => x.Id);
        var history = await _context.Set<CTHistory>()
            .Where(x => interfaceIds.Contains(x.EntityId))
            .Select(x => new { x.EntityId, x.Timestamp })
            .ToListAsync(cancellationToken);

        // create viewmodels
        return interfaces
            .Select(x => new ListInterfaceVM()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Archived = x.Archived?.ToDateTime(TimeOnly.MinValue),
                Locked = x.Locked,
                LastModified = history.Where(y => y.EntityId == x.Id).Max(y => y.Timestamp),
                HistoryCount = history.Where(y => y.EntityId == x.Id).Count(),
                PropertiesCount = x.Properties
            })
            .OrderBy(x => x.Name)
            .ToList();
    }
}
