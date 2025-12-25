namespace HAB.Auditing.Entities;

public class EntityChange
{
    #region Properties

    public required string EntityName { get; init; } = "";
    public required string EntityId { get; init; } = "";

    private readonly List<PropertyChange> _propertyChanges;
    public IReadOnlyList<PropertyChange> PropertyChanges => _propertyChanges.AsReadOnly();

    #endregion

    #region Methods

    public EntityChange()
    {
        _propertyChanges = new List<PropertyChange>();
    }

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
