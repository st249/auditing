namespace HAB.Auditing.WebApiSample.Models;

public class OrderItem
{
    public int Id { get; private set; }
    public int OrderId { get; private set; }
    public Order Order { get; private set; }
    public required string ProductName { get; init; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Discount { get; private set; }

    public decimal TotalPrice
    {
        get => (UnitPrice * Quantity) - Discount;
        set { }
    }

    private OrderItem()
    {
    }

    public OrderItem(int quantity, decimal unitPrice, decimal discount) : this()
    {
        Quantity = quantity;
        UnitPrice = unitPrice;
        Discount = discount;
    }

    public OrderItem SetUnitPrice(decimal unitPrice)
    {
        if (unitPrice < 0)
            throw new InvalidOperationException("Unit price can not be less than zero");
        UnitPrice = unitPrice;
        return this;
    }

    public OrderItem SetQuantity(int quantity)
    {
        if (quantity < 0)
            throw new InvalidOperationException("Quantity can not be less than or equal to zero");
        Quantity = quantity;
        return this;
    }

    public OrderItem SetDiscount(decimal discount)
    {
        if (discount > TotalPrice)
            throw new InvalidOperationException("Discount can not be greater than total price");
        Discount = discount;
        return this;
    }
}
