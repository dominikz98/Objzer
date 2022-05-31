using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal static class CTObjectConfigExtensions
{
    public static void ApplyCTObject(this ModelBuilder builder)
    {
        builder.ApplyConfiguration(new CTObjectConfig());
        builder.ApplyConfiguration(new CTObjectPropertyConfig());
    }
}

internal class CTObjectConfig : IEntityTypeConfiguration<CTObject>
{
    public void Configure(EntityTypeBuilder<CTObject> builder)
    {
        builder.ToTable("objects");

        builder.HasMany(x => x.Properties)
            .WithOne(x => x.Object)
            .HasForeignKey(x => x.ObjectId);

        builder.Ignore(x => x.History);
        builder.HasIndex(x => x.Deleted);
    }
}

internal class CTObjectPropertyConfig : IEntityTypeConfiguration<CTObjectProperty>
{
    public void Configure(EntityTypeBuilder<CTObjectProperty> builder)
    {
        builder.ToTable("object_properties");
        builder.HasKey(x => new { x.ObjectId, x.Name });
        builder.HasIndex(x => x.Deleted);
    }
}
