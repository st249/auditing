using HAB.Auditing.Entities;


ChangeSet changeSet = new ChangeSet()
{
    ChangeTime = DateTime.UtcNow,
    UserId = 1,
    Reason = "Initial change set",
    ChangedBy = "admin",
    BrowserInfo = "Chrome",
};
var entityChange = new EntityChange()
{
    EntityName = "User",
    EntityId = "1"
};

var entityChange2 = new EntityChange()
{
    EntityName = "Roles",
    EntityId = "1"
};

var propertyChange = new PropertyChange()
{
    PropertyName = "Name",
    OldValue = "Admin",
    NewValue = "Hassan"
};


var propertyChange2 = new PropertyChange()
{
    PropertyName = "CreationTime",
    OldValue = "",
    NewValue = DateTime.UtcNow.ToString("yy-MMM-dd dddd HH:mm:ss")
};

entityChange.AddPropertyChange(propertyChange);
entityChange2.AddPropertyChange(propertyChange2);

changeSet.AddEntityChange(entityChange);
changeSet.AddEntityChange(entityChange2);


Console.WriteLine(changeSet.ToString());
