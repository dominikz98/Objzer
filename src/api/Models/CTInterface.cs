using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Models
{
    public class CTInterface : CTEntity
    {
        public IList<CTObject> Objects { get; set; } = new List<CTObject>();
        public IList<CTInterfaceAssignment> Implementations { get; set; } = new List<CTInterfaceAssignment>();
        public IList<CTInterfaceProperty> Properties { get; set; } = new List<CTInterfaceProperty>();
    }

    internal class CTInterfaceConfig : IEntityTypeConfiguration<CTInterface>
    {
        public void Configure(EntityTypeBuilder<CTInterface> builder)
            => builder.ToTable("interfaces")
                .HasMany(x => x.Properties)
                .WithOne(x => x.Interface)
                .HasForeignKey(x => x.InterfaceId);
    }

    public class CTInterfaceAssignment
    {
        public Guid ParentId { get; set; }
        public CTInterface? Parent { get; set; }
        public IList<CTInterface> Children { get; set; } = new List<CTInterface>();
    }

    internal class CTInterfaceAssignmentConfig : IEntityTypeConfiguration<CTInterfaceAssignment>
    {
        public void Configure(EntityTypeBuilder<CTInterfaceAssignment> builder)
        {
            builder.ToTable("interface_assignments")
                .HasKey(x => x.ParentId);

            builder.HasOne(x => x.Parent)
                .WithMany(x => x.Implementations)
                .HasForeignKey(x => x.ParentId);
        }
    }

    public class CTInterfaceProperty : CTEntity
    {
        public Guid InterfaceId { get; set; }
        public CTInterface? Interface { get; set; }
        public PropertyTypes Type { get; set; } = PropertyTypes.String;
        public bool Required { get; set; } = true;
    }

    internal class CTInterfacePropertyConfig : IEntityTypeConfiguration<CTInterfaceProperty>
    {
        public void Configure(EntityTypeBuilder<CTInterfaceProperty> builder)
            => builder.ToTable("interface_properties");
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
