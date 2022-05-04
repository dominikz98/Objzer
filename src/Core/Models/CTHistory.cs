using Core.Constants;
using Core.DTOs;
using Core.Models.Enumerations;
using System.Text.Json;

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

        public IReadOnlyCollection<HistoryChange> ParseChanges()
        {
            if (string.IsNullOrWhiteSpace(Changes))
                return Array.Empty<HistoryChange>();

            var result = JsonSerializer.Deserialize<HistoryChange[]>(Changes, JsonConstants.Options);
            return result ?? Array.Empty<HistoryChange>();
        }
    }
}
