namespace Core.ViewModels.Objects;

public class AddObjectVM
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<ObjectPropertyVM> Properties { get; set; } = new List<ObjectPropertyVM>();
    public List<Guid> IncludingIds { get; set; } = new List<Guid>();
}
