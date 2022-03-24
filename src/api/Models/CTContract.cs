namespace api.Models
{
    public abstract class CTContract
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Short { get; set; } = string.Empty;
        public string HexColor { get; set; } = string.Empty;
        public List<CTObject> Objects { get; set; } = new List<CTObject>();
    }
}
