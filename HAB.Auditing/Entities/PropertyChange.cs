namespace HAB.Auditing.Entities;
public class PropertyChange
{

    #region Properties
    
    public required string PropertyName { get; set; }
    public required string PropertyType { get; set; }
    public required string OldValue { get; set; }
    public required string NewValue { get; set; }
    
    #endregion
    
    #region Methods

    public PropertyChange()
    {
        
    }
    
    public override string ToString()
    {
        return $"\t{PropertyName}: {OldValue} -> {NewValue}";
    }

    #endregion
    
}