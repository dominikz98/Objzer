using api.Models;
using AutoMapper;

namespace api.ViewModels
{
    public class ListVM
    {
        public Guid Id { get; set; }
        public ListType Type { get; set; }
        public List<ListReferenceVM> References { get; set; } = new List<ListReferenceVM>();
    }

    public enum ListType
    {
        Custom,
        Interface,
        Enumeration,
        Abstraction,
        Object
    }

    public class ListReferenceVM
    {
        public ListReferenceType Type { get; set; }
        public int Count { get; set; }
    }

    public enum ListReferenceType
    {
        Custom,
        History,
        Property,
        Interfaces,
        Objects
    }

    public class ListVMProfile : Profile
    {
        public ListVMProfile()
        {
            CreateMap<CTInterface, ListVM>()
                .ForMember(x => x.Type, x => x.MapFrom(y => ListType.Interface))
                .ForMember(x => x.References, x => x.MapFrom(y => new List<ListReferenceVM>()
                    {
                        new () { Type = ListReferenceType.History, Count = y.History.Count },
                        new () { Type = ListReferenceType.Property, Count = y.Properties.Count },
                        new () { Type = ListReferenceType.Interfaces, Count = y.Implementations.References.Count },
                        new () { Type = ListReferenceType.Objects, Count = y.Objects.Count }
                    }));

            CreateMap<CTEnumeration, ListVM>()
                .ForMember(x => x.Type, x => x.MapFrom(y => ListType.Enumeration))
                .ForMember(x => x.References, x => x.MapFrom(y => new List<ListReferenceVM>()
                    {
                        new () { Type = ListReferenceType.History, Count = y.History.Count },
                        new () { Type = ListReferenceType.Property, Count = y.Properties.Count }
                    }));

            CreateMap<CTAbstraction, ListVM>()
                .ForMember(x => x.Type, x => x.MapFrom(y => ListType.Abstraction))
                .ForMember(x => x.References, x => x.MapFrom(y => new List<ListReferenceVM>()
                    {
                        new () { Type = ListReferenceType.History, Count = y.History.Count },
                        new () { Type = ListReferenceType.Property, Count = y.Properties.Count },
                        new () { Type = ListReferenceType.Interfaces, Count = y.Inheritances.Count },
                        new () { Type = ListReferenceType.Objects, Count = y.Objects.Count }
                    }));

            CreateMap<CTObject, ListVM>()
                .ForMember(x => x.Type, x => x.MapFrom(y => ListType.Object))
                .ForMember(x => x.References, x => x.MapFrom(y => new List<ListReferenceVM>()
                    {
                        new () { Type = ListReferenceType.History, Count = y.History.Count },
                        new () { Type = ListReferenceType.Property, Count = y.Properties.Count },
                        new () { Type = ListReferenceType.Interfaces, Count = y.Interfaces.Count },
                        new () { Type = ListReferenceType.Objects, Count = y.Abstractions.Count }
                    }));
        }
    }
}
