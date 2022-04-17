using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Models
{
    public class CTInterface : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Deleted { get; set; }
        public CTInterfaceAssignment Implementations { get; set; } = new CTInterfaceAssignment();
        public IList<CTObject> Objects { get; set; } = new List<CTObject>();
        public IList<CTHistory> History { get; set; } = new List<CTHistory>();
        public IList<CTInterfaceProperty> Properties { get; set; } = new List<CTInterfaceProperty>();
    }

    internal class CTInterfaceConfig : IEntityTypeConfiguration<CTInterface>
    {
        public void Configure(EntityTypeBuilder<CTInterface> builder)
        {
            builder.ToTable("interfaces")
                .HasMany(x => x.Properties)
                .WithOne(x => x.Interface)
                .HasForeignKey(x => x.InterfaceId);

            builder.HasOne(x => x.Implementations)
                .WithOne(x => x.Parent);

            builder.HasIndex(x => x.Deleted);
        }
    }

    public class CTInterfaceAssignment : IDeletable
    {
        public Guid ParentId { get; set; }
        public CTInterface? Parent { get; set; }
        public IList<CTInterface> References { get; set; } = new List<CTInterface>();
        public bool Deleted { get; set; }
    }

    internal class CTInterfaceAssignmentConfig : IEntityTypeConfiguration<CTInterfaceAssignment>
    {
        public void Configure(EntityTypeBuilder<CTInterfaceAssignment> builder)
        {
            builder.ToTable("interface_assignments");
            builder.HasKey(x => x.ParentId);
            builder.HasIndex(x => x.Deleted);
        }
    }

    public class CTInterfaceProperty : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Deleted { get; set; }
        public Guid InterfaceId { get; set; }
        public CTInterface? Interface { get; set; }
        public PropertyType Type { get; set; } = PropertyType.String;
        public bool Required { get; set; } = true;
        public IList<CTHistory> History { get; set; } = new List<CTHistory>();
    }

    internal class CTInterfacePropertyConfig : IEntityTypeConfiguration<CTInterfaceProperty>
    {
        public void Configure(EntityTypeBuilder<CTInterfaceProperty> builder)
        {
            builder.ToTable("interface_properties");
            builder.HasIndex(x => x.InterfaceId);
            builder.HasIndex(x => x.Deleted);
        }
    }

    internal static class CTInterfaceExtensions
    {
        internal static void ApplyCTInterface(this ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CTInterfaceConfig());
            builder.ApplyConfiguration(new CTInterfaceAssignmentConfig());
            builder.ApplyConfiguration(new CTInterfacePropertyConfig());
        }
    }
}
