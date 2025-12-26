using HAB.Auditing.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HAB.Auditing.EntityFramework.Configurations;

public class ChangeSetEntityConfigurations : IEntityTypeConfiguration<ChangeSet>
{
    public void Configure(EntityTypeBuilder<ChangeSet> builder)
    {
        builder.ToTable("__Audit_ChangeSets");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x=> x.ChangeTime).IsRequired();
        builder.Property(x=> x.UserId).IsRequired().HasMaxLength(64);
        builder.Property(x=> x.Reason).IsRequired().HasMaxLength(256);
        builder.Property(x=> x.ChangedBy).HasMaxLength(128);
        builder.Property(x=> x.BrowserInfo).HasMaxLength(512);
        builder.Property(x=> x.ClientIp).HasMaxLength(64);

        builder.HasMany(x => x.EntityChanges)
            .WithOne()
            .HasForeignKey("ChangeSetId")
            .IsRequired(true);
    }
}
