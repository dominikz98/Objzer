using api.Models;
using AutoMapper;

namespace api.ViewModels.Interface
{
    public class InterfaceVM : AddInterfaceVM
    {
        public Guid Id { get; set; }
        public List<HistoryVM> History { get; set; } = new List<HistoryVM>();
        public new List<PropertyVM> Properties { get; set; } = new List<PropertyVM>();
        public List<ReferenceVM> Implementations { get; set; } = new List<ReferenceVM>();
    }

    public class InterfaceVMProfile : Profile
    {
        public InterfaceVMProfile()
            => CreateMap<CTInterface, InterfaceVM>()
                .ForMember(x => x.Implementations, x => x.MapFrom(y => y.Implementations.References.Select(z => new ReferenceVM { Id = z.Id, Name = z.Name })));
    }
}
