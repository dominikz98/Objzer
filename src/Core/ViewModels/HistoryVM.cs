using Core.Models.Enumerations;

namespace Core.ViewModels
{
    public class HistoryVM
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public HistoryAction Type { get; set; }
        public string? Changes { get; set; }
    }
}
