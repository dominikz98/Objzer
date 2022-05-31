namespace Core.ViewModels.Objects;

public class ObjectVM : AddObjectVM
{
    public Guid Id { get; set; }
    public bool Locked { get; set; }
    public DateTime? Archived { get; set; }

    public List<HistoryVM> History { get; set; } = new List<HistoryVM>();
    public new List<ObjectPropertyVM> Properties { get; set; } = new List<ObjectPropertyVM>();
}
