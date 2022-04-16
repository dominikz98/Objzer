using api.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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
            ApplyCTEntityQueryFilter(builder);
            builder.ApplyCTAbstraction();
            builder.ApplyCTInterface();
            builder.ApplyCTObject();
            builder.ApplyCTHistory();
            builder.ApplyCTEnumeration();
        }

        internal void ApplyCTEntityQueryFilter(ModelBuilder builder)
        {
            var setter = GetType().GetMethod(nameof(SetQueryFilter), BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            if (setter == null)
                throw new MissingMethodException(nameof(DBContext), nameof(SetQueryFilter));

            var entities = typeof(IDeletable)
                .Assembly
                .GetTypes()
                .Where(x => x.GetInterface(nameof(IDeletable)) != null)
                .Where(x => !x.IsAbstract);

            foreach (var entity in entities)
            {
                var generic = setter.MakeGenericMethod(entity);
                generic.Invoke(null, new object[] { builder });
            }
        }

        private static void SetQueryFilter<T>(ModelBuilder modelBuilder) where T : class, IDeletable
            => modelBuilder.Entity<T>().HasQueryFilter(x => !x.Deleted);

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var toAdd = new List<CTHistory>();
            foreach (var entry in ChangeTracker.Entries())
            {
                var entity = HistoryFactory.Create(entry);
                if (entity is null)
                    continue;

                toAdd.Add(entity);
            }

            await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

            await AddRangeAsync(toAdd, cancellationToken);
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
