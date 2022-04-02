using api.Models;
using AutoMapper;

namespace api.ViewModels
{
    public class PropertyVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IList<HistoryVM> History { get; set; } = new List<HistoryVM>();
        public PropertyTypes Type { get; set; } = PropertyTypes.String;
        public bool Required { get; set; } = true;
    }

    public class PropertyVMProfile : Profile
    {
        public PropertyVMProfile()
            => CreateMap<CTInterfaceProperty, PropertyVM>();
    }
}
