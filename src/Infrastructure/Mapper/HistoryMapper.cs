using AutoMapper;
using Core.Models;
using Core.ViewModels;

namespace Infrastructure.Mapper
{
    public class HistoryVMProfile : Profile
    {
        public HistoryVMProfile()
            => CreateMap<CTHistory, HistoryVM>();
    }
}
