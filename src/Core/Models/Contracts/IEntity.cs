namespace Core.Models.Contracts
{
    public interface IEntity : IDeletable
    {
        Guid Id { get; set; }
        string Name { get; set; }
        bool Locked { get; set; }
        DateOnly? Archived { get; set; }
    }

    public interface ISubEntity : IDeletable
    {
        Guid ReferenceId { get; set; }
    }
}
