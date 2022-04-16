using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Models
{
    public class CTHistory
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public HistoryType Type { get; set; }
        public Guid EntityId { get; set; }
        public string? Changes { get; set; }
    }

    internal class CTHistoryConfig : IEntityTypeConfiguration<CTHistory>
    {
        public void Configure(EntityTypeBuilder<CTHistory> builder)
            => builder.ToTable("history")
            .HasIndex(x => x.EntityId);
    }

    internal static class CTHistoryExtensions
    {
        internal static void ApplyCTHistory(this ModelBuilder builder)
            => builder.ApplyConfiguration(new CTHistoryConfig());
    }

    public enum HistoryType
    {
        Add,
        Update,
        Delete
    }
}
