namespace api.Models
{
    public class CatalogueInterface
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Short { get; set; } = string.Empty;
        public string HexColor { get; set; } = string.Empty;
        public List<CatalogueObject> Objects { get; set; } = new List<CatalogueObject>();
    }
}
