namespace api.Models
{
    public class CatalogueObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<Guid> CatalogueInterfaceIds { get; set; } = new HashSet<Guid>();
    }
}
