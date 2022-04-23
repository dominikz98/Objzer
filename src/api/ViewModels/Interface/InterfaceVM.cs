using api.Models;
using AutoMapper;

namespace api.ViewModels.Interface
{
    public class InterfaceVM : AddInterfaceVM
    {
        public Guid Id { get; set; }
        public List<HistoryVM> History { get; set; } = new List<HistoryVM>();
        public new List<PropertyVM> Properties { get; set; } = new List<PropertyVM>();
        public List<ReferenceVM> Includings { get; set; } = new List<ReferenceVM>();
    }

    public class InterfaceVMProfile : Profile
    {
        public InterfaceVMProfile()
            => CreateMap<CTInterface, InterfaceVM>()
                .ForMember(x => x.Includings,
                    x => x.MapFrom(y => y.Includings
                        .Where(z => z.Destination != null)
                        .Select(z => new ReferenceVM
                        {
                            Id = z.Destination!.Id,
                            Name = z.Destination.Name
                        })));
    }
}
