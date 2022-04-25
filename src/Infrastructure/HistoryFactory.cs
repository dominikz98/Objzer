using Core.Models;
using Core.Models.Contracts;
using Core.Models.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;

namespace Infrastructure.Core
{
    public static class HistoryFactory
    {
        public static CTHistory? Create(EntityEntry entry)
        {
            var id = ExtractEntityId(entry);
            if (id is null)
                return null;

            HistoryAction action;
            string? changes;
            switch (entry.State)
            {
                case EntityState.Deleted:
                    changes = null;
                    action = HistoryAction.Delete;
                    break;
                case EntityState.Modified:
                    changes = CreateChanges(entry, HistoryAction.Update);
                    action = HistoryAction.Update;
                    break;
                case EntityState.Added:
                    changes = CreateChanges(entry, HistoryAction.Add);
                    action = HistoryAction.Add;
                    break;

                case EntityState.Detached:
                case EntityState.Unchanged:
                default:
                    return null;
            }

            return new CTHistory()
            {
                Id = Guid.NewGuid(),
                Type = entry.Entity.GetType().Name,
                EntityId = id.Value,
                Timestamp = DateTime.UtcNow,
                Action = action,
                Changes = changes
            };
        }

        private static Guid? ExtractEntityId(EntityEntry entry)
        {
            if (entry.Entity is IEntity entity)
                return entity.Id;
            else if (entry.Entity is ISubEntity subEntity)
                return subEntity.ReferenceId;

            return null;
        }

        private static string CreateChanges(EntityEntry entry, HistoryAction type)
        {
            var changes = new List<Change>();
            foreach (var property in entry.Properties)
            {
                if (type == HistoryAction.Update && !property.IsModified)
                    continue;

                if (property.Metadata?.PropertyInfo?.PropertyType is null)
                    continue;

                if (!IsSimpleType(property.Metadata.PropertyInfo.PropertyType))
                    continue;

                if (type == HistoryAction.Add && property.CurrentValue is null)
                    continue;

                changes.Add(new Change()
                {
                    Name = property.Metadata.Name,
                    New = property.CurrentValue,
                    Old = type == HistoryAction.Update ? property.CurrentValue : null
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
