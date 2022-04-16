using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;

namespace api.Core
{
    public static class HistoryFactory
    {
        public static CTHistory? Create(EntityEntry entry)
        {
            if (entry.Entity.GetType().GetInterface(nameof(IEntity)) is null)
                return null;

            HistoryType type;
            string? changes;

            switch (entry.State)
            {
                case EntityState.Deleted:
                    changes = null;
                    type = HistoryType.Delete;
                    break;
                case EntityState.Modified:
                    changes = CreateChanges(entry, HistoryType.Update);
                    type = HistoryType.Update;
                    break;
                case EntityState.Added:
                    changes = CreateChanges(entry, HistoryType.Add);
                    type = HistoryType.Add;
                    break;

                case EntityState.Detached:
                case EntityState.Unchanged:
                default:
                    return null;
            }

            return new CTHistory()
            {
                Id = Guid.NewGuid(),
                EntityId = ((IEntity)entry.Entity).Id,
                Timestamp = DateTime.UtcNow,
                Type = type,
                Changes = changes
            };
        }

        private static string CreateChanges(EntityEntry entry, HistoryType type)
        {
            var changes = new List<Change>();
            foreach (var property in entry.Properties)
            {
                if (type == HistoryType.Update && !property.IsModified)
                    continue;

                if (property.Metadata?.PropertyInfo?.PropertyType is null)
                    continue;

                if (!IsSimpleType(property.Metadata.PropertyInfo.PropertyType))
                    continue;

                if (type == HistoryType.Add && property.CurrentValue is null)
                    continue;

                changes.Add(new Change()
                {
                    Name = property.Metadata.Name,
                    New = property.CurrentValue,
                    Old = type == HistoryType.Update ? property.CurrentValue : null
                });
            }

            return JsonSerializer.Serialize(changes, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }

        private static bool IsSimpleType(Type type)
            => type.IsPrimitive
                || new Type[] {
                    typeof(string),
                    typeof(decimal),
                    typeof(TimeOnly),
                    typeof(DateOnly),
                    typeof(DateTime),
                    typeof(DateTimeOffset),
                    typeof(TimeSpan),
                    typeof(Guid)
                }.Contains(type)
                || type.IsEnum
                || Convert.GetTypeCode(type) != TypeCode.Object
                || (type.IsGenericType
                    && type.GetGenericTypeDefinition() == typeof(Nullable<>)
                    && IsSimpleType(type.GetGenericArguments()[0]));

        class Change
        {
            public string Name { get; set; } = string.Empty;
            public object? New { get; set; }
            public object? Old { get; set; }
        }
    }
}
