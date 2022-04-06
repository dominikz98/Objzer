namespace api.ViewModels
{
    public class ListInterfaceVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int HistoryCount { get; set; }
        public int PropertiesCount { get; set; }
        public int ObjectsCount { get; set; }
    }
}
