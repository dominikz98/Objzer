namespace api.ViewModels
{
    public class ContractVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int PropertyCount { get; set; }
    }
}
