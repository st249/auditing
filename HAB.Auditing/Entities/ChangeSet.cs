namespace HAB.Auditing.Entities;

public class ChangeSet
{

    #region Properties

    public required DateTime ChangeTime { get; init; }
    public string? BrowserInfo { get; init; } = "";
    public string ClientIp { get; init; } = "";
    public string? ChangedBy { get; init; }
    public required int UserId { get; init; }
    public required string Reason { get; init; }

    private readonly List<EntityChange> _entityChanges = new();
    public IReadOnlyList<EntityChange> EntityChanges => _entityChanges.AsReadOnly();
    
    public void AddEntityChange(EntityChange entityChange)
    {
        _entityChanges.Add(entityChange);
    }

    #endregion
    
    #region Methodsß

    public override string ToString()
    {
        var head = $"Change set created at {ChangeTime} \r\nby {ChangedBy} \r\nwith user id {UserId} \r\nwith reason: {Reason}";
        var body = string.Join("\r\n", EntityChanges.Select(x => x.ToString()));
        return $"{head}\r\n{body}";
    }

    #endregion
}