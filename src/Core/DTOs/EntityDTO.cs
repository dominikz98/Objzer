using Core.Models.Contracts;

namespace Core.DTOs;

public class EntityDTO : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Properties { get; set; }
    public bool Locked { get; set; }
    public DateOnly? Archived { get; set; }
    public bool Deleted { get; set; }
}
