using HAB.Auditing.Abstractions;
using HAB.Auditing.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace HAB.Auditing.EntityFramework;

public class AuditsSaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly IAuditInfoProvider infoProvider;

    public AuditsSaveChangesInterceptor(IAuditInfoProvider infoProvider)
    {
        this.infoProvider = infoProvider;
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        if (!infoProvider.ShouldAudit)
            return await base.SavingChangesAsync(eventData, result, cancellationToken);

        var context = eventData.Context;
        if (context == null) return await base.SavingChangesAsync(eventData, result, cancellationToken);

        var entries = context.ChangeTracker.Entries()
            .Where(e => (e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted))
            .ToList();

        if (!entries.Any()) return await base.SavingChangesAsync(eventData, result, cancellationToken);

        var changeSet = new ChangeSet
        {
            ChangeTime = DateTime.UtcNow,
            ChangedBy = infoProvider.GetChangedBy(),
            UserId = infoProvider.GetUserId(),
            Reason = infoProvider.GetReason(),
            BrowserInfo = infoProvider.GetBrowserInfo(),
            ClientIp = infoProvider.GetClientIp()
        };

        foreach (var entry in entries)
        {
            var changeType = MapState(entry.State);
            var ec = new EntityChange
            {
                EntityName = entry.Entity.GetType().Name,
                ChangeType = changeType,
                EntityId = changeType != ChangeType.Added ? FindPrimaryKeyValue(entry) : ""
            };
            var propertiesChanges = GetChangeProperties(entry);
            foreach (var propChange in propertiesChanges)
            {
                var propertyChange = new PropertyChange
                {
                    PropertyName = propChange.Key,
                    PropertyType = propChange.Value.propType,
                    OldValue = propChange.Value.oldValue,
                    NewValue = propChange.Value.newValue
                };

                ec.AddPropertyChange(propertyChange);
            }

            changeSet.AddEntityChange(ec);
        }

        context.Add(changeSet);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static ChangeType MapState(EntityState state) =>
        state switch
        {
            EntityState.Added => ChangeType.Added,
            EntityState.Modified => ChangeType.Modified,
            EntityState.Deleted => ChangeType.Deleted,
            _ => ChangeType.UnChanged
        };

    private Dictionary<string, (string propType, string oldValue, string newValue)> GetChangeProperties(EntityEntry entry)
    {
        var result = new Dictionary<string, (string propType, string oldValue, string newValue)>();
        foreach (var prop in entry.Properties)
        {
            if (entry.State == EntityState.Added)
                result[prop.Metadata.Name] = new(prop.Metadata.ClrType.ToString(), "", prop.CurrentValue?.ToString() ?? "");
            else if (entry.State == EntityState.Deleted)
                result[prop.Metadata.Name] = new(prop.Metadata.ClrType.ToString(), prop.OriginalValue?.ToString() ?? "", "");
            else if (prop.IsModified)
                result[prop.Metadata.Name] = new(prop.Metadata.ClrType.ToString(), prop.OriginalValue?.ToString() ?? "",
                    prop.CurrentValue?.ToString() ?? "");
        }

        return result;
    }

    private static string FindPrimaryKeyValue(EntityEntry entry)
    {
        var pk = entry.Metadata.FindPrimaryKey();
        string entityId;
        if (pk == null)
        {
            entityId = string.Empty;
        }
        else
        {
            var parts = pk.Properties.Select(p =>
            {
                var propEntry = entry.Property(p.Name);
                var value = entry.State == EntityState.Deleted ? propEntry.OriginalValue : propEntry.CurrentValue;
                return value?.ToString() ?? string.Empty;
            }).ToArray();

            entityId = parts.Length == 1 ? parts[0] : string.Join(";", parts);
        }

        return entityId;
    }
}
