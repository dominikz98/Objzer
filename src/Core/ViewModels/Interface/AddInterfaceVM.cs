using Core.ViewModels.Properties;

namespace Core.ViewModels.Interface
{
    public class AddInterfaceVM
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<PropertyVM> Properties { get; set; } = new List<PropertyVM>();
        public List<Guid> IncludingIds { get; set; } = new List<Guid>();
    }
}
