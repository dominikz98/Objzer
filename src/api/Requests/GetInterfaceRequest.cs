using api.Core;
using api.Models;
using api.ViewModels.Interface;
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
        private readonly IHistoryLoader _historyLoader;
        private readonly IMapper _mapper;
        private readonly DBContext _context;

        public GetInterfaceRequestHandler(IHistoryLoader historyLoader, IMapper mapper, DBContext context)
        {
            _historyLoader = historyLoader;
            _mapper = mapper;
            _context = context;
        }

        public async Task<RequestResult<InterfaceVM>> Handle(GetInterfaceRequest request, CancellationToken cancellationToken)
        {
            // load interface with references
            var @interface = await _context.Set<CTInterface>()
                .Include(x => x.Includings)
                .Include(x => x.Properties)
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (@interface is null)
                return RequestResult.Null<InterfaceVM>();

            // attach history
            @interface.History = await _historyLoader.Load(@interface, cancellationToken);

            var vm = _mapper.Map<InterfaceVM>(@interface);
            return RequestResult.Success(vm);
        }
    }
}
