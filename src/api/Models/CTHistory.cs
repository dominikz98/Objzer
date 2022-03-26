using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Models
{
    public class CTHistory
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid EntityId { get; set; }
        public string? New { get; set; }
        public string? Old { get; set; }
        public CTEntity? Entity { get; set; }
    }

    internal class CTHistoryConfig : IEntityTypeConfiguration<CTHistory>
    {
        public void Configure(EntityTypeBuilder<CTHistory> builder)
            => builder.ToTable("history");
    }

    internal static class CTHistoryExtensions
    {
        internal static void ApplyCTHistory(this ModelBuilder builder)
            => builder.ApplyConfiguration(new CTHistoryConfig());
    }
}
