using Core.Models.Contracts;
using Core.Models.Enumerations;

namespace Core.Models;

public class CTObject : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool Locked { get; set; }
    public DateOnly? Archived { get; set; }
    public bool Deleted { get; set; }

    public IList<CTInterface> Interfaces { get; set; } = new List<CTInterface>();
    public IList<CTObjectProperty> Properties { get; set; } = new List<CTObjectProperty>();
    public IList<CTHistory> History { get; set; } = new List<CTHistory>();
}

public class CTObjectProperty : IDeletable
{
    public Guid ObjectId { get; set; }
    public CTObject? Object { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public PropertyType Type { get; set; } = PropertyType.String;
    public bool Required { get; set; } = true;
    public bool Deleted { get; set; }
}
