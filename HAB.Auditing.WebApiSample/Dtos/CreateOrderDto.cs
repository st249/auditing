namespace HAB.Auditing.WebApiSample.Dtos;

public class CreateOrderDto
{
    public required string CustomerName { get; set; }

    public List<CreateOrderItemDto> OrderItems { get; set; } = new();
}
