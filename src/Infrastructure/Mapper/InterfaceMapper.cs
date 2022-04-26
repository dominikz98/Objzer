using AutoMapper;
using Core.Models;
using Core.ViewModels;
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
           => CreateMap<EditInterfaceVM, EditInterfaceRequest>();
    }

    public class InterfaceVMProfile : Profile
    {
        public InterfaceVMProfile()
            => CreateMap<CTInterface, InterfaceVM>();
                //.ForMember(x => x.Includings,
                //    x => x.MapFrom(y => y.Includings
                //        .Where(z => z.Destination != null)
                //        .Select(z => new ReferenceVM
                //        {
                //            Id = z.Destination!.Id,
                //            Name = z.Destination.Name
                //        })));
    }
}
