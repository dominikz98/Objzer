namespace Core.DTOs;

public class HistoryChange
{
    public string Name { get; set; } = string.Empty;
    public object? New { get; set; }
    public object? Old { get; set; }
}
