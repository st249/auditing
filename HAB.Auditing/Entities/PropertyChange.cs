namespace HAB.Auditing.Entities;
public class PropertyChange
{
    public required string PropertyName { get; set; }
    public required string OldValue { get; set; }
    public required string NewValue { get; set; }

    public PropertyChange()
    {
        
    }
    
    public override string ToString()
    {
        return $"\t{PropertyName}: {OldValue} -> {NewValue}";
    }
}