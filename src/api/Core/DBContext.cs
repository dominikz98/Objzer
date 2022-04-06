using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Core
{
    public class DBContext : DbContext
    {
        public string DbPath { get; }

        public DBContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "objzer.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyCTAbstraction();
            builder.ApplyCTInterface();
            builder.ApplyCTObject();
            builder.ApplyCTHistory();
            builder.ApplyCTEnumeration();
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                var entity = HistoryFactory.Create(entry);
                if (entity is null)
                    continue;

                await AddAsync(entity, cancellationToken);
            }

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
