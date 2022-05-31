namespace Core.ViewModels.Interface
{
    public class AddInterfaceVM
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<InterfacePropertyVM> Properties { get; set; } = new List<InterfacePropertyVM>();
        public List<Guid> IncludingIds { get; set; } = new List<Guid>();
    }
}
