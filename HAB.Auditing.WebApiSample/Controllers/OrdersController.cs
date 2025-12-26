using HAB.Auditing.AspnetCore.Attributes;
using HAB.Auditing.AspnetCore.Implementations;
using HAB.Auditing.WebApiSample.Context;
using HAB.Auditing.WebApiSample.Dtos;
using HAB.Auditing.WebApiSample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HAB.Auditing.WebApiSample.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController
{
    private readonly SampleDbContext _context;

    public OrdersController(SampleDbContext context)
    {
        _context = context;
    }

    [HttpGet("get-orders")]
    public async Task<IEnumerable<OrderDto>> GetOrders()
    {
        var orders = await _context.Orders.Include(e => e.OrderItems).ToListAsync();
        return orders.Select(e => new OrderDto(e));
    }

    [HttpGet("get-order/{id}")]
    public async Task<OrderDto?> GetOrders(int id)
    {
        var order = await _context.Orders.Include(e => e.OrderItems).FirstOrDefaultAsync(e => e.Id == id);
        return new OrderDto(order);
    }

    [HttpPost("create-order")]
    [Auditing]
    public async Task<OrderDto> CreateOrder(CreateOrderDto createDto,
        [FromHeader(Name = "X-User-Name")] string userName,
        [FromHeader(Name = "X-User-Id")] int userId)
    {
        var order = new Order()
        {
            CustomerName = createDto.CustomerName
        };
        createDto.OrderItems.ForEach(e =>
        {
            order.AddOrderItem(new OrderItem(e.Quantity, e.UnitPrice, e.Discount)
            {
                ProductName = e.ProductName
            });
        });
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return new OrderDto(order);
    }

    [HttpPost("add-item/{orderId}")]
    [Auditing]
    public async Task<OrderDto?> AddOrderItem(int orderId, CreateOrderItemDto createDto,
        [FromHeader(Name = "X-User-Name")] string userName,
        [FromHeader(Name = "X-User-Id")] int userId)
    {
        var order = await _context.Orders.Include(e => e.OrderItems).FirstOrDefaultAsync(e => e.Id == orderId);
        if (order == null)
            throw new Exception("Order not found");
        order.AddOrderItem(new OrderItem(createDto.Quantity, createDto.UnitPrice, createDto.Discount)
        {
            ProductName = createDto.ProductName
        });
        await _context.SaveChangesAsync();
        return new OrderDto(order);
    }

    [HttpDelete("remove-item/{orderId}/{itemId}")]
    [Auditing]
    public async Task<OrderDto?> UpdateOrder(int orderId, int itemId,
        [FromHeader(Name = "X-User-Name")] string userName,
        [FromHeader(Name = "X-User-Id")] int userId)
    {
        var order = await _context.Orders.Include(e => e.OrderItems).FirstOrDefaultAsync(e => e.Id == orderId);
        if (order == null)
            throw new Exception("Order not found");

        order.RemoveOrderItem(order.OrderItems.First(e => e.Id == itemId));
        await _context.SaveChangesAsync();
        return new OrderDto(order);
    }

    [HttpPatch("change-item-quantity/{orderId}/{itemId}")]
    [Auditing]
    public async Task<OrderDto?> ChangeItemQuantity(int orderId, int itemId, int newQuantity,
        [FromHeader(Name = "X-User-Name")] string userName,
        [FromHeader(Name = "X-User-Id")] int userId)
    {
        var order = await _context.Orders.Include(e => e.OrderItems).FirstOrDefaultAsync(e => e.Id == orderId);
        if (order == null)
            throw new Exception("Order not found");

        var item = order.OrderItems.First(e => e.Id == itemId);
        item.SetQuantity(newQuantity);
        await _context.SaveChangesAsync();
        return new OrderDto(order);
    }
}
