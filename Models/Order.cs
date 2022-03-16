using Online.DTOs;

namespace Online.Models;

public record Order
{
    public DateTimeOffset OrderDate { get; set; }
    public DateTimeOffset DeliveryDate { get; set; }
    public int CustomerId { get; set; }

    public OrderDTO asDto => new OrderDTO
    {
        OrderDate = OrderDate,
        DeliveryDate = DeliveryDate,
        CustomerId = CustomerId
    };
}