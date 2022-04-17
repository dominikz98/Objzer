using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Models
{
    public class CTEnumeration : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Deleted { get; set; }
        public IList<CTHistory> History { get; set; } = new List<CTHistory>();
        public IList<CTEnumerationProperty> Properties { get; set; } = new List<CTEnumerationProperty>();
    }

    internal class CTEnumerationConfig : IEntityTypeConfiguration<CTEnumeration>
    {
        public void Configure(EntityTypeBuilder<CTEnumeration> builder)
        {
            builder.ToTable("enumerations");

            builder.HasMany(x => x.Properties)
            .WithOne(x => x.Enumeration)
            .HasForeignKey(x => x.EnumerationId);

            builder.HasIndex(x => x.Deleted);
        }
    }

    public class CTEnumerationProperty : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Deleted { get; set; }
        public Guid EnumerationId { get; set; }
        public CTEnumeration? Enumeration { get; set; }
        public PropertyType Type { get; set; } = PropertyType.String;
        public bool Required { get; set; } = true;
        public IList<CTHistory> History { get; set; } = new List<CTHistory>();
    }

    internal class CTEnumerationPropertyConfig : IEntityTypeConfiguration<CTEnumerationProperty>
    {
        public void Configure(EntityTypeBuilder<CTEnumerationProperty> builder)
        {
            builder.ToTable("enumeration_properties");
            builder.HasIndex(x => x.EnumerationId);
            builder.HasIndex(x => x.Deleted);
        }
    }

    internal static class CTEnumerationExtensions
    {
        internal static void ApplyCTEnumeration(this ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CTEnumerationConfig());
            builder.ApplyConfiguration(new CTEnumerationPropertyConfig());
        }
    }
}
