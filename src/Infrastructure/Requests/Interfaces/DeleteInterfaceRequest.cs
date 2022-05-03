﻿using Core.Models;
using Core.ViewModels;
using Infrastructure.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Requests.Interfaces
{
    public class DeleteInterfaceRequest : IRequest<RequestResult<IdVM>>
    {
        public Guid Id { get; set; }

        public DeleteInterfaceRequest(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteInterfaceRequestHandler : IRequestHandler<DeleteInterfaceRequest, RequestResult<IdVM>>
    {
        private readonly DBContext _context;

        public DeleteInterfaceRequestHandler(DBContext context)
        {
            _context = context;
        }

        public async Task<RequestResult<IdVM>> Handle(DeleteInterfaceRequest request, CancellationToken cancellationToken)
        {
            var @interface = await _context.Set<CTInterface>()
                .Include(x => x.Includings)
                .Include(x => x.Properties)
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (@interface is null)
                return RequestResult<IdVM>.Null();

            // interface
            @interface.Deleted = true;

            // properties
            foreach (var property in @interface.Properties)
                property.Deleted = true;

            // includings
            foreach (var include in @interface.Includings)
                include.Deleted = true;

            _context.Update(@interface);
            await _context.SaveChangesAsync(cancellationToken);

            return RequestResult<IdVM>.Success(new IdVM() { Id = request.Id });
        }
    }
}
