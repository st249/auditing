namespace HAB.Auditing.WebApiSample.Dtos;

public class CreateOrderItemDto
{
    public required string ProductName { get; set; }

    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
}
