using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Models
{
    public class CTObject : CTEntity
    {
        public IList<CTContract> Contracts { get; set; } = new List<CTContract>();
        public IList<CTObjectProperty> Properties { get; set; } = new List<CTObjectProperty>();
    }

    internal class CTObjectConfig : IEntityTypeConfiguration<CTObject>
    {
        public void Configure(EntityTypeBuilder<CTObject> builder)
        {
            builder.ToTable("objects")
                .HasMany(x => x.Contracts)
                .WithMany(x => x.Objects);

            builder.HasMany(x => x.Properties)
                .WithOne(x => x.Object);
        }
    }

    public class CTObjectProperty : CTEntity
    {
        public bool Key { get; set; }
        public PropertyTypes Type { get; set; } = PropertyTypes.String;
        public string Column { get; set; } = string.Empty;
        public bool Required { get; set; } = true;
        public int? MaxLength { get; set; }
        public int? StringLength { get; set; }

        public CTObject? Object { get; set; }
    }

    internal class CTObjectPropertyConfig : IEntityTypeConfiguration<CTObjectProperty>
    {
        public void Configure(EntityTypeBuilder<CTObjectProperty> builder)
            => builder.ToTable("object_properties");
    }

    internal static class CTObjectPropertyExtensions
    {
        internal static void ApplyCTObject(this ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CTObjectConfig());
            builder.ApplyConfiguration(new CTObjectPropertyConfig());
        }
    }

    public enum PropertyTypes
    {
        Bool,
        Byte,
        Char,
        String,
        Double,
        Short,
        Int,
        Long
    }
}
