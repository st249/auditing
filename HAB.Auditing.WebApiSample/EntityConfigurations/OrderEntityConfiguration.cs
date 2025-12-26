using HAB.Auditing.WebApiSample.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HAB.Auditing.WebApiSample.EntityConfigurations;

public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");
        
        builder.Property(x=>x.CustomerName).IsRequired().HasMaxLength(128);
        
        builder.HasMany(x=>x.OrderItems)
            .WithOne(x=>x.Order)
            .HasForeignKey(x=>x.OrderId)
            .IsRequired();
    }
}
