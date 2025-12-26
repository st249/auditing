using HAB.Auditing.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HAB.Auditing.EntityFramework.Configurations;

public class EntityChangeEntityConfigurations:IEntityTypeConfiguration<EntityChange>
{
    public void Configure(EntityTypeBuilder<EntityChange> builder)
    {
        builder.ToTable("__Audit_EntityChanges");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x=> x.EntityName).IsRequired().HasMaxLength(256);
        builder.Property(x=> x.EntityId).IsRequired().HasMaxLength(128);
        builder.Property(x=> x.ChangeType).IsRequired();
        
        builder.HasMany(x => x.PropertyChanges)
            .WithOne()
            .HasForeignKey("EntityChangeId")
            .IsRequired();
        
    }
}
