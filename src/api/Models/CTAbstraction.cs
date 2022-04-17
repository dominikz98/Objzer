//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace api.Models
//{
//    public class CTAbstraction : IEntity
//    {
//        public Guid Id { get; set; }
//        public string Name { get; set; } = string.Empty;
//        public string Description { get; set; } = string.Empty;
//        public bool Deleted { get; set; }
//        public IList<CTObject> Objects { get; set; } = new List<CTObject>();
//        public IList<CTAbstractionAssignment> Inheritances { get; set; } = new List<CTAbstractionAssignment>();
//        public IList<CTAbstractionProperty> Properties { get; set; } = new List<CTAbstractionProperty>();
//    }

//    internal class CTAbstractionConfig : IEntityTypeConfiguration<CTAbstraction>
//    {
//        public void Configure(EntityTypeBuilder<CTAbstraction> builder)
//        {
//            builder.ToTable("abstractions")
//                .HasMany(x => x.Properties)
//                .WithOne(x => x.Abstraction)
//                .HasForeignKey(x => x.AbstractionId);

//            builder.HasMany(x => x.Inheritances)
//                .WithOne(x => x.Parent)
//                .HasForeignKey(x => x.ParentId);

//            builder.HasIndex(x => x.Deleted);
//        }
//    }

//    public class CTAbstractionAssignment : IDeletable
//    {
//        public Guid ParentId { get; set; }
//        public CTAbstraction? Parent { get; set; }
//        public IList<CTAbstraction> Children { get; set; } = new List<CTAbstraction>();
//        public bool Deleted { get; set; }
//    }

//    internal class CTAbstractionAssignmentConfig : IEntityTypeConfiguration<CTAbstractionAssignment>
//    {
//        public void Configure(EntityTypeBuilder<CTAbstractionAssignment> builder)
//        {
//            builder.ToTable("abstraction_assignments")
//                .HasKey(x => x.ParentId);

//            builder.HasIndex(x => x.Deleted);
//        }
//    }

//    public class CTAbstractionProperty : IEntity
//    {
//        public Guid Id { get; set; }
//        public string Name { get; set; } = string.Empty;
//        public string Description { get; set; } = string.Empty;
//        public bool Deleted { get; set; }
//        public Guid AbstractionId { get; set; }
//        public CTAbstraction? Abstraction { get; set; }
//        public PropertyType Type { get; set; } = PropertyType.String;
//        public bool Required { get; set; } = true;
//    }

//    internal class CTAbstractionPropertyConfig : IEntityTypeConfiguration<CTAbstractionProperty>
//    {
//        public void Configure(EntityTypeBuilder<CTAbstractionProperty> builder)
//        {
//            builder.ToTable("abstraction_properties");
//            builder.HasIndex(x => x.AbstractionId);
//            builder.HasIndex(x => x.Deleted);
//        }
//    }

//    internal static class CTAbstractionExtensions
//    {
//        internal static void ApplyCTAbstraction(this ModelBuilder builder)
//        {
//            builder.ApplyConfiguration(new CTAbstractionConfig());
//            builder.ApplyConfiguration(new CTAbstractionAssignmentConfig());
//            builder.ApplyConfiguration(new CTAbstractionPropertyConfig());
//        }
//    }
//}
