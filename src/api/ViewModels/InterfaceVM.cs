using api.Models;
using AutoMapper;

namespace api.ViewModels
{
    public class InterfaceVM : AddInterfaceVM
    {
        public Guid Id { get; set; }
        public List<InterfaceVM> Children { get; set; } = new List<InterfaceVM>();
    }

    public class InterfaceVMProfile : Profile
    {
        public InterfaceVMProfile()
        {
            CreateMap<CTInterface, InterfaceVM>();
        }
    }
}
