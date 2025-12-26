using HAB.Auditing.WebApiSample.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HAB.Auditing.WebApiSample.EntityConfigurations;

public class OrderItemEntityConfigurations : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");
        
        builder.Property(x=>x.ProductName).IsRequired().HasMaxLength(128);
        
    }
}
