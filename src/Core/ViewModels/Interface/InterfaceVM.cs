using Core.ViewModels.Properties;

namespace Core.ViewModels.Interface
{
    public class InterfaceVM : AddInterfaceVM
    {
        public Guid Id { get; set; }
        public bool Locked { get; set; }
        public DateOnly? Archived { get; set; }

        public List<ReferenceVM> Includings { get; set; } = new List<ReferenceVM>();
        public List<HistoryVM> History { get; set; } = new List<HistoryVM>();
        public new List<PropertyVM> Properties { get; set; } = new List<PropertyVM>();
    }
}
