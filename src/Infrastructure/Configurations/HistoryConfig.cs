using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public static class CTHistoryConfigExtensions
    {
        public static void ApplyCTHistory(this ModelBuilder builder)
        => builder.ApplyConfiguration(new CTHistoryConfig());
    }

    public class CTHistoryConfig : IEntityTypeConfiguration<CTHistory>
    {
        public void Configure(EntityTypeBuilder<CTHistory> builder)
            => builder.ToTable("history")
            .HasIndex(x => x.EntityId);
    }
}
