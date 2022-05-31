using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal static class CTInterfaceConfigExtensions
{
    public static void ApplyCTInterface(this ModelBuilder builder)
    {
        builder.ApplyConfiguration(new CTInterfaceConfig());
        builder.ApplyConfiguration(new CTInterfacePropertyConfig());
    }
}

internal class CTInterfaceConfig : IEntityTypeConfiguration<CTInterface>
{
    public void Configure(EntityTypeBuilder<CTInterface> builder)
    {
        builder.ToTable("interfaces");

        builder.HasMany(x => x.Properties)
            .WithOne(x => x.Interface)
            .HasForeignKey(x => x.InterfaceId);

        builder.Ignore(x => x.History);
        builder.HasIndex(x => x.Deleted);
    }
}

internal class CTInterfacePropertyConfig : IEntityTypeConfiguration<CTInterfaceProperty>
{
    public void Configure(EntityTypeBuilder<CTInterfaceProperty> builder)
    {
        builder.ToTable("interface_properties");
        builder.HasKey(x => new { x.InterfaceId, x.Name });
        builder.HasIndex(x => x.Deleted);
    }
}
