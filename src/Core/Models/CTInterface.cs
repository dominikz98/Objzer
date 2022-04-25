using Core.Models.Contracts;
using Core.Models.Enumerations;

namespace Core.Models
{
    public class CTInterface : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Deleted { get; set; }

        public IList<CTInterfaceAssignment> Includings { get; set; } = new List<CTInterfaceAssignment>();
        public IList<CTInterfaceAssignment> Usings { get; set; } = new List<CTInterfaceAssignment>();
        public IList<CTInterfaceProperty> Properties { get; set; } = new List<CTInterfaceProperty>();
        public IList<CTHistory> History { get; set; } = new List<CTHistory>();
    }

    public class CTInterfaceAssignment : ISubEntity
    {
        public Guid ReferenceId { get; set; }
        public CTInterface? Source { get; set; }
        public Guid DestinationId { get; set; }
        public CTInterface? Destination { get; set; }
        public bool Deleted { get; set; }
    }

    public class CTInterfaceProperty : ISubEntity
    {
        public Guid ReferenceId { get; set; }
        public CTInterface? Interface { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool Deleted { get; set; }
        public PropertyType Type { get; set; } = PropertyType.String;
        public bool Required { get; set; } = true;
    }
}
