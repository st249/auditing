using HAB.Auditing.WebApiSample.Models;

namespace HAB.Auditing.WebApiSample.Dtos;

public class OrderDto
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public DateTime OrderDate { get; set; }
    public int TotalQuantity { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal TotalDiscount { get; set; }

    public List<OrderItemDto> OrderItems { get; set; } = new();

    public OrderDto()
    {
        
    }

    public OrderDto(Order order)
    {
        Id = order.Id;
        CustomerName = order.CustomerName;
        OrderDate = order.OrderDate;
        TotalQuantity = order.TotalQuantity;
        TotalPrice = order.TotalPrice;
        TotalDiscount = order.TotalDiscount;
        OrderItems = order.OrderItems.Select(oi => new OrderItemDto(oi)).ToList();
    }
}
