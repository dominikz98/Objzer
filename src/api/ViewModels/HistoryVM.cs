using api.Models;
using AutoMapper;

namespace api.ViewModels
{
    public class HistoryVM
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public HistoryType Type { get; set; }
        public Guid EntityId { get; set; }
        public string? New { get; set; }
        public string? Old { get; set; }
    }

    public class HistoryVMProfile : Profile
    {
        public HistoryVMProfile()
            => CreateMap<CTHistory, HistoryVM>();
    }
}
