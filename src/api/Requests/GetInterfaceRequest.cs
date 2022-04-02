using api.Core;
using api.Models;
using api.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace api.Requests
{
    public class GetInterfaceRequest : IRequest<RequestResult<InterfaceVM>>
    {
        public Guid Id { get; set; }

        public GetInterfaceRequest(Guid id)
        {
            Id = id;
        }
    }

    public class GetInterfaceRequestHandler : IRequestHandler<GetInterfaceRequest, RequestResult<InterfaceVM>>
    {
        private readonly IMapper _mapper;
        private readonly DBContext _context;

        public GetInterfaceRequestHandler(IMapper mapper, DBContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<RequestResult<InterfaceVM>> Handle(GetInterfaceRequest request, CancellationToken cancellationToken)
        {
            // load interface with references
            var @interface = await _context.Set<CTInterface>()
                .Include(x => x.History)
                .Include(x => x.Objects)
                .Include(x => x.Implementations)
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (@interface is null)
                return RequestResult.Null<InterfaceVM>();

            // attach properties with history
            var propertyIds = @interface.Properties.Select(x => x.Id);
            @interface.Properties = await _context.Set<CTInterfaceProperty>()
                .Include(x => x.History)
                .AsNoTracking()
                .Where(x => propertyIds.Contains(x.Id))
                .ToListAsync(cancellationToken);

            var vm = _mapper.Map<InterfaceVM>(@interface);
            return RequestResult.Success(vm);
        }
    }
}
