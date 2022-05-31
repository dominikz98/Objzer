using Core.Models;
using Core.Models.Contracts;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;

namespace Infrastructure.Core
{
    public class ObjzerContext : DbContext
    {
        private readonly HistoryFactory _historyFactory;

        public string DbPath { get; }

        public ObjzerContext(HistoryFactory historyFactory)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "objzer.db");

            _historyFactory = historyFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder builder)
        {
            ApplyCTEntityQueryFilter(builder);

            builder.ApplyCTObject();
            builder.ApplyCTInterface();
            builder.ApplyCTHistory();
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var toAdd = new List<CTHistory>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is not IEntity)
                    continue;

                var casted = Entry((IEntity)entry.Entity);
                var history = _historyFactory.Create(casted);
                if (history is null)
                    continue;

                toAdd.Add(history);
            }

            await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

            await AddRangeAsync(toAdd, cancellationToken);
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override EntityEntry Remove(object entity)
        {
            if (entity is not IEntity casted)
                return base.Remove(entity);

            casted.Deleted = true;
            _historyFactory.Delete(casted);

            return Update(entity);
        }

        public override EntityEntry<TEntity> Remove<TEntity>(TEntity entity)
        {
            if (entity is not IEntity casted)
                return base.Remove(entity);

            casted.Deleted = true;
            _historyFactory.Delete(casted);

            return Update(entity);
        }

        public override void RemoveRange(IEnumerable<object> entities)
        {
            foreach (var entity in entities)
                Remove(entity);
        }

        public override void RemoveRange(params object[] entities)
        {
            foreach (var entity in entities)
                Remove(entity);
        }

        public async ValueTask<EntityEntry<TEntity>> LockAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class, IEntity
        {
            entity.Locked = true;

            var entry = Entry(entity);
            if (entry.State != EntityState.Modified)
                return entry;

            var history = _historyFactory.Lock(entity);
            await AddAsync(history, cancellationToken);
            return Update(entity);
        }

        public async ValueTask<EntityEntry<TEntity>> UnlockAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class, IEntity
        {
            entity.Locked = false;

            var entry = Entry(entity);
            if (entry.State != EntityState.Modified)
                return entry;

            var history = _historyFactory.Unlock(entity);
            await AddAsync(history, cancellationToken);
            return Update(entity);
        }

        public async ValueTask<EntityEntry<TEntity>> ArchiveAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class, IEntity
        {
            entity.Locked = true;
            entity.Archived = DateOnly.FromDateTime(DateTime.Now);

            var entry = Entry(entity);
            if (entry.State != EntityState.Modified)
                return entry;

            var history = _historyFactory.Archive(entity);
            await AddAsync(history, cancellationToken);
            return Update(entity);
        }

        public async ValueTask<EntityEntry<TEntity>> RestoreAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class, IEntity
        {
            entity.Locked = false;
            entity.Archived = null;

            var entry = Entry(entity);
            if (entry.State != EntityState.Modified)
                return entry;

            var history = _historyFactory.Restore(entity);
            await AddAsync(history, cancellationToken);
            return Update(entity);
        }

        internal void ApplyCTEntityQueryFilter(ModelBuilder builder)
        {
            var setter = GetType().GetMethod(nameof(SetQueryFilter), BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            if (setter == null)
                throw new MissingMethodException(nameof(ObjzerContext), nameof(SetQueryFilter));

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
    }
}
