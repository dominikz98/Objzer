using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Core
{
    public class ObjzerContext : DbContext
    {
        public DbSet<CTObject> Objects { get; set; }
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
            builder.Entity<CTObject>()
                .ToTable("objects")
                .HasMany(x => x.Interfaces)
                .WithMany(x => x.Objects);

            builder.Entity<CTContract>()
                .ToTable("contracts");

            builder.Entity<CTAbstraction>()
                .ToTable("abstractions");

            builder.Entity<CTInterface>()
                .ToTable("interfaces");

            builder.Entity<CTEnumeration>().ToTable("enumerations");
            builder.Entity<CTEnumeration>()
                .Property(x => x.Values)
                .HasConversion(x => string.Join(';', x), x => x.Split(';', StringSplitOptions.None).ToList());

        }
    }
}
