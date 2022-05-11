using Core.DTOs;
using Core.Models.Enumerations;

namespace Core.ViewModels
{
    public class HistoryVM
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public HistoryAction Action { get; set; }
        public List<HistoryChange> Changes { get; set; } = new();
    }
}
