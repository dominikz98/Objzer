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
        public IList<CTInterfaceAssignment> Includings { get; set; } = new List<CTInterfaceAssignment>();
        public IList<CTInterfaceAssignment> Usings { get; set; } = new List<CTInterfaceAssignment>();
        public IList<CTInterfaceProperty> Properties { get; set; } = new List<CTInterfaceProperty>();
        public IList<CTHistory> History { get; set; } = new List<CTHistory>();
    }

    internal class CTInterfaceConfig : IEntityTypeConfiguration<CTInterface>
    {
        public void Configure(EntityTypeBuilder<CTInterface> builder)
        {
            builder.ToTable("interfaces")
                .HasMany(x => x.Properties)
                .WithOne(x => x.Interface)
                .HasForeignKey(x => x.InterfaceId);

            builder.Ignore(x => x.History);
            builder.HasIndex(x => x.Deleted);
        }
    }

    public class CTInterfaceAssignment : IDeletable
    {
        public Guid SourceId { get; set; }
        public CTInterface? Source { get; set; }
        public Guid DestinationId { get; set; }
        public CTInterface? Destination { get; set; }
        public bool Deleted { get; set; }
    }

    internal class CTInterfaceAssignmentConfig : IEntityTypeConfiguration<CTInterfaceAssignment>
    {
        public void Configure(EntityTypeBuilder<CTInterfaceAssignment> builder)
        {
            builder.ToTable("interface_assignments");
            
            builder.HasOne(x => x.Destination)
                .WithMany(x => x.Usings)
                .HasForeignKey(x => x.DestinationId);

            builder.HasOne(x => x.Source)
                .WithMany(x => x.Includings)
                .HasForeignKey(x => x.SourceId);

            builder.HasKey(x => new { x.SourceId, x.DestinationId });
            builder.HasIndex(x => x.Deleted);
        }
    }

    public class CTInterfaceProperty : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool Deleted { get; set; }
        public Guid InterfaceId { get; set; }
        public CTInterface? Interface { get; set; }
        public PropertyType Type { get; set; } = PropertyType.String;
        public bool Required { get; set; } = true;
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
