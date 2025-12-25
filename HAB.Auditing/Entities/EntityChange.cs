namespace HAB.Auditing.Entities;
public class EntityChange
{
    public required string EntityName { get; set; }
    public required string EntityId { get; set; }
    
    private List<PropertyChange> _propertyChanges;
    public IReadOnlyList<PropertyChange> PropertyChanges => _propertyChanges.AsReadOnly();

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
}