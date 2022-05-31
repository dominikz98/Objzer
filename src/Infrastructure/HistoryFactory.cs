using Core.Constants;
using Core.DTOs;
using Core.Models;
using Core.Models.Contracts;
using Core.Models.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;
using System.Text.Json;

namespace Infrastructure;

public class HistoryFactory
{
    private readonly List<Guid> _excludeIds = new();

    public CTHistory? Create<T>(EntityEntry<T> entry) where T : class, IEntity
    {
        if (_excludeIds.Contains(entry.Entity.Id))
            return null;

        return entry.State switch
        {
            EntityState.Added => Add(entry),
            EntityState.Modified => Update(entry),
            EntityState.Detached or EntityState.Unchanged or _ => null
        };
    }

    public static CTHistory Add<T>(EntityEntry<T> entry) where T : class, IEntity
    {
        var history = CreateHistory(entry.Entity, HistoryAction.Add);
        history.Changes = CreateAddChanges(entry);
        return history;
    }

    private static string CreateAddChanges(EntityEntry entry)
    {
        var changes = new List<HistoryChange>();
        foreach (var property in entry.Properties)
        {
            if (property.Metadata?.PropertyInfo?.PropertyType is null)
                continue;

            var type = property.Metadata.PropertyInfo.PropertyType;
            if (!IsSimpleType(type))
                continue;

            if (property.CurrentValue?.ToString() == GetDefault(type)?.ToString())
                continue;

            ThrowErrorIfRequired(property);

            changes.Add(new HistoryChange()
            {
                Name = property.Metadata.Name,
                New = property.CurrentValue
            });
        }

        return JsonSerializer.Serialize(changes, JsonConstants.Options);
    }

    public static CTHistory? Update<T>(EntityEntry<T> entry) where T : class, IEntity
    {
        var history = CreateHistory(entry.Entity, HistoryAction.Update);
        if (history is null)
            return null;

        history.Changes = CreateUpdateChanges(entry);
        return history;
    }

    private static string CreateUpdateChanges(EntityEntry entry)
    {
        var changes = new List<HistoryChange>();
        foreach (var property in entry.Properties)
        {
            if (!property.IsModified)
                continue;

            if (property.CurrentValue?.ToString() == property.OriginalValue?.ToString())
                continue;

            if (property.Metadata?.PropertyInfo?.PropertyType is null)
                continue;

            if (!IsSimpleType(property.Metadata.PropertyInfo.PropertyType))
                continue;

            ThrowErrorIfRequired(property);

            changes.Add(new HistoryChange()
            {
                Name = property.Metadata.Name,
                New = property.CurrentValue,
                Old = property.OriginalValue
            });
        }

        return JsonSerializer.Serialize(changes, JsonConstants.Options);
    }

    public CTHistory Delete<T>(T entity) where T : class, IEntity
    {
        var history = CreateHistory(entity, HistoryAction.Delete);
        _excludeIds.Add(history.EntityId);
        return history;
    }

    public CTHistory Lock<T>(T entry) where T : class, IEntity
    {
        var history = CreateHistory(entry, HistoryAction.Lock);
        _excludeIds.Add(history.EntityId);
        return history;
    }

    public CTHistory Unlock<T>(T entry) where T : class, IEntity
    {
        var history = CreateHistory(entry, HistoryAction.Unlock);
        _excludeIds.Add(history.EntityId);
        return history;
    }

    public CTHistory Archive<T>(T entry) where T : class, IEntity
    {
        var history = CreateHistory(entry, HistoryAction.Archive);
        _excludeIds.Add(history.EntityId);
        return history;
    }

    public CTHistory Restore<T>(T entry) where T : class, IEntity
    {
        var history = CreateHistory(entry, HistoryAction.Restore);
        _excludeIds.Add(history.EntityId);
        return history;
    }

    private static CTHistory CreateHistory<T>(T entity, HistoryAction action) where T : class, IEntity
        => new()
        {
            Id = Guid.NewGuid(),
            Type = entity.GetType().Name,
            EntityId = entity.Id,
            Timestamp = DateTime.UtcNow,
            Action = action,
        };

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
            || type.IsGenericType
                && type.GetGenericTypeDefinition() == typeof(Nullable<>)
                && IsSimpleType(type.GetGenericArguments()[0]);

    private static void ThrowErrorIfRequired(PropertyEntry property)
    {
        if (property.Metadata.PropertyInfo?.Name == nameof(IEntity.Locked))
            throw new ArgumentException($"Property '{nameof(IEntity.Locked)}' can not be changed! Use {nameof(ObjzerContext.LockAsync)} or {nameof(ObjzerContext.UnlockAsync)} instead!");

        if (property.Metadata.PropertyInfo?.Name == nameof(IEntity.Archived))
            throw new ArgumentException($"Property '{nameof(IEntity.Archived)}' can not be changed! Use {nameof(ObjzerContext.ArchiveAsync)} or {nameof(ObjzerContext.RestoreAsync)} instead!");
    }

    private static object? GetDefault(Type t)
        => typeof(HistoryFactory)
            .GetMethod(nameof(GetDefaultGeneric), BindingFlags.Static | BindingFlags.NonPublic)
            ?.MakeGenericMethod(t)
            ?.Invoke(null, null);

    private static T? GetDefaultGeneric<T>()
        => default;
}
