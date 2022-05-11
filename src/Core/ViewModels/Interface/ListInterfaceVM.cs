namespace Core.ViewModels.Interface
{
    public class ListInterfaceVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime LastModified { get; set; }
        public int HistoryCount { get; set; }
        public int PropertiesCount { get; set; }
        public bool Locked { get; set; }
        public DateTime? Archived { get; set; }
    }
}
