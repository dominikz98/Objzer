namespace api.Models
{
    public class CTObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<CTContract> Interfaces { get; set; } = new List<CTContract>();
    }
}
