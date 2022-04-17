namespace api.Models
{
    public interface IEntity : IDeletable
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }

    public interface IDeletable
    {
        bool Deleted { get; set; }
    }
}
