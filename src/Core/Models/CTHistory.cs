using Core.Models.Enumerations;

namespace Core.Models
{
    public class CTHistory
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public HistoryAction Action { get; set; }
        public string Type { get; set; } = string.Empty;
        public Guid EntityId { get; set; }
        public string? Changes { get; set; }
    }
}
