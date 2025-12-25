using HAB.Auditing.Entities;

namespace HAB.Auditing.Tests.Entities;

public class ChangeSetTests
{
    [Fact]
    public void CanCreateChangeSet_WithRequiredProperties_AndEntityAndPropertyChanges()
    {
        // Arrange
        var changeSet = new ChangeSet
        {
            ChangeTime = DateTime.UtcNow,
            UserId = 42,
            Reason = "Test Reason",
            ChangedBy = "tester",
            BrowserInfo = "Safari",
            ClientIp = "127.0.0.1"
        };

        var entityChange = new EntityChange
        {
            EntityId = "1",
            EntityName = "TestEntity"
        };

        var propertyChange1 = new PropertyChange
        {
            PropertyName = "Name",
            OldValue = "OldName",
            NewValue = "NewName",
            PropertyType = "System.String"
        };

        var propertyChange2 = new PropertyChange
        {
            PropertyName = "Age",
            OldValue = "25",
            NewValue = "26",
            PropertyType = "System.Int32"
        };

        entityChange.AddPropertyChange(propertyChange1);
        entityChange.AddPropertyChange(propertyChange2);

        // Act
        changeSet.AddEntityChange(entityChange);

        // Assert
        Assert.NotNull(changeSet);
        Assert.Equal("Test Reason", changeSet.Reason);
        Assert.Equal(42, changeSet.UserId);
        Assert.Equal("tester", changeSet.ChangedBy);
        Assert.Equal("Safari", changeSet.BrowserInfo);
        Assert.Equal("127.0.0.1", changeSet.ClientIp);

        Assert.Single(changeSet.EntityChanges);
        Assert.Equal("TestEntity", changeSet.EntityChanges[0].EntityName);
        Assert.Equal(2, changeSet.EntityChanges[0].PropertyChanges.Count);
        Assert.Equal("Name", changeSet.EntityChanges[0].PropertyChanges[0].PropertyName);
        Assert.Equal("OldName", changeSet.EntityChanges[0].PropertyChanges[0].OldValue);
        Assert.Equal("NewName", changeSet.EntityChanges[0].PropertyChanges[0].NewValue);
    }
}
