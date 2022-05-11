using Core.ViewModels;
using MediatR;

namespace Infrastructure.Requests
{
    public class GetDataFromEnumRequest : IRequest<IReadOnlyCollection<EnumVM>>
    {
        public Type Enumeration { get; }

        public GetDataFromEnumRequest(Type enumeration)
        {
            Enumeration = enumeration;
        }
    }

    public class GetDataFromEnumRequestHandler : IRequestHandler<GetDataFromEnumRequest, IReadOnlyCollection<EnumVM>>
    {
        public Task<IReadOnlyCollection<EnumVM>> Handle(GetDataFromEnumRequest request, CancellationToken _)
        {
            var result = Enum
                .GetNames(request.Enumeration)
                .Select(x => new EnumVM()
                {
                    Index = (int)Enum.Parse(request.Enumeration, x),
                    Name = x
                })
                .OrderBy(x => x.Index)
                .ToList();

            return Task.FromResult<IReadOnlyCollection<EnumVM>>(result);
        }
    }
}
