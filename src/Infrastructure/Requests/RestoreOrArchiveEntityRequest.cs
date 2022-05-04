using Core.Models.Contracts;
using Infrastructure.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Requests
{
    public class RestoreOrArchiveEntityRequest<T> : IRequest<EmptyRequestResult> where T : class, IEntity
    {
        public Guid Id { get; set; }
        public DateOnly? Archived { get; set; }

        public RestoreOrArchiveEntityRequest(Guid id, DateOnly? archive)
        {
            Id = id;
            Archived = archive;
        }
    }

    public class RestoreOrArchiveEntityRequestHandler<T> : IRequestHandler<RestoreOrArchiveEntityRequest<T>, EmptyRequestResult> where T : class, IEntity
    {
        private readonly ObjzerContext _context;

        public RestoreOrArchiveEntityRequestHandler(ObjzerContext context)
        {
            _context = context;
        }

        public async Task<EmptyRequestResult> Handle(RestoreOrArchiveEntityRequest<T> request, CancellationToken cancellationToken)
        {
            var entity = await _context.Set<T>()
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (entity is null)
                return EmptyRequestResult.Null();

            if (request.Archived is not null)
                await _context.ArchiveAsync(entity, cancellationToken);
            else
                await _context.RestoreAsync(entity, cancellationToken);

            return EmptyRequestResult.Success();
        }
    }
}
