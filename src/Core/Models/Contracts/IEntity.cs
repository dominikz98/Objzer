namespace Core.Models.Contracts
{
    public interface IEntity : IDeletable
    {
        Guid Id { get; set; }
        string Name { get; set; }
        bool Locked { get; set; }
        DateOnly? Archived { get; set; }
    }
}
