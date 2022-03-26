using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Models
{
    public class CTEnumeration : CTContract
    {
        public IList<CTEnumerationProperty> Properties { get; set; } = new List<CTEnumerationProperty>();
    }

    internal class CTEnumerationConfig : IEntityTypeConfiguration<CTEnumeration>
    {
        public void Configure(EntityTypeBuilder<CTEnumeration> builder)
            => builder.ToTable("enumerations")
                .HasMany(x => x.Properties)
                .WithOne(x => x.Enumeration)
                .HasForeignKey(x => x.EnumerationId);
    }

    public class CTEnumerationProperty : CTEntity
    {
        public Guid EnumerationId { get; set; }
        public CTEnumeration? Enumeration { get; set; }
        public PropertyTypes Type { get; set; } = PropertyTypes.String;
        public bool Required { get; set; } = true;
    }

    internal class CTEnumerationPropertyConfig : IEntityTypeConfiguration<CTEnumerationProperty>
    {
        public void Configure(EntityTypeBuilder<CTEnumerationProperty> builder)
            => builder.ToTable("enumeration_properties");
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
