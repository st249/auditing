namespace HAB.Auditing.WebApiSample.Dtos;

public class CreateOrderDto
{
    public string CustomerName { get; set; }

    public List<CreateOrderItemDto> OrderItems { get; set; } = new();
}
