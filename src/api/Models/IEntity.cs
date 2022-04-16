namespace api.Models
{
    public interface IEntity : IDeletable
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        IList<CTHistory> History { get; set; }
        bool Deleted { get; set; }
    }

    public interface IDeletable
    {
        bool Deleted { get; set; }
    }

    //public class CTEntity
    //{
    //    public Guid Id { get; set; }
    //    public string Name { get; set; } = string.Empty;
    //    public string Description { get; set; } = string.Empty;
    //    public bool Deleted { get; set; }
    //}
}
