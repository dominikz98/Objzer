using AutoMapper;
using Core.Models;
using Core.ViewModels.Interface;
using Infrastructure.Requests.Interfaces;

namespace Infrastructure.Mapper
{
    public class AddInterfaceVMProfile : Profile
    {
        public AddInterfaceVMProfile()
           => CreateMap<AddInterfaceVM, AddInterfaceRequest>();
    }

    public class EditInterfaceVMProfile : Profile
    {
        public EditInterfaceVMProfile()
           => CreateMap<InterfaceVM, EditInterfaceRequest>();
    }

    public class InterfaceVMProfile : Profile
    {
        public InterfaceVMProfile()
            => CreateMap<CTInterface, InterfaceVM>()
                .ForMember(x => x.Archived,
                    x => x.MapFrom(y => y.Archived == null ? (DateTime?)null : y.Archived.Value.ToDateTime(TimeOnly.MinValue)))
                .ForMember(x => x.IncludingIds,
                    x => x.MapFrom(y => y.Includings
                        .Select(z => z.DestinationId)));
    }
}
