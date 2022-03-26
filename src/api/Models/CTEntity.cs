using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Models
{
    public abstract class CTEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IList<CTHistory> History { get; set; } = new List<CTHistory>();
    }

    internal class CTEntityConfig : IEntityTypeConfiguration<CTEntity>
    {
        public void Configure(EntityTypeBuilder<CTEntity> builder)
            => builder.ToTable("entities")
                .HasMany(x => x.History)
                .WithOne(x => x.Entity);
    }

    internal static class CTEntityExtensions
    {
        internal static void ApplyCTEntity(this ModelBuilder builder)
            => builder.ApplyConfiguration(new CTEntityConfig());
    }
}
