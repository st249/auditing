namespace HAB.Auditing.Entities;
public class PropertyChange
{

    #region Properties
    public long Id { get; private set; }
    public string PropertyName { get; set; }
    public string PropertyType { get; set; }
    public string OldValue { get; set; }
    public string NewValue { get; set; }
    
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