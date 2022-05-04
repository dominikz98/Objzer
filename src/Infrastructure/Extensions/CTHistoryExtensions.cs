using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Extensions
{
    internal static class CTHistoryExtensions
    {
        public class CTHistoryConfig : IEntityTypeConfiguration<CTHistory>
        {
            public void Configure(EntityTypeBuilder<CTHistory> builder)
                => builder.ToTable("history")
                .HasIndex(x => x.EntityId);
        }

        public static void ApplyCTHistory(this ModelBuilder builder)
            => builder.ApplyConfiguration(new CTHistoryConfig());

        public static async Task<List<CTHistory>> GetByEntity(this IQueryable<CTHistory> query, Guid id)
            => await query.AsNoTracking()
                .Where(x => x.EntityId == id)
                .OrderByDescending(x => x.Timestamp)
                .ToListAsync();
    }
}
