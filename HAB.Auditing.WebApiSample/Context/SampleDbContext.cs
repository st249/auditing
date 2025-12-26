using HAB.Auditing.Entities;
using HAB.Auditing.EntityFramework.Context;
using HAB.Auditing.WebApiSample.Models;
using Microsoft.EntityFrameworkCore;

namespace HAB.Auditing.WebApiSample.Context;

public class SampleDbContext : AuditingDbContext
{
    public SampleDbContext(DbContextOptions<SampleDbContext> options):base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            GetType().Assembly,
            t => t.GetInterfaces().Any(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuditingDbContext).Assembly);
      
    }  
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
}
