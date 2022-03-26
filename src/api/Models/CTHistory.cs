namespace api.Models
{
    public class CTHistory
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid EntityId { get; set; }
        public string? New { get; set; }
        public string? Old { get; set; }
        public CTEntity? Entity { get; set; }
    }
}
