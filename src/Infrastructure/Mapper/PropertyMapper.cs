using AutoMapper;
using Core.Models;
using Core.ViewModels.Properties;

namespace Infrastructure.Mapper
{
    public class AddPropertyVMProfile : Profile
    {
        public AddPropertyVMProfile()
           => CreateMap<PropertyVM, CTInterfaceProperty>();
    }
}
