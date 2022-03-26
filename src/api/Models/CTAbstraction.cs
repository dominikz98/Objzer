using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Models
{
    public class CTAbstraction : CTContract
    {
        public IList<CTAbstractionAssignment> Inheritances { get; set; } = new List<CTAbstractionAssignment>();
        public IList<CTAbstractionProperty> Properties { get; set; } = new List<CTAbstractionProperty>();
    }

    internal class CTAbstractionConfig : IEntityTypeConfiguration<CTAbstraction>
    {
        public void Configure(EntityTypeBuilder<CTAbstraction> builder)
            => builder.ToTable("abstractions")
                .HasMany(x => x.Properties)
                .WithOne(x => x.Abstraction)
                .HasForeignKey(x => x.AbstractionId);
    }

    public class CTAbstractionAssignment
    {
        public Guid ParentId { get; set; }
        public CTAbstraction? Parent { get; set; }
        public IList<CTAbstraction> Children { get; set; } = new List<CTAbstraction>();
    }

    internal class CTAbstractionAssignmentConfig : IEntityTypeConfiguration<CTAbstractionAssignment>
    {
        public void Configure(EntityTypeBuilder<CTAbstractionAssignment> builder)
        {
            builder.ToTable("abstraction_assignments")
                .HasKey(x => x.ParentId);

            builder.HasOne(x => x.Parent)
                .WithMany(x => x.Inheritances)
                .HasForeignKey(x => x.ParentId);
        }
    }

    public class CTAbstractionProperty : CTEntity
    {
        public Guid AbstractionId { get; set; }
        public CTAbstraction? Abstraction { get; set; }
        public PropertyTypes Type { get; set; } = PropertyTypes.String;
        public bool Required { get; set; } = true;
    }

    internal class CTAbstractionPropertyConfig : IEntityTypeConfiguration<CTAbstractionProperty>
    {
        public void Configure(EntityTypeBuilder<CTAbstractionProperty> builder)
            => builder.ToTable("abstraction_properties");
    }

    internal static class CTAbstractionExtensions
    {
        internal static void ApplyCTAbstraction(this ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CTAbstractionConfig());
            builder.ApplyConfiguration(new CTAbstractionAssignmentConfig());
            builder.ApplyConfiguration(new CTAbstractionPropertyConfig());
        }
    }
}
