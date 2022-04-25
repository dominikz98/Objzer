using Core.ViewModels.Properties;

namespace Core.ViewModels.Interface
{
    public class EditInterfaceVM : AddInterfaceVM
    {
        public Guid Id { get; set; }
        public new List<PropertyVM> Properties { get; set; } = new List<PropertyVM>();
    }
}
