//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Models
{
    //    public class CTObject : IEntity
    //    {
    //        public Guid Id { get; set; }
    //        public string Name { get; set; } = string.Empty;
    //        public string Description { get; set; } = string.Empty;
    //        public bool Deleted { get; set; }
    //        public IList<CTAbstraction> Abstractions { get; set; } = new List<CTAbstraction>();
    //        public IList<CTInterface> Interfaces { get; set; } = new List<CTInterface>();
    //        public IList<CTObjectProperty> Properties { get; set; } = new List<CTObjectProperty>();
    //    }

    //    internal class CTObjectConfig : IEntityTypeConfiguration<CTObject>
    //    {
    //        public void Configure(EntityTypeBuilder<CTObject> builder)
    //        {
    //            builder.ToTable("objects")
    //                .HasMany(x => x.Interfaces)
    //                .WithMany(x => x.Objects);

    //            builder.HasMany(x => x.Abstractions)
    //                .WithMany(x => x.Objects);

    //            builder.HasMany(x => x.Properties)
    //                .WithOne(x => x.Object);

    //            builder.HasIndex(x => x.Deleted);
    //        }
    //    }

    //    public class CTObjectProperty : IEntity
    //    {
    //        public Guid Id { get; set; }
    //        public string Name { get; set; } = string.Empty;
    //        public string Description { get; set; } = string.Empty;
    //        public bool Deleted { get; set; }
    //        public bool Key { get; set; }
    //        public PropertyType Type { get; set; } = PropertyType.String;
    //        public string Column { get; set; } = string.Empty;
    //        public bool Required { get; set; } = true;
    //        public int? MaxLength { get; set; }
    //        public int? StringLength { get; set; }
    //        public CTObject? Object { get; set; }
    //    }

    //    internal class CTObjectPropertyConfig : IEntityTypeConfiguration<CTObjectProperty>
    //    {
    //        public void Configure(EntityTypeBuilder<CTObjectProperty> builder)
    //        {
    //            builder.ToTable("object_properties");
    //            builder.HasIndex(x => x.Deleted);
    //        }
    //    }

    //    internal static class CTObjectPropertyExtensions
    //    {
    //        internal static void ApplyCTObject(this ModelBuilder builder)
    //        {
    //            builder.ApplyConfiguration(new CTObjectConfig());
    //            builder.ApplyConfiguration(new CTObjectPropertyConfig());
    //        }
    //    }

    public enum PropertyType
    {
        Guid,
        String,
        Int,
        Double,
        Decimal,
        Bool,
        Byte,
        Char,
        Short,
        Long
    }
}
