namespace Core.ViewModels.Interfaces;

public class InterfaceVM : AddInterfaceVM
{
    public Guid Id { get; set; }
    public bool Locked { get; set; }
    public DateTime? Archived { get; set; }

    public List<HistoryVM> History { get; set; } = new List<HistoryVM>();
    public new List<InterfacePropertyVM> Properties { get; set; } = new List<InterfacePropertyVM>();
}
