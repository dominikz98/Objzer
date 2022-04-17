using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Core
{
    public interface IHistoryLoader
    {
        Task<List<CTHistory>> Load<T>(T entity, CancellationToken cancellationToken) where T : IEntity;
        Task<List<CTHistory>> Load<T>(T entity, bool lazyLoadd, CancellationToken cancellationToken) where T : IEntity;

        IQueryable<CTHistory> Query<T>(T entity, bool lazyLoad = false) where T : IEntity;
        IQueryable<CTHistory> QueryAll<T>(List<T> entities, bool lazyLoad = false) where T : IEntity;
    }

    public class HistoryLoader : IHistoryLoader
    {
        private readonly DBContext _context;

        public HistoryLoader(DBContext context)
        {
            _context = context;
        }

        public async Task<List<CTHistory>> Load<T>(T entity, CancellationToken cancellationToken) where T : IEntity
            => await Load(entity, false, cancellationToken);

        public async Task<List<CTHistory>> Load<T>(T entity, bool lazyLoad, CancellationToken cancellationToken) where T : IEntity
            => await Query(entity, lazyLoad)
                 .AsNoTracking()
                 .ToListAsync(cancellationToken);


        public IQueryable<CTHistory> Query<T>(T entity, bool lazyLoad = false) where T : IEntity
        {
            var historyIds = GetEntityIds(entity, lazyLoad);
            return _context.Set<CTHistory>()
                .Where(x => historyIds.Contains(x.EntityId));
        }

        public IQueryable<CTHistory> QueryAll<T>(List<T> entities, bool lazyLoad = false) where T : IEntity
        {
            var historyIds = new List<Guid>();
            foreach (var entity in entities)
            {
                var ids = GetEntityIds(entity, lazyLoad);
                historyIds.AddRange(ids);
            }

            return _context.Set<CTHistory>()
                .Where(x => historyIds.Contains(x.EntityId));
        }


        private IReadOnlyCollection<Guid> GetEntityIds<T>(T entity, bool lazyLoad) where T : IEntity
        {
            var historyIds = new List<Guid>()
            {
                entity.Id
            };

            if (lazyLoad)
                return historyIds;

            // get ids from IEntity properties
            var properties = typeof(T)
                .GetProperties()
                .Where(x => x.PropertyType.GetInterface(nameof(IEntity)) is not null)
                .Where(x => !x.PropertyType.IsGenericType)
                .ToList();

            foreach (var property in properties)
            {
                var instance = property.GetValue(entity);
                if (instance is null || instance is not IEntity subentity)
                    continue;

                historyIds.Add(subentity.Id);
            }

            // get ids from properties with list of IEntity as type
            var listTypes = new List<Type>()
            {
                 typeof(IEnumerable<>),
                 typeof(IList<>),
                 typeof(List<>),
            };
            var lists = typeof(T)
                .GetProperties()
                .Where(x => x.PropertyType.IsGenericType)
                .Where(x => listTypes.Contains(x.PropertyType.GetGenericTypeDefinition()))
                .Where(x => x.PropertyType.GenericTypeArguments.Single().IsAssignableTo(typeof(IEntity)))
                .ToList();

            foreach (var list in lists)
            {
                var instance = list.GetValue(entity);
                if (instance is null || instance is not IEnumerable<IEntity> subentities)
                    continue;

                var ids = subentities.Select(x => x.Id);
                historyIds.AddRange(ids);
            }

            return historyIds;
        }
    }
}
