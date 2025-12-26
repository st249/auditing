using HAB.Auditing.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HAB.Auditing.EntityFramework.Configurations;

public class PropertyChangeEntityConfigurations : IEntityTypeConfiguration<PropertyChange>
{
    public void Configure(EntityTypeBuilder<PropertyChange> builder)
    {
        builder.ToTable("__Audit_PropertyChanges");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x=> x.PropertyName).IsRequired().HasMaxLength(256);
        builder.Property(x=> x.PropertyType).IsRequired().HasMaxLength(128);
        builder.Property(x=> x.OldValue).IsRequired();
        builder.Property(x=> x.NewValue).IsRequired();
    }
}
