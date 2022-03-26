using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Core
{
    public class ObjzerContext : DbContext
    {
        public DbSet<CTEntity> Entities { get; set; }
        public DbSet<CTHistory> History { get; set; }
        public DbSet<CTObject> Objects { get; set; }
        public DbSet<CTProperty> Properties { get; set; }
        public DbSet<CTContract> Contracts { get; set; }
        public DbSet<CTInterface> Interfaces { get; set; }
        public DbSet<CTAbstraction> Abstractions { get; set; }
        public DbSet<CTEnumeration> Enumerations { get; set; }

        public string DbPath { get; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ObjzerContext()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "objzer.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CTHistory>()
                .ToTable("history");

            builder.Entity<CTEntity>()
                .ToTable("entities");

            builder.Entity<CTEntity>()
                .HasMany(x => x.History)
                .WithOne(x => x.Entity);

            builder.Entity<CTObject>()
                .ToTable("objects")
                .HasMany(x => x.Contracts)
                .WithMany(x => x.Objects);

            builder.Entity<CTObject>()
                .HasMany(x => x.Properties)
                .WithOne(x => x.Object);

            builder.Entity<CTProperty>()
                .ToTable("properties");

            builder.Entity<CTContract>()
                .ToTable("contracts");

            builder.Entity<CTAbstractionAssignment>()
                .ToTable("abstractions_assignments")
                .HasKey(x => x.ParentId);
            builder.Entity<CTAbstractionAssignment>()
                .HasOne(x => x.Parent)
                .WithMany(x => x.Inheritances)
                .HasForeignKey(x => x.ParentId);

            builder.Entity<CTAbstraction>()
                .ToTable("abstractions");

            builder.Entity<CTInterfaceAssignment>()
                .ToTable("interfaces_assignments")
                .HasKey(x => x.ParentId);
            builder.Entity<CTInterfaceAssignment>()
                .HasOne(x => x.Parent)
                .WithMany(x => x.Implementations)
                .HasForeignKey(x => x.ParentId);

            builder.Entity<CTInterface>()
                .ToTable("interfaces");

            builder.Entity<CTEnumeration>().ToTable("enumerations");
            builder.Entity<CTEnumeration>()
                .Property(x => x.Values)
                .HasConversion(x => string.Join(';', x), x => x.Split(';', StringSplitOptions.None).ToList());
        }
    }
}
