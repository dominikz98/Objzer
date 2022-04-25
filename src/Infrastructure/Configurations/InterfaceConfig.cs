using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    internal static class CTInterfaceConfigExtensions
    {
        public static void ApplyCTInterface(this ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CTInterfaceConfig());
            builder.ApplyConfiguration(new CTInterfaceAssignmentConfig());
            builder.ApplyConfiguration(new CTInterfacePropertyConfig());
        }
    }

    internal class CTInterfaceConfig : IEntityTypeConfiguration<CTInterface>
    {
        public void Configure(EntityTypeBuilder<CTInterface> builder)
        {
            builder.ToTable("interfaces")
                .HasMany(x => x.Properties)
                .WithOne(x => x.Interface)
                .HasForeignKey(x => x.ReferenceId);

            builder.Ignore(x => x.History);
            builder.HasIndex(x => x.Deleted);
        }
    }

    internal class CTInterfaceAssignmentConfig : IEntityTypeConfiguration<CTInterfaceAssignment>
    {
        public void Configure(EntityTypeBuilder<CTInterfaceAssignment> builder)
        {
            builder.ToTable("interface_assignments");
            builder.HasKey(x => new { x.ReferenceId, x.DestinationId });
            builder.HasIndex(x => x.Deleted);

            builder.HasOne(x => x.Destination)
                .WithMany(x => x.Usings)
                .HasForeignKey(x => x.DestinationId);

            builder.HasOne(x => x.Source)
                .WithMany(x => x.Includings)
                .HasForeignKey(x => x.ReferenceId);
        }
    }

    public class CTInterfacePropertyConfig : IEntityTypeConfiguration<CTInterfaceProperty>
    {
        public void Configure(EntityTypeBuilder<CTInterfaceProperty> builder)
        {
            builder.ToTable("interface_properties");
            builder.HasKey(x => new { x.ReferenceId, x.Name });
            builder.HasIndex(x => x.Deleted);
        }
    }
}
