namespace HAB.Auditing.Entities;

public class ChangeSet
{

    #region Properties
    
    public int Id { get; set; }
    public DateTime ChangeTime { get; init; }
    public string? BrowserInfo { get; init; } = "";
    public string? ClientIp { get; init; } = "";
    public string? ChangedBy { get; init; }
    public string UserId { get; init; }
    public string Reason { get; init; }

    private readonly List<EntityChange> _entityChanges = new();
    public IReadOnlyList<EntityChange> EntityChanges => _entityChanges.AsReadOnly();
    
    public void AddEntityChange(EntityChange entityChange)
    {
        _entityChanges.Add(entityChange);
    }

    #endregion
    
    #region Methods

    public override string ToString()
    {
        var head = $"Change set created at {ChangeTime} \r\nby {ChangedBy} \r\nwith user id {UserId} \r\nwith reason: {Reason}";
        var body = string.Join("\r\n", EntityChanges.Select(x => x.ToString()));
        return $"{head}\r\n{body}";
    }

    #endregion
}