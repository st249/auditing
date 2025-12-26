using Microsoft.AspNetCore.Mvc;

namespace HAB.Auditing.WebApiSample.Models;

public class Order
{
    public int Id { get; private set; }
    public string CustomerName { get; init; } 
    public DateTime OrderDate { get; private set; } = DateTime.UtcNow;

    public int TotalQuantity
    {
        get=> _orderItems.Sum(oi => oi.Quantity);
        set { }
    }

    public decimal TotalPrice
    {
        get=> _orderItems.Sum(oi => oi.TotalPrice);
        set { }
    }
    
    public decimal TotalDiscount
    {
        get=> _orderItems.Sum(oi => oi.Discount);
        set { }
    }

    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public Order AddOrderItem(OrderItem orderItem)
    {
        _orderItems.Add(orderItem);
        return this;
    }
    
    public Order RemoveOrderItem(OrderItem orderItem)
    {
        _orderItems.Remove(orderItem);
        return this;
    }
    
}
