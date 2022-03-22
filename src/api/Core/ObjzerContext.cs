using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Core
{
    public class ObjzerContext : DbContext
    {
        public DbSet<CatalogueObject> Objects { get; set; }
        public DbSet<CatalogueInterface> Interfaces { get; set; }

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
            builder.Entity<CatalogueObject>()
                .ToTable("objects")
                .HasMany(x => x.Interfaces)
                .WithMany(x => x.Objects);

            builder.Entity<CatalogueInterface>()
                .ToTable("interfaces");
        }
    }
}
