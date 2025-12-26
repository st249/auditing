namespace HAB.Auditing.Entities;

public class EntityChange
{
    #region Properties
    public long Id { get; private set; }
    public string EntityName { get; init; } = "";
    public string EntityId { get; init; } = "";
    public ChangeType ChangeType { get; init; } = ChangeType.UnChanged;

    private readonly List<PropertyChange> _propertyChanges = new();
    public IReadOnlyList<PropertyChange> PropertyChanges => _propertyChanges.AsReadOnly();

    #endregion

    #region Methods

    public EntityChange AddPropertyChange(PropertyChange propertyChange)
    {
        _propertyChanges.Add(propertyChange);
        return this;
    }

    public override string ToString()
    {
        var head = $"Entity '{EntityName}' with id '{EntityId}' changed";
        return $"{head}:\r\n{string.Join("\r\n", PropertyChanges.Select(x => x.ToString()))}";
    }

    #endregion
}
