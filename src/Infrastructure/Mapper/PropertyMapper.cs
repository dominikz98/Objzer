using AutoMapper;
using Core.Models;
using Core.ViewModels.Properties;

namespace Infrastructure.Mapper
{
    public class PropertyVMProfile : Profile
    {
        public PropertyVMProfile()
           => CreateMap<PropertyVM, CTInterfaceProperty>()
            .ReverseMap();
    }
}
