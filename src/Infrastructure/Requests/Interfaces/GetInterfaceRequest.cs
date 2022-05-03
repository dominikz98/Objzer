using AutoMapper;
using Core.Models;
using Core.ViewModels.Interface;
using Infrastructure.Core;
using Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Requests.Interfaces
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
                .Include(x => x.Includings)
                .Include(x => x.Properties)
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (@interface is null)
                return RequestResult<InterfaceVM>.Null();

            // attach history
            @interface.History = await _context.Set<CTHistory>().GetByEntity(@interface.Id);

            var vm = _mapper.Map<InterfaceVM>(@interface);
            return RequestResult<InterfaceVM>.Success(vm);
        }
    }
}
