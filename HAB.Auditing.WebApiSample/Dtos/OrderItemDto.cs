using HAB.Auditing.WebApiSample.Models;

namespace HAB.Auditing.WebApiSample.Dtos;

public class OrderItemDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalPrice { get; set; }

    public OrderItemDto()
    {
        
    }

    public OrderItemDto(OrderItem orderItem)
    {
        Id= orderItem.Id;
        OrderId= orderItem.OrderId;
        ProductName= orderItem.ProductName;
        Quantity= orderItem.Quantity;
        UnitPrice= orderItem.UnitPrice;
        Discount= orderItem.Discount;
        TotalPrice= orderItem.TotalPrice;
    }
}