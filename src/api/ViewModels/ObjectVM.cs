namespace api.ViewModels
{
    public class ObjectVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int PropertyCount { get; set; }
        public int AbstractionCount { get; set; }
        public int InterfaceCount { get; set; }
    }
}
