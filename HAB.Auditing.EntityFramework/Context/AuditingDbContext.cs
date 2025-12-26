using HAB.Auditing.Entities;
using Microsoft.EntityFrameworkCore;

namespace HAB.Auditing.EntityFramework.Context;

public class AuditingDbContext : DbContext
{
    public AuditingDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(
            GetType().Assembly,
            t => t.GetInterfaces().Any(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));
    }
}
