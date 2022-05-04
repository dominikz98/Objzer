using Core.Models.Contracts;
using Infrastructure.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Requests
{
    public class UnLockEntityRequest<T> : IRequest<EmptyRequestResult> where T : class, IEntity
    {
        public Guid Id { get; set; }
        public bool Lock { get; set; }

        public UnLockEntityRequest(Guid id, bool @lock)
        {
            Id = id;
            Lock = @lock;
        }
    }

    public class UnLockEntityRequestHandler<T> : IRequestHandler<UnLockEntityRequest<T>, EmptyRequestResult> where T : class, IEntity
    {
        private readonly ObjzerContext _context;

        public UnLockEntityRequestHandler(ObjzerContext context)
        {
            _context = context;
        }

        public async Task<EmptyRequestResult> Handle(UnLockEntityRequest<T> request, CancellationToken cancellationToken)
        {
            var entity = await _context.Set<T>()
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (entity is null)
                return EmptyRequestResult.Null();

            if (request.Lock)
                await _context.LockAsync(entity, cancellationToken);
            else
                await _context.UnlockAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            return EmptyRequestResult.Success();
        }
    }
}
