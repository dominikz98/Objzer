using api.Models;
using MediatR;

namespace api.Requests
{
    public class GetObjectsRequest : IRequest<IReadOnlyCollection<CatalogueObject>> { }

    public class GetObjectsRequestHandler : IRequestHandler<GetObjectsRequest, IReadOnlyCollection<CatalogueObject>> 
    { 
    
    }
}
